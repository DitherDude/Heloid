using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands.Chains
{
    public class Chain
    {
        public readonly string Name;

        public Chain(string name) { Name = name; }

        public virtual string Execute(string[] args, string prevcmdout) { return ""; }

        public virtual string Help() { return ""; }

        public virtual string cmd() { return ""; }
    }
}
