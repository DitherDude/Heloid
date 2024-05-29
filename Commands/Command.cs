using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class Command
    {
        public readonly string Name;

        public Command(string name) { Name = name; }

        public virtual string Execute(string[] args) { return ""; }

        public virtual string Help() { return ""; }

        public virtual string cmd() { return ""; }
    }
}
