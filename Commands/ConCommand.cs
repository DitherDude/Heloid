using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Con : Command
    {
        public Con(string name = "con") : base(name) { }
        public override string Execute(string[] args)
        {
            string temp = "";
            foreach (string arg in args)
            {
                temp += arg + " ";
            }
            Console.WriteLine(temp);
            return null;
        }

        public override string Help()
        {
            string data = "   Echoes inputted text to CLI.\n";
            data += "Usage: \u001b[32mCON\u001b[33m text to echo\u001b[0m\n";
            data += "Examples: \"\u001b[32mCON\u001b[33m Hello, World!\u001b[0m\" - Echoes \"Hello, World!\" to the CLI.\n";
            data += "\u001b[31mWARNING:\u001b[35mIf command was executed via the init.hax script,\n" +
                "it \u001b[31mWILL ONLY\u001b[35m be echoed to the CLI. See \u001b[32mHELP\u001b[33m ECHO\u001b[35m to echo according to execution method.\u001b[0m";
            return data;
        }
    }
}
