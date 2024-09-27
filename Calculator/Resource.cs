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
        public int AmountIn { get; set; }
        public int AmountOut { get; set; }
        public int Amount { get; set; }
        public bool Renewable { get; set; }
        public bool Insufficient { get; set; }
        public Type IOType { get; set; }

        public Resource(Editor.ItemObject iobj)
        {
            ItemObject = iobj;
        }

        public enum Type
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
