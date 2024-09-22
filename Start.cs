using Heloid;
using Heloid.Commands;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Heloid
{
    public static partial class Env
    {

        public static string hold = "";
        public static bool script = false;
        public static bool logInit = false;
        public static bool unsafecode = false;
        public static int lineNumber = 0;
        public static string logLoc;
        public static string workingDrive = "";
        public static string workingDir;
        public static StreamReader sr;
        static bool foundInit = false;
        static string initLoc;
        public static DataTable table = new DataTable();
        static private CommandManager commandManager = new CommandManager();

        static void Main(string[] args)
        {
            Ext.Logo();
            table.Columns.Add("varname", typeof(string));
            table.Columns.Add("vartype", typeof(string));
            table.Columns.Add("vardata", typeof(string));
            Log("Searching for initialization script...");
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (File.Exists(drive.Name + "init.hax"))
                {
                    Log($"Found script in drive {drive.Name}!");
                    if (foundInit)
                    {
                        Crash("Multiple init files!", 1);
                    }
                    foundInit = true;
                    initLoc = drive.Name + "init.hax";
                    workingDrive = drive.Name;
                }
            }
            Log("Confirmed that there is less than two init.hax files.");
            Log("Imagine the havoc that would be caused if multiple init.hax files were found across multiple drives!");
            if (!foundInit)
            {
                //Crash("Missing init file!", 2);
                Log("Missing init file!");
            }
            else
            {
                using (sr = new StreamReader(initLoc))
                {
                    Log("Running script in init.hax...");
                    script = true;
                    string lineraw;
                    while ((lineraw = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(lineraw))
                        {
                            ProcessCommand(lineraw);
                        }
                    }
                    sr.Close();
                    script = false;
                    Log("\n\n===================================================================================", showDateTime: false, trim: false);
                    Log("Reached end of init.hax file. Entering CLI mode.");
                    Log("===================================================================================\n\n", showDateTime: false, trim: false);
                }
            }
            while (true)
            {
                Console.Write("\n\u001b[35m>\u001b[0m ");
                string inputraw = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputraw))
                {
                    ProcessCommand(inputraw);
                }

            }
        }

        public static void ProcessCommand(string inputraw1)
        {
            Log("Received MAJOR Command \"" + inputraw1 + "\"...", spaceBefore: 1, logAll: true);
            string inputraw = Regex.Replace(inputraw1, @"(?<=^|[^|^])\|(?![|]|$)", " | ");
            inputraw = Regex.Replace(inputraw, @"(?<=^|[^>^])>(?![>]|$)", " > ");
            inputraw = Regex.Replace(inputraw, @"^(?!.*\^>>).*>>.*", " >> ");
            string[] inputs = Regex.Split(inputraw, @"(?<!\^)&");
            foreach (string inputraw0 in inputs)
            {
                unsafecode = false;
                string input = inputraw0.Replace("^&", "&").Trim();
                input = input.Replace("^>", ">").Trim();
                input = input.Replace("^|", "|").Trim();
                if (input.ToCharArray()[0] == '!')
                {
                    Log("Unsafe command detected! Switching to unsafe mode.");
                    unsafecode = true;
                    input = input[1..];
                }
                string response = commandManager.ProcessInput(input);
                if (script == true)
                {
                    Log(response, logAll: true, showDateTime: false);
                }
                else
                {
                    Log(response, showDateTime: false);
                    Console.Write(response);
                }
            }
        }
    }
}
