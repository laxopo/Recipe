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
        public static void ScanTrees(List<Tree> trees)
        {
            Editor.EEngine.IODataBase.ForEach(x => x.Tree = null);
            var done = new List<Editor.ItemObject>();
            var path = new List<Editor.ItemObject>();
            Tree tree = null;

            void NodeExp(Editor.ItemObject iobj)
            {
                var IIO = iobj.ID; //test

                //path level-down
                path.Add(iobj);

                //read all outputs
                foreach (var linkOut in iobj.LinkOutTags)
                {
                    var LNK = linkOut.ID; //test
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
                        Name = trees.Count.ToString()
                    };
                    trees.Add(tree);
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
                        res.IsOutput = true;
                        tree.Outputs.Add(res);
                    }
                }
                else if (iobj.LinkInTags.Count == 0)
                {
                    //mark it as input
                    res.IsInput = true;
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
            foreach (var iobj in Editor.EEngine.IODataBase)
            {
                if (done.Contains(iobj))
                {
                    continue;
                }

                var EIO = iobj.ID; //test
                NodeExp(iobj);
            }

            foreach (var tr in trees)
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
                    trees.Clear();
                    return;
                }
            }
        }

        public static void Calculate(Tree tree)
        {
            var done = new List<Resource>();
            var path = new List<Mech>();

            tree.Outputs.ForEach(x => x.Quantity = 0);

            void Calc(Resource res)
            {
                var RES = res.ItemObject.ID; //test

                //assign this res as done
                done.Add(res);

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
                int k = res.Quantity / res.ItemObject.QuantityOut;

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
                    var RESOUT = resOut.ItemObject.ID; //test
                    if (done.Contains(resOut))
                    {
                        continue;
                    }

                    resOut.Quantity += resOut.ItemObject.QuantityIn * k;
                    Calc(resOut);
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

                Calc(res);
            }
        }

        public static List<VisualObject> GenerateVOs(Tree tree, GroupBox areaInputs, GroupBox areaOutputs)
        {
            List<VisualObject> list = new List<VisualObject>();

            void Gen(List<Resource> source, GroupBox container)
            {
                int x = 10, y = 15, h = 0;

                foreach (var res in source)
                {
                    var vo = new VisualObject(res);
                    list.Add(vo);

                    if (x + vo.Container.Width > container.Width - 10)
                    {
                        x = 10;
                        y += h + 10;
                    }

                    vo.Container.Location = new Point(x, y);
                    container.Controls.Add(vo.Container);

                    if (h < vo.Container.Height)
                    {
                        h = vo.Container.Height;
                    }

                    x += vo.Container.Width + 10;
                }
            }

            areaInputs.Controls.Clear();
            areaOutputs.Controls.Clear();
            Gen(tree.Inputs, areaInputs);
            Gen(tree.Outputs, areaOutputs);

            return list;
        }

        /**/

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
