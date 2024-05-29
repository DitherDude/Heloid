using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Echo : Command
    {
        public Echo(string name = "echo") : base(name) { }
        public override string Execute(string[] args)
        {
            string temp = "";
            foreach (string arg in args)
            {
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
