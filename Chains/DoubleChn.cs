using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class DoubleChn : Chain
    {
        public DoubleChn(string name = "double") : base(name) { }
        public override string Execute(string[] args, string prevcmdout)
        {
            //Do something
            bool OK = false;
            string vdata = "";
            if (args.Count() == 1)
            {
                for (int i = 0; i < Env.table.Rows.Count; i++)
                {
                    if (Env.table.Rows[i][0].ToString() == args[0])
                    {
                        if (Env.table.Rows[i][1].ToString() == "double")
                        {
                            OK = false;
                            vdata = prevcmdout.Trim();
                            if (vdata.ToLower() == "true")
                            {
                                vdata = "1";
                            }
                            if (vdata.ToLower() == "false")
                            {
                                vdata = "0";
                            }
                            try
                            {
                                if (double.Parse(vdata).ToString() == vdata)
                                {
                                    Env.table.Rows[i][2] = prevcmdout.Trim();
                                    OK = true;
                                }
                                else
                                {
                                    return $"\"{vdata}\" is not a double!";
                                }
                            }
                            catch
                            {
                                return $"\"{vdata}\" is not a double!\n" +
                                    $"(double cannot exceed +/-1.7976931348623157E+308).";
                            }
                        }
                    }
                }
                if (!OK)
                {
                    return $"Double variable not found: \"{args[0]}\"";
                }
                else
                {
                    return $"Set variable \"{args[0]}\" to \"{vdata}\"";
                }

            }
            else
            {
                return "Syntax Error. See CHELP DOUBLE for correct syntax.";
            }

        }

        public override string Help()
        {
            string data = "   Sets the data of a double variable to the output of the previous command.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mDOUBLE\u001b[33m doubleName\u001b[0m\n";
            data += "Examples: \"\u001b[34mECHO 0.5 \u001b[35m| \u001b[32mDOUBLE\u001b[33m db\u001b[0m\"\n";
            data += "- If the double variable \"\u001b[33m db\u001b[0m\" exists, it will be set to \"\u001b[34m0.5\u001b[0m\".";
            return data;
        }
    }
}
