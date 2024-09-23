using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class IntChn : Chain
    {
        public IntChn(string name = "int") : base(name) { }
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
                        if (Env.table.Rows[i][1].ToString() == "int")
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
                                if (int.Parse(vdata).ToString() == vdata)
                                {
                                    Env.table.Rows[i][2] = prevcmdout.Trim();
                                    OK = true;
                                }
                                else
                                {
                                    return $"\"{vdata}\" is not an integer!";
                                }
                            }
                            catch
                            {
                                return $"\"{vdata}\" is not an integer!\n" +
                                    $"(int cannot exceed +/-2,147,483,647).";
                            }
                        }
                    }
                }
                if (!OK)
                {
                    return $"Integer variable not found: \"{args[0]}\"";
                }
                else
                {
                    return $"Set variable \"{args[0]}\" to \"{vdata}\"";
                }

            }
            else
            {
                return "Syntax Error. See CHELP INT for correct syntax.";
            }

        }

        public override string Help()
        {
            string data = "   Sets the data of an integer variable to the output of the previous command.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mINT\u001b[33m integerName\u001b[0m\n";
            data += "Examples: \"\u001b[34mECHO 12345 \u001b[35m| \u001b[32mINT\u001b[33m i\u001b[0m\"\n";
            data += "- If the integer variable \"\u001b[33m i\u001b[0m\" exists, it will be set to \"\u001b[34m12345\u001b[0m\".";
            return data;
        }
    }
}
