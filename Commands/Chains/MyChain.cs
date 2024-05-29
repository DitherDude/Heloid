using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands.Chains
{
    public class MyChain : Chain
    {
        public MyChain(string name = "mychain") : base(name) { }
        public override string Execute(string[] args, string prevcmdout)
        {
            //Do something
            return "Something stupid happened. Command output:\n" + prevcmdout;
        }

        public override string Help()
        {
            string data = "   A base chain to build others on.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mMYCOMMAND\u001b[33m subcommand\u001b[0m\n";
            data += "Examples: \"\u001b[34mBASECOMMAND \u001b[35m| \u001b[32mMYCOMMAND\u001b[33m subcommand\u001b[0m\" - does nothing.";
            return data;
        }
    }
}
