using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Mech
    {
        public Editor.ItemObject ItemObject { get; set; }
        public Tree Tree { get; set; }
        public List<Resource> Inputs { get; set; }
        public List<Resource> Outputs { get; set; }
        public int Coefficient { get; set; }

        public Mech(Editor.ItemObject iobj, Tree tree)
        {
            Inputs = new List<Resource>();
            Outputs = new List<Resource>();
            ItemObject = iobj;
            Tree = tree;
        }
    }
}
