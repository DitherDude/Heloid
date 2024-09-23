using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Math : Command
    {
        public Math(string name = "math") : base(name) { }
        public override string Execute(string[] args1)
        {
            int b1 = 0;
            int b2 = 0;
            string op;
            bool OK = false;
            string argsline = "";
            foreach (string arg in args1)
            {
                string argx = "";
                if (arg.Contains("+"))
                {
                    argx = arg.Replace("+", " + ");
                }
                else if (arg.Contains("-"))
                {
                    argx = arg.Replace("-", " - ");
                }
                else if (arg.Contains("*"))
                {
                    argx = arg.Replace("*", " * ");
                }
                else if (arg.Contains("/"))
                {
                    argx = arg.Replace("/", " / ");
                }
                else if (arg.Contains("^"))
                {
                    argx = arg.Replace("^", " ^ ");
                }
                if (argx != "")
                {
                    argsline += argx + " ";
                }
                else
                {
                    argsline += arg + " ";
                }
            }
            string[] argsx = argsline.Split(' ');
            string[] args = argsx.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (args.Length == 3)
            {
                try
                {
                    b1 = int.Parse(args[0]);
                    b2 = int.Parse(args[2]);
                    OK = true;
                }
                catch
                {
                    return "Invalid syntax. See HELP MATH for correct syntax.";
                }
                op = args[1].ToLower();
                if (OK)
                {
                    if (op == "add" || op == "+")
                    {
                        return (b1 + b2).ToString();
                    }
                    else if (op == "sub" || op == "subtract" || op == "-")
                    {
                        return (b1 - b2).ToString();
                    }
                    else if (op == "mul" || op == "multiply" || op == "*")
                    {
                        return (b1 * b2).ToString();
                    }
                    else if (op == "div" || op == "divide" || op == "/")
                    {
                        return (b1 / b2).ToString();
                    }
                    else if (op == "mod" || op == "modulo" || op == "rem" || op == "remainder" || op == "%")
                    {
                        return (b1 % b2).ToString();
                    }
                    else if (op == "pow" || op == "power" || op == "ind" || op == "^")
                    {
                        return (System.Math.Pow(b1, b2)).ToString();
                    }
                    else
                    {
                        return "Invalid syntax. See HELP MATH for correct syntax.";
                    }
                }
                else
                {
                    return "Invalid syntax. See HELP MATH for correct syntax.";
                }
            }
            else
            {
                return "Invalid syntax. See HELP MATH for correct syntax.";
            }
        }

        public override string Help()
        {
            string data = "   Performs an arithmetic operation on the two operands.\n";
            data += "Usage: \u001b[32mMATH\u001b[33m int1 \u001b[34m<+/-/*///%/^...>\u001b[33m int2\u001b[0m\n";
            data += "Examples: \"\u001b[32mMATH\u001b[33m 5 \u001b[34m/\u001b[33m 3\u001b[0m\" - outputs \"\u001b[33m1\u001b[0m\" (remainder of 2 is dropped).";
            return data;
        }
    }
}
