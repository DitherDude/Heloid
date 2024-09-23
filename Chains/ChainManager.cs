using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Heloid.Chains;

namespace Heloid.Chains
{
    public class ChainManager
    {
        public static List<Chain> chains = new List<Chain>
        {
            new Length(),
            new StringChn(),
            new BoolChn(),
            new IntChn(),
            new DoubleChn(),
            new FloatChn(),
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
            int procType = 0;
            string proc = "";
            foreach (string s in split)
            {
                //if (count != 0)
                //{
                //    args.Add(s);
                //}
                //count++;
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
                    if (count != 0 && !string.IsNullOrWhiteSpace(s))
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
            bool chained = false;
            foreach (Chain chn in chains)
            {
                if (chn.Name == chain)
                {
                    if (procType == 0)
                    {
                        return chn.Execute(args.ToArray(), inputraw) + "\n";
                    }
                    else
                    {
                        string output = chn.Execute(args.ToArray(), inputraw).Trim();
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
                                //output = Chains.Chains.Chain(bitstring, output);
                                output = ProcessInput(bitstring, output);
                                part = temppart;
                            }
                        }
                        return output;
                    }
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
