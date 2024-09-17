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
            ScanTrees(Forest);
            FormUpdate();
            isActual = true;
        }


        public static void ScanTrees(List<Tree> forest)
        {
            Editor.Engine.IODataBase.ForEach(x => x.Tree = null);
            var done = new List<Editor.ItemObject>();
            var path = new List<Editor.ItemObject>();
            Tree tree = null;

            void NodeExp(Editor.ItemObject iobj)
            {
                var IOBJ = iobj.ID; //test

                //path level-down
                path.Add(iobj);

                //read all outputs
                foreach (var linkOut in iobj.LinkOutTags)
                {
                    //if the iobj is scanned or the path is looped
                    if (done.Contains(linkOut) || path.Contains(linkOut))
                    {
                        //get the existed tree
                        if (linkOut.Tree != null && tree == null)
                        {
                            tree = linkOut.Tree;
                        }

                        //skip iobj
                        continue;
                    }

                    //open included iobj
                    NodeExp(linkOut);
                }

                //if this is an ending of a path and the tree is still undefined
                if (tree == null)
                {
                    //create a new tree
                    tree = new Tree()
                    {
                        Name = forest.Count.ToString()
                    };
                    forest.Add(tree);
                }

                bool unlinked = false;
                var res = new Resource(iobj);
                if (iobj.LinkOutTags.Count == 0)
                {
                    if (iobj.LinkInTags.Count == 0)
                    {
                        unlinked = true;
                    }
                    else
                    {
                        //mark it as output
                        res.IOType = Resource.IO.Output;
                        tree.Outputs.Add(res);
                    }
                }
                else if (iobj.LinkInTags.Count == 0)
                {
                    //mark it as input
                    res.IOType = Resource.IO.Input;
                    tree.Inputs.Add(res);
                }
                else
                {
                    //it is an intermediate
                    if (iobj.Item.ItemType == Library.Item.Type.Mechanism)
                    {
                        tree.Mechanisms.Add(new Mech(iobj));
                    }
                    else
                    {
                        tree.Intermediates.Add(res);
                    }
                }

                if (!unlinked)
                {
                    //create a reference to the current tree in this iobj
                    iobj.Tree = tree;

                    //level-up
                    path.Remove(iobj);
                }

                //mark the iobj as done
                done.Add(iobj);
            }

            //build the trees
            foreach (var iobj in Editor.Engine.IODataBase)
            {
                if (done.Contains(iobj))
                {
                    continue;
                }

                NodeExp(iobj);
            }

            foreach (var tr in forest)
            {
                //generate tree names
                if (tr.Outputs.Count > 0)
                {
                    tr.Name = tr.Outputs[0].ItemObject.Item.Name;
                }

                //generate links
                var result = GenerateLinks(tr);
                if (result.Fault)
                {
                    MessageBox.Show(result.Text, "Failed to link", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    forest.Clear();
                    return;
                }

                //find intemediate i/o or const
                tr.Inputs.ForEach(x => x.Quantity = 1);
                Trace(tr);

                var rmInters = new List<Resource>();
                foreach (var inter in tr.Intermediates)
                {
                    if (inter.Quantity >= 0 && inter.Insufficient)
                    {
                        inter.IOType = Resource.IO.Constant;
                        tr.Constants.Add(inter.Clone() as Resource);
                    }

                    if (inter.Quantity < 0)
                    {
                        inter.IOType = Resource.IO.Input;
                        tr.Inputs.Add(inter);
                        rmInters.Add(inter);
                    }
                    else if (inter.Quantity > 0)
                    {
                        inter.IOType = Resource.IO.Output;
                        tr.Outputs.Add(inter);
                        rmInters.Add(inter);
                    }
                }

                //remove all relocated resources in itermediate bank
                rmInters.ForEach(x => tr.Intermediates.Remove(x));

                //normalize coeffs
                double k = -1;
                foreach (var output in tr.Outputs)
                {
                    if (k == -1 || k > output.Quantity)
                    {
                        k = output.Quantity;
                    }
                }

                k = 1 / k;

                //Generate equation
                tr.Equation = new Equation();
                tr.Outputs.ForEach(y => tr.Equation.Outputs.Add(
                    new Argument(y, y.Quantity * k)));
                tr.Inputs.ForEach(x => tr.Equation.Inputs.Add(
                    new Argument(x, k)));

                //
                tr.Inputs.ForEach(x => x.Quantity = tr.Equation.Inputs.Find(y => y.Resource == x).Coefficient);
                tr.Outputs.ForEach(x => x.Quantity = tr.Equation.Outputs.Find(y => y.Resource == x).Coefficient);
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
            tree.Outputs.ForEach(x => x.Quantity = 0);
            tree.Inputs.ForEach(x => x.Quantity = x.Given);

            tree.Equation.Calculate();

            VisualObjects.ForEach(x => x.UpdateVO());
        }

        /**/

        private static void Trace(Tree tree)
        {
            var done = new List<Resource>();
            var path = new List<Mech>();

            tree.Outputs.ForEach(x => x.Quantity = 0);

            foreach (var inter in tree.Intermediates)
            {
                if (inter.IOType != Resource.IO.Constant)
                {
                    inter.Quantity = inter.ItemObject.Injected;
                }
            }

            void TraceNodes(Resource res)
            {
                var RES = res.ItemObject.ID; //test
                var RES_Q = res.Quantity;

                if (done.Contains(res))
                {
                    return;
                }

                //assign this res as done
                done.Add(res);

                //update the quantity
                res.Quantity += res.ItemObject.Injected;

                //res is an output
                if (tree.Outputs.Contains(res))
                {
                    return;
                }

                //get the mech with input "res"
                var mech = res.LinkMechOut;
                if (path.Contains(mech))
                {
                    return;
                }
                else
                {
                    path.Add(mech);
                }

                //calc productivity coefficient
                double k = res.Quantity / res.ItemObject.QuantityOut;
                mech.Coefficient = k;

                //take an aliquot amount of all inputs
                foreach (var resIn in mech.Inputs)
                {
                    var RESIN = resIn.ItemObject.ID; //test
                    resIn.Quantity -= resIn.ItemObject.QuantityOut * k;
                    resIn.Insufficient = resIn.Quantity < 0;
                }

                //produce an aliquot amount of the output and put it to the next mech
                foreach (var resOut in mech.Outputs)
                {
                    var RESOUT = resOut.ItemObject.ID;
                    if (done.Contains(resOut))
                    {
                        continue;
                    }

                    resOut.Quantity += resOut.ItemObject.QuantityIn * k;
                    TraceNodes(resOut);
                }

                //mech is done
                path.Remove(mech);
            }

            foreach (var res in tree.Inputs)
            {
                if (done.Contains(res))
                {
                    continue;
                }

                TraceNodes(res);
            }
        }

        private static Message GenerateLinks(Tree tree)
        {
            var resources = tree.Resources(Tree.Bank.All);

            //link resources
            foreach (var res in resources)
            {
                if (res.ItemObject.LinkInTags.Count > 1 || res.ItemObject.LinkOutTags.Count > 1)
                {
                    //error
                    return new Message("The item has more than 1 link at the input or output side.", true);
                }

                var listIn = res.ItemObject.LinkInTags;
                var listOut = res.ItemObject.LinkOutTags;

                if (listIn.Count > 0 && listIn[0].Item.ItemType == Library.Item.Type.Mechanism)
                {
                    res.LinkMechIn = tree.Mechanisms.Find(x => x.ItemObject == listIn[0]);
                }

                if (listOut.Count > 0 && listOut[0].Item.ItemType == Library.Item.Type.Mechanism)
                {
                    res.LinkMechOut = tree.Mechanisms.Find(x => x.ItemObject == listOut[0]);
                }
            }

            //link mechanisms
            foreach (var mech in tree.Mechanisms)
            {
                if (mech.ItemObject.LinkInTags.Count == 0 || mech.ItemObject.LinkOutTags.Count == 0)
                {
                    //error
                    return new Message("The mechanism has no links at the input or output side.", true);
                }

                //inputs
                foreach (var link in mech.ItemObject.LinkInTags)
                {
                    if (link.Item.ItemType == Library.Item.Type.Mechanism)
                    {
                        return new Message("The mechanism has an input link to the other one", true);
                    }

                    mech.Inputs.Add(resources.Find(x => x.ItemObject == link));
                }

                //outputs
                foreach (var link in mech.ItemObject.LinkOutTags)
                {
                    if (link.Item.ItemType == Library.Item.Type.Mechanism)
                    {
                        //error
                        return new Message("The mechanism has an output link to the other one", true);
                    }

                    mech.Outputs.Add(resources.Find(x => x.ItemObject == link));
                }
            }

            return new Message("", false);
        }
    }
}
