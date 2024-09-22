using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Heloid.Commands
{
    public class Echo : Command
    {
        public Echo(string name = "echo") : base(name) { }
        public override string Execute(string[] args)
        {
            bool OK = false;
            string temp = "";

            foreach (string arg in args)
            {
                //if (arg.StartsWith("%") && !arg.StartsWith("%%") &&
                //    arg.EndsWith("%") && !arg.EndsWith("%%"))
                //{
                //    string arg1 = arg.Remove(0, 1);
                //    arg1 = arg1.Remove(arg1.Length - 1, 1);
                //    for (int i = 0; i < Env.table.Rows.Count; i++)
                //    {
                //        if (Env.table.Rows[i][0].ToString() == arg1)
                //        {
                //            OK = true;
                //            temp += Env.table.Rows[i][2].ToString() + " ";
                //        }
                //    }
                //    if (!OK)
                //    {
                //        return $"Variable not found: {arg1}";
                //    }
                //}
                //else if (arg.StartsWith("%%") && arg.EndsWith("%%"))
                //{
                //    string arg1 = arg.Remove(0, 1);
                //    arg1 = arg1.Remove(arg1.Length - 1, 1);
                //    temp += arg1 + " ";
                //}
                //else
                //{
                //    temp += arg + " ";
                //}
                temp += arg + " ";
            }
            return temp;
        }

        public override string Help()
        {
            string data = "   Echoes inputted text.\n";
            data += "Usage: \u001b[32mECHO\u001b[33m text to echo\u001b[0m\n";
            data += "Examples: \"\u001b[32mECHO\u001b[33m Hello, World!\u001b[0m\" - Echoes \"Hello, World!\".\n";
            data += "\u001b[31mWARNING:\u001b[35mIf command was executed via the init.hax script,\n" +
                "it \u001b[31mWILL NOT\u001b[35m be echoed to the CLI. See \u001b[32mHELP\u001b[33m CON\u001b[35m for echoing to the CLI.\u001b[0m";
            return data;
        }
    }
}
