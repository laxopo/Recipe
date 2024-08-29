using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Recipe.Library
{
    public class Item : ICloneable
    {
        public string Name { get; set; }
        public string IconPath { get; set; }
        public Type ItemType { get; set; }
        
        public enum Type
        {
            None,
            Item,
            Block,
            Fluid
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
