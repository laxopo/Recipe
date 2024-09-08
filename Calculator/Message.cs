using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Calculator
{
    public class Message
    {
        public bool Fault { get; set; }
        public string Text { get; set; }

        public Message(string text, bool fault)
        {
            Fault = fault;
            Text = text;
        }
    }
}
