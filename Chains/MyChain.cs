using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class MyChain : Chain
    {
        public MyChain(string name = "mychain") : base(name) { }
        public override string Execute(string[] args, string prevcmdout)
        {
            //Do something
            bool argsnull = true;
            foreach (string arg in args)
            {
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    argsnull = false;
                }
            }
            if (args.Count() == 0 || argsnull)
            {
                //Def. do something!
            }
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
