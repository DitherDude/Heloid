using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Logic : Command
    {
        public Logic(string name = "logic") : base(name) { }
        public override string Execute(string[] args)
        {
            bool b1 = true;
            bool b2 = true;
            string op;
            bool OK = false;
            if (args.Length == 3)
            {
                if (args[0].ToLower() == "true" || args[0] == "1")
                {
                    OK = true;
                    b1 = true;
                }
                else if (args[0].ToLower() == "false" || args[0] == "0")
                {
                    OK = true;
                    b1 = false;
                }
                if (args[2].ToLower() == "true" || args[2] == "1")
                {
                    OK = true;
                    b2 = true;
                }
                else if (args[2].ToLower() == "false" || args[2] == "0")
                {
                    OK = true;
                    b2 = false;
                }
                if (OK)
                {
                    if (args[1].ToLower() == "and")
                    {
                        return (b1 && b2).ToString();
                    }
                    else if (args[1].ToLower() == "or")
                    {
                        return (b1 || b2).ToString();
                    }
                    else if (args[1].ToLower() == "xor")
                    {
                        return (b1 ^ b2).ToString();
                    }
                    else if (args[1].ToLower() == "nand")
                    {
                        return (!(b1 && b2)).ToString();
                    }
                    else if (args[1].ToLower() == "nor")
                    {
                        return (!(b1 || b2)).ToString();
                    }
                    else if (args[1].ToLower() == "xnor")
                    {
                        return (!(b1 ^ b2)).ToString();
                    }
                    else
                    {
                        return "Invalid syntax. See HELP LOGIC for correct syntax.";
                    }
                }
                else
                {
                    return "Invalid syntax. See HELP LOGIC for correct syntax.";
                }
            }
            else
            {
                return "Invalid syntax. See HELP LOGIC for correct syntax.";
            }
            return null;
        }

        public override string Help()
        {
            string data = "   Performs a logical operation on the two operands.\n";
            data += "Usage: \u001b[32mLOGIC\u001b[33m bool1 \u001b[34m<AND/OR/XOR/NAND/NOR/XNOR>\u001b[33m bool2\u001b[0m\n";
            data += "Examples: \"\u001b[32mLOGIC\u001b[33m 0 \u001b[34mXOR\u001b[33m 1\u001b[0m\" - outputs \"\u001b[33m1\u001b[0m\".";
            return data;
        }
    }
}
