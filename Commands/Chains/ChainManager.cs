using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands.Chains
{
    public class ChainManager
    {
        public static List<Chain> chains = new List<Chain>
        {
            new Length(),
        };

        public ChainManager()
        {
            //chains = [];
        }

        public string ProcessInput(string chainraw, string inputraw)
        {
            string input = chainraw.Trim();
            string[] split = input.Split(' ');
            string chain = split[0].ToLower();
            List<string> args = new List<string>();
            int count = 0;
            foreach (string s in split)
            {
                if (count != 0)
                {
                    args.Add(s);
                }
                count++;
            }

            foreach (Chain chn in chains)
            {
                if (chn.Name == chain)
                {
                    return chn.Execute(args.ToArray(), inputraw) + "\n";
                }
            }
            return $"\u001b[93mChain Command \'\u001b[31m{chain}\u001b[93m\' couldn't be found. Are you sure it exists?\u001b[0m\n\n";
        }

        public static bool ChainExists(string input)
        {
            return chains.Any(chain => chain.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
        }
    }
}
