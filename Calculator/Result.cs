using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Result
    {
        public Energy Energy { get; set; } = new Energy();
        public double Time { get; set; }
        public int Amount { get; set; }

        public string[] GetEnergyList(bool perItem)
        {
            var result = new List<string>();
            if (perItem)
            {
                Energy.Data.ForEach(x => result.Add((x.Value / Amount).ToString() + " " + x.Units));
            }
            else
            {
                Energy.Data.ForEach(x => result.Add(x.Value.ToString() + " " + x.Units));
            }
            
            return result.ToArray();
        }

        public double GetTime(bool perItem)
        {
            if (perItem)
            {
                return Time / Amount;
            }

            return Time;
        }
    }
}
