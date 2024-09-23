using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Clear : Command
    {
        public Clear(string name = "clear") : base(name) { }
        public override string Execute(string[] args)
        {
            if (args.Length > 0)
            {
                return "Ignoring superfluous arguments...";
            }
            Console.Clear();
            return null;
        }

        public override string Help()
        {
            string data = "   Clears the console screen.\n";
            data += "Usage: \u001b[32mCLEAR\u001b[0m\n";
            data += "Examples: \"\u001b[32mCLEAR\u001b[0m\" - Clears the console screen.";
            return data;
        }
    }
}
