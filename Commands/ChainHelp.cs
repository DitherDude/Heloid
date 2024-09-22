using Heloid.Chains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class ChainHelpCommand : Command
    {
        public ChainHelpCommand(string name = "chelp") : base(name) { }
        public override string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                string help = "";
                foreach (Chain c in ChainManager.chains.OrderBy(o => o.Name))
                {
                    help += "\u001b[34m" + c.Name.ToUpper() + "\u001b[0m:\n";
                    help += c.Help() + "\n\n";
                }
                return help;
            }
            if (args.Length == 1)
            {
                if (ChainManager.ChainExists(args[0].ToLower()))
                {
                    Chain c = ChainManager.chains.Find(chain => chain.Name.Equals(args[0], StringComparison.OrdinalIgnoreCase));
                    return "\u001b[34m" + c.Name.ToUpper() + "\u001b[0m:\n" + c.Help() + "\n\n";
                }
                else
                {
                    return Ext.Error("Syntax Error", "That chain doesn't exist.", Name);
                }
            }
            return Ext.Error("Arg Overflow Error", "Too many arguments.", Name);
        }

        public override string Help()
        {
            string data = "   Provides help information for Heloid chains.\n";
            data += "Usage: \u001b[32mCHELP\u001b[33m [commandName]\u001b[0m\n";
            data += "Examples: \"\u001b[32mCHELP\u001b[0m\" - returns all chain help information.\n";
            data += "Examples: \"\u001b[32mCHELP\u001b[33m LENGTH\u001b[0m\" - returns help information regarding the LENGTH chain.";
            return data;
        }
    }
}
