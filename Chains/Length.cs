using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Chains
{
    public class Length : Chain
    {
        public Length(string name = "length") : base(name) { }
        public override string Execute(string[] args, string prevcmdout)
        {
            if (args.Count() == 0)
            {
                return prevcmdout.Trim().Length.ToString();
            }
            return "Chain does not take arguments. Command output:\n" + prevcmdout;
        }

        public override string Help()
        {
            string data = "   Returns the length of the command's output.\n";
            data += "Usage: \u001b[34mBASECOMMAND \u001b[35m| \u001b[32mLENGTH\n\u001b[0m";
            data += "Examples: \"\u001b[34mECHO HELLO, WORLD! \u001b[35m| \u001b[32mLENGTH\u001b[0m\" - returns '13'.";
            return data;
        }
    }
}
