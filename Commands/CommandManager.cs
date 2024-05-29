using Heloid.Commands.Chains;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class CommandManager
    {
        static private ChainManager chainManager = new ChainManager();
        public static List<Command> commands = new List<Command>
        {
            new HelpCommand(),
            new WorkingDir(),
            new Shutdown(),
            new Echo(),
            new Con(),
        };

        public CommandManager()
        {
            //commands = [];
        }
        public string ProcessInput(string rawinput)
        {
            string input = rawinput.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                return "\r";
            }
            if (Env.unsafecode == true)
            {
                Env.Log($"Received minor Command \"{input}\"... (unsafe)", logAll: true);
            }
            else
            {
                Env.Log($"Received minor Command \"{input}\"...", logAll: true);
            }
            string[] split = input.Split(' ');
            string command = split[0].ToLower();
            List<string> args = new List<string>();

            int count = 0;
            int procType = 0;
            string proc = "";
            foreach (string s in split)
            {
                if (s == ">>")
                {
                    procType = 1;
                }
                if (s == ">")
                {
                    procType = 2;
                }
                if (s == "|")
                {
                    if (procType == 1 || procType == 2)
                    {
                        return "Cannot chain after a >> or >.";
                    }
                    procType = 3;
                }
                if (procType == 0)
                {
                    if (count != 0)
                    {
                        args.Add(s);
                    }
                    count++;
                }
                else
                {
                    proc += s + " ";
                }
            }

            foreach (Command cmd in commands)
            {
                if (cmd.Name == command)
                {
                    if (procType == 0)
                    {
                        return cmd.Execute(args.ToArray()) + "\n";
                    }
                    else
                    {
                        string output = cmd.Execute(args.ToArray()).Trim();
                        int part = 0;
                        bool done = false;
                        string[] cmds = proc.Split(" ");
                        while (!done)
                        {
                            if (cmds[part + 0] == ">>")
                            {
                                done = true;
                                try
                                {
                                    StreamWriter sr = new StreamWriter(cmds[part + 1], append: true);
                                    sr.WriteLine(output);
                                    sr.Close();
                                    return "";
                                }
                                catch (Exception ex)
                                {
                                    Env.Log(ex.ToString(), logAll: true);
                                    return "Error. Check log for details.";
                                }
                            }
                            else if (cmds[part + 0] == ">")
                            {
                                done = true;
                                try
                                {
                                    StreamWriter sr = new StreamWriter(cmds[part + 1], append: false);
                                    sr.WriteLine(output);
                                    sr.Close();
                                    return "";
                                }
                                catch (Exception ex)
                                {
                                    Env.Log(ex.ToString(), logAll: true);
                                    return "Error. Check log for details.";
                                }
                            }
                            else if (cmds[part + 0] == "|")
                            {
                                string bitstring = "";
                                int temppart = 0;
                                foreach (string rawbit in cmds)
                                {
                                    string bit = rawbit.Trim();
                                    if (temppart == cmds.Length - 1)
                                    {
                                        done = true;
                                        continue;
                                    }

                                    if (/*temppart != 0*/true)
                                    {
                                        if (bit != "|" && bit != ">>" && bit != ">")
                                        {
                                            bitstring += bit + " ";
                                        }
                                        else if (bit == "|")
                                        {
                                        }
                                        else if (bit == ">>" || bit == ">")
                                        {
                                            continue;
                                        }
                                    }
                                    temppart++;
                                }
                                //output = Chains.Chains.Chain(bitstring, output);
                                output = chainManager.ProcessInput(bitstring, output);
                                part = temppart;
                            }
                        }
                        return output;
                    }
                }
            }
            return $"\u001b[93mCommand \'\u001b[31m{command}\u001b[93m\' couldn't be found. Are you sure it exists?\u001b[0m\n\n";
            //return Ext.BugReport();


        }

        public static bool CommandExists(string input)
        {
            return commands.Any(command => command.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
        }
    }
}
