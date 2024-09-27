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
        public static List<VisualObject> VisualObjects = new List<VisualObject>();

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

        public static void Initialize(Action clear, Action update)
        {
            FormUpdate = update;
            FormClear = clear;
        }

        public static void Clear()
        {
            FormClear();
            Forest.Clear();
            VisualObjects.Clear();
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

            //test
            var inputs = new List<int>();
            var outputs = new List<int>();
            var inters = new List<int>();
            var mechs = new List<int>();

            Forest[0].Inputs.ForEach(x => inputs.Add(x.ItemObject.ID));
            Forest[0].Outputs.ForEach(x => outputs.Add(x.ItemObject.ID));
            Forest[0].Intermediates.ForEach(x => inters.Add(x.ItemObject.ID));
            Forest[0].Mechanisms.ForEach(x => mechs.Add(x.ItemObject.ID));
            //test end

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

        public static void GenerateVOs(Tree tree, Control areaInputs, Control areaOutputs)
        {
            VisualObjects = new List<VisualObject>();

            areaInputs.Controls.Clear();
            areaOutputs.Controls.Clear();

            Gen(tree.Resources(Tree.Bank.InputConstant), areaInputs);
            Gen(tree.Outputs, areaOutputs);

            void Gen(List<Resource> source, Control container)
            {
                container.Height = 0;
                int x = 0, y = 0, h = 0;

                foreach (var res in source)
                {
                    var vo = new VisualObject(res);
                    VisualObjects.Add(vo);

                    if (h < vo.Container.Height)
                    {
                        h = vo.Container.Height;
                    }

                    if (x + vo.Container.Width > container.Width)
                    {
                        //new line
                        x = 0;
                        y += h + 10;

                    }

                    vo.Container.Location = new Point(x, y);

                    if (container.Height < vo.Container.Bottom)
                    {
                        container.Height = vo.Container.Bottom;
                    }

                    container.Controls.Add(vo.Container);

                    x += vo.Container.Width + 10;
                }
            }
        }

        public static void Calculate(Tree tree)
        {
            tree.Outputs.ForEach(x => x.Amount = 0);
            tree.Intermediates.ForEach(x => x.Amount = 0);

            var res_done = new List<Resource>();
            var mech_done = new List<Mech>();
            var mech_wait = new List<Mech>();

            tree.ResetAmount(Tree.Bank.Output);

            foreach (var res in tree.Inputs)
            {
                if (res_done.Contains(res))
                {
                    continue;
                }

                Processing(res);
            }

            VisualObjects.ForEach(x => x.UpdateVO());


            void Processing(Resource res)
            {
                var RES = res.ItemObject.ID;    //test
                var RES_Q = res.Amount;         //test

                if (res_done.Contains(res))
                {
                    return;
                }

                //assign this res as done
                res_done.Add(res);

                //res is an output
                if (res.IOType == Resource.Type.Output)
                {
                    return;
                }

                //get the mech with input "res"
                var mech = res.LinkMechOut;

                //calc productivity coefficient
                mech.Coefficient = -1;
                foreach (var resIn in mech.Inputs)
                {
                    if (resIn.IOType == Resource.Type.None) //inter
                    {
                        continue;
                    }

                    int s = resIn.Amount / resIn.AmountOut;

                    if (resIn.IOType == Resource.Type.Input)
                    {
                        if (!res_done.Contains(resIn))
                        {
                            res_done.Add(resIn);
                        }
                    }
                    else if (s == 0)
                    {
                        mech_wait.Add(mech);
                        return;
                    }

                    if (mech.Coefficient == -1 || mech.Coefficient > s)
                    {
                        mech.Coefficient = s;
                    }
                }

                mech_wait.Remove(mech);
                mech_done.Add(mech);

                if (mech.Coefficient > 0)
                {
                    //take an aliquot amount of all inputs
                    foreach (var resIn in mech.Inputs)
                    {
                        var RESIN = resIn.ItemObject.ID; //test

                        resIn.Amount -= resIn.AmountOut * mech.Coefficient;
                        resIn.Insufficient = resIn.Amount < 0;
                    }

                    //produce an aliquot amount of the output and put it to the next mech
                    foreach (var resOut in mech.Outputs)
                    {
                        var RESOUT = resOut.ItemObject.ID; //test
                        if (res_done.Contains(resOut))
                        {
                            continue;
                        }

                        resOut.Amount += resOut.AmountIn * mech.Coefficient;
                        Processing(resOut);
                    }
                }
            }
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
                        tree.Mechanisms.Add(new Mech(iobj));
                    }
                    else //resource
                    {
                        //create new resource
                        if (iobj.LinkInTags.Count == 0) //input
                        {
                            tree.Inputs.Add(new Resource(iobj)
                            {
                                AmountOut = iobj.QuantityOut,
                                IOType = Resource.Type.Input
                            });
                        }
                        else if (iobj.LinkOutTags.Count == 0) //output
                        {
                            tree.Outputs.Add(new Resource(iobj)
                            {
                                AmountIn = iobj.QuantityIn,
                                IOType = Resource.Type.Output
                            });
                        }
                        else //inter
                        {
                            tree.Intermediates.Add(new Resource(iobj)
                            {
                                AmountIn = iobj.QuantityIn,
                                AmountOut = iobj.QuantityOut,
                                IOType = Resource.Type.None
                            });
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
                var mech = tree.Mechanisms.Find(x => x.ItemObject == res.ItemObject);
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

                //input
                res.LinkMechOut = mech;
                mech.Inputs.Add(res);

                //outputs
                foreach (var iobjOut in mech.ItemObject.LinkOutTags)
                {
                    var resOut = resources.Find(x => x.ItemObject == iobjOut);

                    resOut.LinkMechIn = mech;
                    NodeExp(resOut);
                }

                pathMech.Remove(mech);
                pathRes.Remove(res);
            }
        }
    }
}
