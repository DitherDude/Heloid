using Heloid.Chains;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Heloid.Commands
{
    public class CommandManager
    {
        static private ChainManager chainManager = new ChainManager();
        public static List<Command> commands = new List<Command>
        {
            new BoolCmd(),
            new ChainHelpCommand(),
            new Clear(),
            new Con(),
            new DoubleCmd(),
            new Echo(),
            new Exit(),
            new FloatCmd(),
            new HelpCommand(),
            new IntCmd(),
            new Logic(),
            new Math(),
            new Shutdown(),
            new StringCmd(),
            new WorkingDir()
        };

        public CommandManager()
        {
            //commands = [];
        }
        public string ProcessInput(string rawinput)
        {
            string input = rawinput.Trim();
            Regex regex = new Regex(@"(?<!%)%(?!\s)[^%\s]+%(?!%)");
            MatchCollection matches = regex.Matches(input);
            while (Matcher(matches, ref input) != "OK")
            {
                matches = regex.Matches(input);
            }
            input = Regex.Replace(input, @"%%{1}", "%");
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
                        // command is already ToLower();
                        if (command != "echo" && command != "con")
                        {
                            if (!string.IsNullOrWhiteSpace(s))
                            {
                                args.Add(s);
                            }
                        }
                        else
                        {
                            args.Add(s);
                        }

                    }
                    count++;
                }
                else
                {
                    proc += s + " ";
                }
            }
            bool chained = false;
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
                                            if (chained)
                                            {
                                                bitstring += bit + " ";
                                            }
                                            chained = true;
                                        }
                                        else if (bit == ">>" || bit == ">")
                                        {
                                            continue;
                                        }
                                    }
                                    temppart++;
                                }
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

        public static string Matcher(MatchCollection matches, ref string input)
        {
            foreach (Match match in matches)
            {
                bool OK = false;
                string arg1 = match.Value.Remove(0, 1);
                arg1 = arg1.Remove(arg1.Length - 1, 1);
                if (!string.IsNullOrWhiteSpace(arg1))
                {
                    for (int i = 0; i < Env.table.Rows.Count; i++)
                    {
                        if (Env.table.Rows[i][0].ToString() == arg1)
                        {
                            OK = true;
                            input = input.Remove(match.Index, match.Value.Length).Insert(match.Index, Env.table.Rows[i][2].ToString());
                            return "Yes";
                        }
                    }
                    if (!OK)
                    {
                        return $"Variable not found: {arg1}";
                    }
                }
            }
            return "OK";
        }
    }
}
