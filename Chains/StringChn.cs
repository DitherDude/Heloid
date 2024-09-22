using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class StringChn : Chain
    {
        public StringChn(string name = "string") : base(name) { }
        public override string Execute(string[] args, string prevcmdout)
        {
            //Do something
            bool OK = false;
            if (args.Count() == 1)
            {
                for (int i = 0; i < Env.table.Rows.Count; i++)
                {
                    if (Env.table.Rows[i][0].ToString() == args[0])
                    {
                        if (Env.table.Rows[i][1].ToString() == "string")
                        {
                            OK = true;
                            Env.table.Rows[i][2] = prevcmdout.Trim();
                        }
                    }
                }
                if (!OK)
                {
                    return $"String variable not found: \"{args[0]}\"";
                }
                else
                {
                    return $"Set variable \"{args[0]}\" to \"{prevcmdout.Trim()}\"";
                }

            }
            else
            {
                return "Syntax Error. See CHELP STRING for correct syntax.";
            }

        }

        public override string Help()
        {
            string data = "   Sets the data of a string variable to the output of the previous command.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mSTRING\u001b[33m stringName\u001b[0m\n";
            data += "Examples: \"\u001b[34mECHO HELLO, WORLD! \u001b[35m| \u001b[32mSTRING\u001b[33m str\u001b[0m\"\n";
            data += "- If the string variable \"\u001b[33m str\u001b[0m\" exists, it will be set to \"\u001b[34mHello, World!\u001b[0m\".";
            return data;
        }
    }
}
