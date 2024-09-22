using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class BoolCmd : Command
    {
        public BoolCmd(string name = "bool") : base(name) { }
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
            if (args.Length != 3)
            {
                return "Incorrect amount of arguments: see HELP BOOL for correct syntax.";
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
                        vdata = arg.Trim();
                        if (vdata.ToLower() == "true" || vdata == "1")
                        {
                            vdata = "1";
                            OK = true;
                        }
                        else if (vdata.ToLower() == "false" || vdata == "0")
                        {
                            vdata = "0";
                            OK = true;
                        }
                        else
                        {
                            OK = false;
                        }
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
                    Env.table.Rows.Add(vname, "bool", vdata);
                    return $"Added boolean variable \"{vname}\" with value \"{vdata}\"";
                }
                else
                {
                    return $"Variable \"{vname}\" already exists!";
                }
            }
            else
            {
                return "Invalid syntax: see HELP BOOL for correct syntax.";
            }
        }

        public override string Help()
        {
            string data = "   A command to add a bool variable.\n";
            data += "Usage: \u001b[32mBOOL\u001b[33m boolName\u001b[0m = \u001b[34m<true/false/0/1>\u001b[0m\n";
            data += "Examples: \"\u001b[32mBOOL \u001b[33mbool\u001b[0m = \u001b[34mtrue\u001b[0m\" - adds a boolean variable \"\u001b[33mbool\u001b[0m\" with value \"\u001b[34m1\u001b[0m\"\n";
            data += "Note: the \u001b[33m boolName\u001b[31m CANNOT \u001b[0mcontain a\u001b[36m %\u001b[0m character.";
            return data;
        }
    }
}
