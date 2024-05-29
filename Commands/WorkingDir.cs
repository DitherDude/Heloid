using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class WorkingDir : Command
    {
        public WorkingDir(string name = "workingdir") : base(name) { }
        public override string Execute(string[] args)
        {
            if (args.Length != 1)
            {
                if (args.Length == 0)
                {
                    return Ext.Error("Arg Count Error", "Missing args.", Name);
                }
                return Ext.Error("Arg Overflow Error", "Too many arguments.", Name);
            }
            string arg = args[0].ToString();
            if (arg.StartsWith(":\\"))
            {
                Env.Log("args[0] starts with a \':\'!");
                arg = arg.Remove(0, 2);
                arg = Env.workingDrive + arg;
                Env.Log($"Argument changed to {arg}");
                Env.Log($"Attempting to create folder at {arg}...");
                if (Directory.Exists(arg))
                {
                    Env.Log($"Folder {arg} already exists! Trying {arg}.001...");
                    bool settled = false;
                    int i = 1;
                    while (!settled)
                    {
                        string loc = arg + "." + i.ToString("000");
                        if (Directory.Exists(loc))
                        {
                            Env.Log($"Folder {loc} already exists! Trying {arg + "." + (i + 1).ToString("000")}...");
                            i++;
                        }
                        else
                        {
                            Directory.CreateDirectory(loc);
                            Env.workingDir = loc;
                            Env.logLoc = loc + "\\session.log";
                            settled = true;
                            Env.Log($"Folder {loc} seems good to me!");
                            return $"Using folder \u001b[35m{loc}\u001b[0m.";
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(arg);
                    Env.workingDir = arg;
                    Env.logLoc = arg + "\\session.log";
                    Env.Log($"Folder {arg} seems good to me!");
                    return $"Using folder \u001b[35m{arg}\u001b[0m.";
                }
            }
            else if (DriveInfo.GetDrives().Any(d => d.Name.Equals(args[0].Substring(0, args[0].IndexOf('\\') + 1), StringComparison.InvariantCultureIgnoreCase)))
            {
                string workingDrive = args[0].Substring(0, args[0].IndexOf('\\') + 1);
                arg = arg.Remove(0, 3);
                Env.Log($"Drive {workingDrive} exists!");
                Env.workingDrive = workingDrive;
                arg = workingDrive + arg;
                Env.Log($"Attempting to create folder at {arg}...");
                if (Directory.Exists(arg))
                {
                    Env.Log($"Folder {arg} already exists! Trying {arg}.001...");
                    bool settled = false;
                    int i = 1;
                    while (!settled)
                    {
                        string loc = arg + "." + i.ToString("000");
                        if (Directory.Exists(loc))
                        {
                            Env.Log($"Folder {loc} already exists! Trying {arg + "." + (i + 1).ToString("000")}...");
                            i++;
                        }
                        else
                        {
                            Directory.CreateDirectory(loc);
                            Env.workingDir = loc;
                            Env.logLoc = loc + "\\session.log";
                            settled = true;
                            Env.Log($"Folder {loc} seems good to me!");
                            return $"Using folder \u001b[35m{loc}\u001b[0m.";
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(arg);
                    Env.workingDir = arg;
                    Env.logLoc = arg + "\\session.log";
                    Env.Log($"Folder {arg} seems good to me!");
                    return $"Using folder \u001b[35m{arg}\u001b[0m.";
                }
            }
            Console.WriteLine(Ext.Error("\u001b[31mSystem.IO Error", "Something went wrong making the folder. Check read/write permissions, and if the specified drive exists/is available.\u001b[0m", Name));
            return Ext.Error("System.IO Error", "Something went wrong making the folder. Check read/write permissions, and if the specified drive exists/is available.", Name);
        }

        public override string Help()
        {
            string data = "   Defines where all logged output should be stored.\n";
            data += "Usage: \u001b[32mWORKINGDIR\u001b[33m Target\u001b[0m\n";
            data += "Examples: \"\u001b[32mWORKINGDIR\u001b[33m C:\\Folder1\u001b[0m\" - will save all configuration into C:\\Folder1.\n";
            data += "\"\u001b[32mWORKINGDIR\u001b[33m :\\Folder1\u001b[0m\" - will save all configuration into {drive containing init file}\\Folder1.\n";
            data += "\u001b[35mIf the folder already exists, a \u001b[33m.001\u001b[35m will be added to the end of the folder name, then \u001b[33m.002\u001b[35m, and so on.\u001b[0m";
            return data;
        }
    }
}
