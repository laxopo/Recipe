using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Resource
    {
        public Editor.ItemObject ItemObject { get; set; }
        public Mech LinkMechIn { get; set; }
        public Mech LinkMechOut { get; set; }
        public int Quantity { get; set; }
        public bool Insufficient { get; set; }
        public bool IsInput { get; set; }
        public bool IsOutput { get; set; }

        public Resource(Editor.ItemObject iobj)
        {
            ItemObject = iobj;
        }
    }
}
