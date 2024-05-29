using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class HelpCommand : Command
    {
        public HelpCommand(string name = "help") : base(name) { }
        public override string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                string help = "";
                foreach (Command c in CommandManager.commands.OrderBy(o => o.Name))
                {
                    help += "\u001b[34m" + c.Name.ToUpper() + "\u001b[0m:\n";
                    help += c.Help() + "\n\n";
                }
                return help;
            }
            if (args.Length == 1)
            {
                if (CommandManager.CommandExists(args[0].ToLower()))
                {
                    Command c = CommandManager.commands.Find(command => command.Name.Equals(args[0], StringComparison.OrdinalIgnoreCase));
                    return "\u001b[34m" + c.Name.ToUpper() + "\u001b[0m:\n" + c.Help() + "\n\n";
                }
                else
                {
                    return Ext.Error("Syntax Error", "That command doesn't exist.", Name);
                }
            }
            return Ext.Error("Arg Overflow Error", "Too many arguments.", Name);
        }

        public override string Help()
        {
            string data = "   Provides help information for Heloid commands.\n";
            data += "Usage: \u001b[32mHELP\u001b[0m\n";
            data += "Examples: \"\u001b[32mHELP\u001b[0m\" - returns all help information.";
            return data;
        }
    }
}
