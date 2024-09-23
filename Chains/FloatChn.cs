using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class FloatChn : Chain
    {
        public FloatChn(string name = "float") : base(name) { }
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
                        if (Env.table.Rows[i][1].ToString() == "float")
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
                                if (float.Parse(vdata).ToString() == vdata)
                                {
                                    Env.table.Rows[i][2] = prevcmdout.Trim();
                                    OK = true;
                                }
                                else
                                {
                                    return $"\"{vdata}\" is not a float!";
                                }
                            }
                            catch
                            {
                                return $"\"{vdata}\" is not a float!\n" +
                                    $"(double cannot exceed +/-3.402823E+38).";
                            }
                        }
                    }
                }
                if (!OK)
                {
                    return $"Float variable not found: \"{args[0]}\"";
                }
                else
                {
                    return $"Set variable \"{args[0]}\" to \"{vdata}\"";
                }

            }
            else
            {
                return "Syntax Error. See CHELP FLOAT for correct syntax.";
            }

        }

        public override string Help()
        {
            string data = "   Sets the data of a float variable to the output of the previous command.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mFLOAT\u001b[33m floatName\u001b[0m\n";
            data += "Examples: \"\u001b[34mECHO 1.25 \u001b[35m| \u001b[32mFLOAT\u001b[33m f\u001b[0m\"\n";
            data += "- If the float variable \"\u001b[33m f\u001b[0m\" exists, it will be set to \"\u001b[34m1.25\u001b[0m\".";
            return data;
        }
    }
}
