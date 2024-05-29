using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class MyChain : Command
    {
        public MyChain(string name = "mycommand") : base(name) { }
        public override string Execute(string[] args)
        {
            //Do something
            return null;
        }

        public override string Help()
        {
            string data = "   A base command to build others on.\n";
            data += "Usage: \u001b[32mMYCOMMAND\u001b[33m subcommand\u001b[0m\n";
            data += "Examples: \"\u001b[32mMYCOMMAND\u001b[33m subcommand\u001b[0m\" - does nothing.";
            return data;
        }
    }
}
