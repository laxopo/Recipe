using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Tree
    {
        public string Name { get; set; }
        public List<Resource> Inputs { get; set; }
        public List<Resource> Outputs { get; set; }
        public List<Resource> Constants { get; set; }
        public List<Resource> Intermediates { get; set; }
        public List<Mech> Mechanisms { get; set; }
        public Equation Equation { get; set; }

        public Tree()
        {
            Inputs = new List<Resource>();
            Outputs = new List<Resource>();
            Intermediates = new List<Resource>();
            Constants = new List<Resource>();
            Mechanisms = new List<Mech>();
        }

        public enum Bank
        {
            All,
            InputOutput,
            InputIntermediate,
            OutputIntermediate,
            InputConstant,
        }

        public List<Resource> Resources(Bank bank)
        {
            var list = new List<Resource>();

            switch (bank)
            {
                case Bank.All:
                    list.AddRange(Inputs);
                    list.AddRange(Outputs);
                    list.AddRange(Intermediates);
                    list.AddRange(Constants);
                    break;

                case Bank.InputOutput:
                    list.AddRange(Inputs);
                    list.AddRange(Outputs);
                    break;

                case Bank.InputIntermediate:
                    list.AddRange(Inputs);
                    list.AddRange(Intermediates);
                    break;

                case Bank.OutputIntermediate:
                    list.AddRange(Outputs);
                    list.AddRange(Intermediates);
                    break;

                case Bank.InputConstant:
                    list.AddRange(Inputs);
                    list.AddRange(Constants);
                    break;
            }

            return list;
        }
    }
}
