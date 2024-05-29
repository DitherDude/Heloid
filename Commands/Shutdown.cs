using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Shutdown : Command
    {
        public Shutdown(string name = "shutdown") : base(name) { }
        public override string Execute(string[] args)
        {
            if (args.Length > 0)
            {
                return "Ignoring superfluous arguments...";
            }
            Env.ShutDown(0);
            return null;
        }

        public override string Help()
        {
            string data = "   Sends a shutdown signal.\n";
            data += "Usage: \u001b[32mSHUTDOWN\\u001b[0m\n";
            data += "Examples: \"\u001b[32mSHUTDOWN\u001b[0m\" - Shuts down this machine.";
            return data;
        }
    }
}
