using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recipe.Library
{
    public class Exp
    {
        public TreeNode Node { get; set; }
        public Directory Directory { get; set; }
        public List<Item> Items { get; set; }
        public List<Exp> SubNodes { get; set; }
        public Exp Parent { get; set; }

        public Exp()
        {
            Items = new List<Item>();
            SubNodes = new List<Exp>();
        }

        public Exp(string nodeName)
        {
            Node = new TreeNode(nodeName);
            Items = new List<Item>();
            SubNodes = new List<Exp>();
        }
    }
}
