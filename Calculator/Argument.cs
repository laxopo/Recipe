using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Argument
    {
        public Resource Resource { get; set; }
        public double Coefficient { get; set; }
        public int MinValue { get; set; }

        public Argument(Resource resource, double coefficient)
        {
            Resource = resource;
            Coefficient = coefficient;
            MinValue = (int)Resource.ItemObject.QuantityOut;
        }
    }
}
