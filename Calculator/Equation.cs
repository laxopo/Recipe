using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Equation
    {
        public List<Argument> Inputs { get; set; }
        public List<Argument> Outputs { get; set; }

        public Equation()
        {
            Inputs = new List<Argument>();
            Outputs = new List<Argument>();
        }

        public void Calculate()
        {
            double k = -1;

            foreach (var x in Inputs)
            {
                if (x.Resource.Quantity < x.MinValue)
                {
                    k = 0;
                    break;
                }

                double s = x.Resource.Quantity / x.Coefficient;
                if (k > s || k == -1)
                {
                    k = s;
                }    
            }

            Inputs.ForEach(x => x.Resource.Quantity -= x.Coefficient * k);
            Outputs.ForEach(y => y.Resource.Quantity = y.Coefficient * k);
        }
    }
}
