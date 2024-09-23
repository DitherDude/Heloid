using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class DoubleCmd : Command
    {
        public DoubleCmd(string name = "double") : base(name) { }
        public override string Execute(string[] argsraw)
        {
            int cnt = 0;
            string argscollected = "";
            foreach (string arg in argsraw)
            {
                argscollected += arg + " ";
            }
            argscollected = argscollected.Trim();
            if (argscollected.Contains("=") &&
                !argscollected.Contains(" = "))
            {
                argscollected = argscollected.Replace("=", " = ");
            }
            string[] args = argscollected.Split(' ');
            string vname = "";
            string vdata = "";
            bool OK = false;
            cnt = 0;
            if (args.Length < 3)
            {
                return "Too few arguments: see HELP DOUBLE for correct syntax.";
            }
            foreach (string arg in args)
            {
                if (cnt == 0)
                {
                    vname = arg;
                }
                else if (cnt == 1)
                {
                    if (arg.Trim() == "=")
                    {
                        OK = true;
                    }
                }
                else
                {
                    if (OK)
                    {
                        vdata += arg + " ";
                    }
                }
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    cnt++;
                }
            }
            if (vname.Contains("%"))
            {
                OK = false;
            }
            if (OK)
            {
                for (int i = 0; i < Env.table.Rows.Count; i++)
                {
                    if (Env.table.Rows[i][0].ToString() == vname)
                    {
                        OK = false;
                    }
                }
                if (OK)
                {
                    vdata = vdata.Trim();
                    try
                    {
                        if (double.Parse(vdata).ToString() == vdata)
                        {
                            Env.table.Rows.Add(vname, "double", vdata);
                            return $"Added double variable \"{vname}\" with value \"{vdata}\"";
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
                else
                {
                    return $"Variable \"{vname}\" already exists!";
                }
            }
            else
            {
                return "Invalid syntax: see HELP DOUBLE for correct syntax.";
            }
        }

        public override string Help()
        {
            string data = "   A command to add a double variable.\n";
            data += "Usage: \u001b[32mDOUBLE\u001b[33m doubleName\u001b[0m = \u001b[34mdoubleValue\u001b[0m\n";
            data += "Examples: \"\u001b[32mDOUBLE \u001b[33mdb\u001b[0m = \u001b[34m0.125\u001b[0m\" - adds a variable \"\u001b[33m db\u001b[0m\" with value \"\u001b[34m0.125\u001b[0m\"\n";
            data += "Note: the \u001b[33m doubleName\u001b[31m CANNOT \u001b[0mcontain a\u001b[36m %\u001b[0m character.";
            return data;
        }
    }
}
