using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands.Chains
{
    public partial class Chains_old
    {
        public static string Chain(string command, string inputraw)
        {
            string output = "";
            string input = "";
            string[] inputs = command.Split(' ');
            int count = 0;
            foreach (string s in inputs)
            {
                if (count != 0)
                {
                    input += s + " ";
                }
                count++;
            }
            switch (inputs[0].ToLower())
            {
                case "length":
                    output = Length(inputraw);
                    break;
                default:
                    output = "Error: Chain doesn't exist!";
                    break;
            }
            return output;
        }
    }
}
