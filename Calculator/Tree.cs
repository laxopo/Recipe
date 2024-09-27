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

        public void ResetAmount(Bank bank)
        {
            Resources(bank).ForEach(x => x.Amount = 0);
        }

        public void ImportParams(Tree source)
        {
            Inputs.AddRange(source.Inputs);
            Intermediates.AddRange(source.Intermediates);
            Mechanisms.AddRange(source.Mechanisms);
            Outputs.AddRange(source.Outputs);
        }
    }
}
