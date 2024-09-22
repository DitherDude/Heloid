using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class BoolChn : Chain
    {
        public BoolChn(string name = "bool") : base(name) { }
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
                        if (Env.table.Rows[i][1].ToString() == "bool")
                        {
                            string data = prevcmdout.Trim();
                            if (data.ToLower() == "true" || data == "1")
                            {
                                prevcmdout = "1";
                                OK = true;
                                Env.table.Rows[i][2] = "1";
                            }
                            else if (data.ToLower() == "false" || data == "0")
                            {
                                prevcmdout = "0";
                                OK = true;
                                Env.table.Rows[i][2] = "0";
                            }
                            else
                            {
                                OK = false;
                            }
                        }
                    }
                }
                if (!OK)
                {
                    return $"Boolean variable not found: \"{args[0]}\"";
                }
                else
                {
                    return $"Set variable \"{args[0]}\" to \"{prevcmdout.Trim()}\"";
                }

            }
            else
            {
                return "Syntax Error. See CHELP BOOL for correct syntax.";
            }

        }

        public override string Help()
        {
            string data = "   Sets the data of a boolean variable to the output of the previous command.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mBOOL\u001b[33m boolName\u001b[0m\n";
            data += "Examples: \"\u001b[34mECHO TRUE \u001b[35m| \u001b[32mBOOL\u001b[33m bool\u001b[0m\"\n";
            data += "- If the boolean variable \"\u001b[33m bool\u001b[0m\" exists, it will be set to \"\u001b[34m1\u001b[0m\".";
            return data;
        }
    }
}
