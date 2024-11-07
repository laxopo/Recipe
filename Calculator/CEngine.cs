using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Recipe.Calculator
{
    public static class CEngine
    {
        public static List<Tree> Forest = new List<Tree>();
        public static List<Resource> PublicResources = new List<Resource>();
        public static ContainerVO Inputs, Outputs;
        public static Tree SelectedTree;

        public static VisualObject SelectedVO
        {
            get
            {
                return selectedVO;
            }

            set
            {
                selectedVO = value;
                SelectedTree.InitialRes = selectedVO.Resources[0];
            }
        }

        private static VisualObject selectedVO;
        private static Action FormUpdate;
        private static Action FormClear;
        private static bool isActual = false;

        public static bool IsActual
        {
            get
            {
                return isActual;
            }
            set
            {
                if (value == isActual)
                {
                    return;
                }

                if (value)
                {
                    Update();
                }
                else
                {
                    Clear();
                }

                isActual = value;
            }
        }

        public static void Initialize(Action clear, Action update, Control contIn, Control contOut)
        {
            FormUpdate = update;
            FormClear = clear;
            Inputs = new ContainerVO(contIn);
            Outputs = new ContainerVO(contOut);
        }

        public static void Clear()
        {
            FormClear();
            Forest.Clear();
            PublicResources.Clear();
        }

        public static void Update()
        {
            if (isActual)
            {
                return;
            }

            FormClear();
            GenerateTrees();
            FormUpdate();
            isActual = true;
        }

        public static void GenerateTrees()
        {
            //clear references
            Editor.Engine.IODataBase.ForEach(x => x.Tree = null);

            //create res, mech and create raw trees
            if (!InitializeTrees())
            {
                return;
            }

            //generate a data for trees
            foreach (var tree in Forest)
            {
                //generate tree names
                if (tree.Outputs.Count > 0)
                {
                    tree.Name = tree.Outputs[0].ItemObject.Item.Name;
                }

                //generate links
                GenerateLinks(tree);
            }
        }

        public static void GenerateVOs(Tree tree)
        {
            Inputs.Clear();
            Outputs.Clear();

            Inputs.AddResRange(tree.Inputs);
            Outputs.AddResRange(tree.Outputs);

            SelectedTree = tree;
        }

        public static void Calculate(Tree tree, bool modeOutput) //ONLY OUTPUT
        {
            if (tree == null || tree.InitialRes == null)
            {
                return;
            }

            if (tree.InitialRes.IOType == Resource.Type.Input) //UNHADLED
            {
                return;
            }

            //init
            var req_path = new List<Mech>();
            tree.ResetAmounts();
            foreach (var pr in PublicResources)
            {
                pr.Tree.ResetAmounts();
                pr.Bridged = false;
            }

            Inputs.RemoveExtras();
            Outputs.RemoveExtras();
            SelectedVO.UpdateData();

            //calculate
            Processing(SelectedVO.Resources[0], false);

            Inputs.Clear();

            //collect all used trees
            var bush = new List<Tree>();
            bush.Add(tree);
            foreach (var pr in PublicResources)
            {
                if (pr.Bridged)
                {
                    if (!bush.Contains(pr.Tree))
                    {
                        bush.Add(pr.Tree);
                    }
                }
            }

            //create VOs
            foreach (var bt in bush)
            {
                foreach (var res in bt.Inputs)
                {
                    if (res.Injected > 0)
                    {
                        Inputs.AddRes(res);
                    }

                    if (res.Amount == 0 && res.Insufficient)
                    {
                        Inputs.AddRes(res);
                    }
                }

                foreach (var res in bt.Intermediates)
                {
                    if (res.Amount > 0)
                    {
                        Outputs.AddRes(res);
                    }
                    else if (res.Insufficient)
                    {
                        Inputs.AddRes(res);
                    }
                }
            }

            //Update VOs data
            Inputs.UpdateVOs();
            Outputs.UpdateVOs();


            void Processing(Resource res, bool forward)
            {
                var RES = res.ItemObject.ID; //test
                //VisualObject.SelectIO(res.ItemObject); //test

                Mech mech;

                if (!forward)
                {
                    if (res.IOType == Resource.Type.Input)
                    {
                        Resource bridge = null;
                        foreach (var pr in PublicResources)
                        {
                            if (pr.ItemObject.Item.Name == res.ItemObject.Item.Name)
                            {
                                pr.Bridged = true;
                                bridge = pr;
                                break;
                            }
                        }

                        if (bridge != null)
                        {
                            bridge.Request = res.Request;
                            Processing(bridge, false);

                            res.Amount += bridge.Amount;
                            bridge.Amount = 0;
                        }
                        else
                        {
                            res.Amount += res.Request;
                            res.Injected += res.Request;
                        }

                        return;
                    }

                    mech = res.LinkMechIn;
                }
                else
                {
                    if (res.IOType == Resource.Type.Output)
                    {
                        return;
                    }

                    mech = res.LinkMechOut;
                }

                var MECH = mech.ItemObject.ID; //test
                //VisualObject.SelectIO(mech.ItemObject); //test

                if (!forward)
                {
                    if (req_path.Contains(mech))
                    {
                        if (res.Amount < res.Request)
                        {
                            res.Insufficient = true;
                            res.Injected = res.Request - res.Amount;
                            res.Amount += res.Injected;
                        }
                        return;
                    }

                    req_path.Add(mech);

                    mech.Coefficient = res.Request / res.AmountIn;
                    if (res.Request % res.AmountIn != 0)
                    {
                        mech.Coefficient++;
                    }
                    
                    foreach (var resIn in mech.Inputs)
                    {
                        var RESIN = resIn.ItemObject.ID; //test
                        //VisualObject.SelectIO(resIn.ItemObject); //test

                        resIn.Request = resIn.AmountOut * mech.Coefficient;
                        Processing(resIn, false);
                        resIn.Amount -= resIn.Request;
                    }

                    foreach (var resOut in mech.Outputs)
                    {
                        var RESOUT = resOut.ItemObject.ID; //test
                        //VisualObject.SelectIO(resOut.ItemObject); //test

                        resOut.Amount = resOut.AmountIn * mech.Coefficient - resOut.Injected;

                        if (resOut == res)
                        {
                            continue;
                        }

                        if (resOut.Amount > 0)
                        {
                            Processing(resOut, true);
                        }
                    }

                    req_path.Remove(mech);
                }
                else
                {
                    if (req_path.Contains(mech))
                    {
                        return;
                    }

                    //get the mech coefficient
                    mech.Coefficient = -1;
                    foreach (var resIn in mech.Inputs)
                    {
                        if (mech.Coefficient == 0)
                        {
                            return;
                        }

                        int k = resIn.Amount / resIn.AmountOut;

                        if (mech.Coefficient > k || mech.Coefficient == -1)
                        {
                            mech.Coefficient = k;
                        }
                    }


                    //calc input amounts
                    foreach (var resIn in mech.Inputs)
                    {
                        var RESIN = resIn.ItemObject.ID; //test
                        //VisualObject.SelectIO(resIn.ItemObject); //test

                        resIn.Amount -= resIn.AmountOut * mech.Coefficient;
                    }

                    //calc output amounts
                    foreach (var resOut in mech.Outputs)
                    {
                        var RESOUT = resOut.ItemObject.ID; //test
                        resOut.Amount += resOut.AmountIn * mech.Coefficient;

                        if (resOut.Amount > 0)
                        {
                            Processing(resOut, true);
                        }
                    }
                }
            }
        }

        public static void InitCalc(Tree tree)
        {
            if (Outputs.VisualObjects[0].Resources.Count > 1)
            {
                return;
            }

            Outputs.VisualObjects[0].SelectVO();
            tree.InitialRes = Outputs.VisualObjects[0].Resources[0];
            tree.InitialRes.Request = 1;
            Calculate(SelectedTree, true);
        }

        /**/

        private static bool InitializeTrees()
        {
            var done = new List<Editor.ItemObject>();
            bool error = false;

            //build the trees
            foreach (var iobj in Editor.Engine.IODataBase)
            {
                if (done.Contains(iobj))
                {
                    continue;
                }

                var tree = NodeExp(iobj);

                if (error)
                {
                    VisualObject.SelectIO(iobj);
                    return false;
                }

                if (!Forest.Contains(tree))
                {
                    Forest.Add(tree);
                }
            }

            return true;

            Tree NodeExp(Editor.ItemObject iobj, Tree tree = null)
            {
                var IOBJ = iobj.ID; //test

                //define tree
                if (tree == null)
                {
                    //create new tree
                    tree = new Tree();
                }

                if (iobj.LinkInTags.Count > 0 || iobj.LinkOutTags.Count > 0) //linked iobj
                {
                    //check the link rule
                    if ((iobj.Item.Type == Library.Item.ItemType.Mechanism &&
                        (iobj.LinkInTags.Count == 0 || iobj.LinkOutTags.Count == 0)) || //mech
                        (iobj.Item.Type != Library.Item.ItemType.Mechanism &&
                        (iobj.LinkInTags.Count > 1 || iobj.LinkOutTags.Count > 1))) //res
                    {
                        MessageBox.Show("Incorrect number of links.", "Tree Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        error = true;
                        return null;
                    }

                    if (iobj.Item.Type == Library.Item.ItemType.Mechanism) //mech
                    {
                        //create new mech
                        tree.Mechanisms.Add(new Mech(iobj, tree));
                    }
                    else //resource
                    {
                        //create new resource
                        var res = new Resource(iobj, tree)
                        {
                            AmountIn = iobj.QuantityIn,
                            AmountOut = iobj.QuantityOut,
                        };

                        if (iobj.LinkInTags.Count == 0 || iobj.IOType == Editor.ItemObject.Type.Input) //input
                        {
                            res.IOType = Resource.Type.Input;
                            tree.Inputs.Add(res);
                        }
                        else if (iobj.LinkOutTags.Count == 0 || iobj.IOType == Editor.ItemObject.Type.Output) //output
                        {
                            res.IOType = Resource.Type.Output;
                            tree.Outputs.Add(res);
                            if (iobj.Public)
                            {
                                PublicResources.Add(res);
                            }
                        }
                        else //inter
                        {
                            res.IOType = Resource.Type.None;
                            tree.Intermediates.Add(res);
                        }
                    }
                }

                done.Add(iobj);

                var links = new List<Editor.ItemObject>(iobj.LinkInTags);
                links.AddRange(iobj.LinkOutTags);

                //inputs & output
                foreach (var ioIn in links)
                {
                    if (done.Contains(ioIn))
                    {
                        continue;
                    }

                    //check the type linking rule
                    if (!RuleTypeLinking(iobj, ioIn))
                    {
                        error = true;
                        return null;
                    }

                    NodeExp(ioIn, tree);
                }

                return tree;
            }
        }

        private static bool RuleTypeLinking(Editor.ItemObject iobj1, Editor.ItemObject iobj2)
        {
            bool tc = iobj1.Item.Type == Library.Item.ItemType.Mechanism;
            bool to = iobj2.Item.Type == Library.Item.ItemType.Mechanism;

            if (tc == to)
            {
                MessageBox.Show("The object is linked to another with the same type.", "Tree Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private static void GenerateLinks(Tree tree)
        {
            var doneRes = new List<Resource>();
            var doneMech = new List<Mech>();
            var resources = tree.Resources(Tree.Bank.All);
            
            //Res > Mech > Res1 > Mech1 > ...
            var pathMech = new List<Mech>();
            var pathRes = new List<Resource>();


            foreach (var res in tree.Inputs)
            {
                if (doneRes.Contains(res))
                {
                    continue;
                }

                NodeExp(res);
            }


            void NodeExp(Resource res)
            {
                var RES = res.ItemObject.ID; //test

                //completion check
                if (doneRes.Contains(res))
                {
                    return;
                }

                doneRes.Add(res);

                if (res.IOType == Resource.Type.Output)
                {
                    return;
                }

                //update path
                pathRes.Add(res);
                var mech = tree.Mechanisms.Find(x => x.ItemObject == res.ItemObject.LinkOutTags[0]);
                var MECH = mech.ItemObject.ID;

                res.LinkMechOut = mech;
                mech.Inputs.Add(res);

                if (pathMech.Contains(mech))
                {
                    /*
                    R   M
                    16  15
                    13* 10
                    14* (15)
                     */

                    int i = 0;
                    int idx = pathMech.IndexOf(mech);
                    while (pathMech.Count - i != idx)
                    {
                        pathRes[pathRes.Count - 1 - i].Renewable = true;
                        i++;
                    }

                    pathRes.Remove(res);
                    return;
                }

                if (doneMech.Contains(mech))
                {
                    pathRes.Remove(res);
                    return;
                }

                doneMech.Add(mech);
                pathMech.Add(mech);       

                //outputs
                foreach (var iobjOut in mech.ItemObject.LinkOutTags)
                {
                    var resOut = resources.Find(x => x.ItemObject == iobjOut);

                    resOut.LinkMechIn = mech;
                    mech.Outputs.Add(resOut);
                    NodeExp(resOut);
                }

                pathMech.Remove(mech);
                pathRes.Remove(res);
            }
        }
    }
}
