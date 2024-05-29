using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heloid
{
    public static class Ext
    {
        public static string HelpCmd(string command)
        {
            return $"\u001b[32mHELP\u001b[33m {command.ToUpper()}\u001b[0m";
        }
        public static string Error(string error, string errorMessage, string command)
        {
            return $"{error}: {errorMessage} See {HelpCmd(command)} for command usage.";
        }
        public static string BugReport()
        {
            return "Internal Error: Something went wrong, but we are not sure why.\n" +
                "Please notify the developer/file a bug report, including all steps taken to reproduce this issue.\n";
        }
        public static void Logo()
        {
            string logo = " \u001b[31m__  __     \u001b[33m______     \u001b[32m__         \u001b[36m______     \u001b[34m__     \u001b[35m_____\u001b[0m";
            Console.WriteLine(logo);
            Env.Log(logo, showDateTime: false, trim: false);
            logo = "\u001b[31m/\\ \\_\\ \\   \u001b[33m/\\  ___\\   \u001b[32m/\\ \\       \u001b[36m/\\  __ \\   \u001b[34m/\\ \\   \u001b[35m/\\  __-.\u001b[0m";
            Console.WriteLine(logo);
            Env.Log(logo, showDateTime: false, trim: false);
            logo = "\u001b[31m\\ \\  __ \\  \u001b[33m\\ \\  __\\   \u001b[32m\\ \\ \\____  \u001b[36m\\ \\ \\/\\ \\  \u001b[34m\\ \\ \\  \u001b[35m\\ \\ \\/\\ \\\u001b[0m";
            Console.WriteLine(logo);
            Env.Log(logo, showDateTime: false, trim: false);
            logo = " \u001b[31m\\ \\_\\ \\_\\  \u001b[33m\\ \\_____\\  \u001b[32m\\ \\_____\\  \u001b[36m\\ \\_____\\  \u001b[34m\\ \\_\\  \u001b[35m\\ \\____-\u001b[0m";
            Console.WriteLine(logo);
            Env.Log(logo, showDateTime: false, trim: false);
            logo = "  \u001b[31m\\/_/\\/_/   \u001b[33m\\/_____/   \u001b[32m\\/_____/   \u001b[36m\\/_____/   \u001b[34m\\/_/   \u001b[35m\\/____/\u001b[0m";
            Console.WriteLine(logo);
            Env.Log(logo + "\n", showDateTime: false, trim: false);
        }
    }
}
