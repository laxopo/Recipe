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
            Default,
            Mechanism,
            Block,
            Fluid
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string GetUnits()
        {
            switch (ItemType)
            {
                case Type.Fluid:
                    return "mB";

                case Type.Mechanism:
                    return "";

                default:
                    return "pcs";
            }
        }
    }
}
