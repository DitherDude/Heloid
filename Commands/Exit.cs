using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Exit : Command
    {
        public Exit(string name = "exit") : base(name) { }
        public override string Execute(string[] args)
        {
            if (args.Length > 0)
            {
                return "Ignoring superfluous arguments...";
            }
            if (Env.unsafecode)
            {
                Environment.Exit(0);
            }
            else
            {
                return "Not allowed in safe mode.";
            }
            return null;
        }

        public override string Help()
        {
            string data = "   Closes the program window (unsafe).\n";
            data += "Usage: \u001b[32m!EXIT\\u001b[0m\n";
            data += "Examples: \"\u001b[32m!EXIT\u001b[0m\" - Exits the program.";
            return data;
        }
    }
}
