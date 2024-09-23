using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class FloatCmd : Command
    {
        public FloatCmd(string name = "float") : base(name) { }
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
                return "Too few arguments: see HELP FLOAT for correct syntax.";
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
                        if (float.Parse(vdata).ToString() == vdata)
                        {
                            Env.table.Rows.Add(vname, "float", vdata);
                            return $"Added float variable \"{vname}\" with value \"{vdata}\"";
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
                else
                {
                    return $"Variable \"{vname}\" already exists!";
                }
            }
            else
            {
                return "Invalid syntax: see HELP FLOAT for correct syntax.";
            }
        }

        public override string Help()
        {
            string data = "   A command to add a float variable.\n";
            data += "Usage: \u001b[32mFLOAT\u001b[33m floatName\u001b[0m = \u001b[34mfloatValue\u001b[0m\n";
            data += "Examples: \"\u001b[32mFLOAT \u001b[33mf\u001b[0m = \u001b[34m2.75\u001b[0m\" - adds a variable \"\u001b[33m f\u001b[0m\" with value \"\u001b[34m2.75\u001b[0m\"\n";
            data += "Note: the \u001b[33m floatName\u001b[31m CANNOT \u001b[0mcontain a\u001b[36m %\u001b[0m character.";
            return data;
        }
    }
}
