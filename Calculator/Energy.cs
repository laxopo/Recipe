using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Energy
    {
        public List<Unit> Data { get; private set; } = new List<Unit>();

        public class Unit
        {
            public string Units { get; set; }
            public int Value { get; set; }

            public Unit(string units, int value)
            {
                Units = units;
                Value = value;
            }
        }

        public void Record(string units, int value)
        {
            if (units == null)
            {
                return;
            }

            foreach (var un in Data)
            {
                if (un.Units == units)
                {
                    un.Value += value;
                    return;
                }
            }

            Data.Add(new Unit(units, value));
        }
    }
}
