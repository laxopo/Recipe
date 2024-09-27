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
        public ItemType Type { get; set; }
        
        public enum ItemType
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
            switch (Type)
            {
                case ItemType.Fluid:
                    return "mB";

                case ItemType.Mechanism:
                    return "";

                default:
                    return "pcs";
            }
        }
    }
}
