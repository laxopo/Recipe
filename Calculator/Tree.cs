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
        public List<Resource> Intermediates { get; set; }
        public Resource InitialRes { get; set; }
        public List<Mech> Mechanisms { get; set; }

        public Tree()
        {
            Inputs = new List<Resource>();
            Outputs = new List<Resource>();
            Intermediates = new List<Resource>();
            Mechanisms = new List<Mech>();
        }

        public enum Bank
        {
            All,
            Input,
            Output,
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
                    break;

                case Bank.Input:
                    list = Inputs;
                    break;

                case Bank.Output:
                    list = Outputs;
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
            }

            return list;
        }

        public void ResetAmounts()
        {
            var resources = Resources(Bank.All);

            foreach (var res in resources)
            {
                res.Amount = 0;
                res.Request = 0;
                res.Injected = 0;
            }
        }
    }
}
