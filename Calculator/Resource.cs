using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Resource : ICloneable
    {
        public Editor.ItemObject ItemObject { get; set; }
        public Mech LinkMechIn { get; set; }
        public Mech LinkMechOut { get; set; }
        public double Quantity { get; set; }
        public int Given { get; set; }
        public bool Insufficient { get; set; }
        public IO IOType { get; set; }

        public Resource(Editor.ItemObject iobj)
        {
            ItemObject = iobj;
        }

        public enum IO
        {
            None,
            Input,
            Output,
            Constant
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
