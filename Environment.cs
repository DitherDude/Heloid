using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Heloid
{
    public static partial class Env
    {
        public static void Crash(string msg, int ec)
        {
            for (int h = 0; h < 3; h++)
            {
                for (int i = 1; i < 8; i++)
                {
                    Console.Write($"\u001b[3{i}m=\u001b[0m");
                }
            }
            Console.WriteLine();
            Console.WriteLine("\u001b[31mCrash: \u001b[0m" + msg);
            Console.WriteLine("\u001b[31mError Code: \u001b[0m" + ec);
            for (int h = 0; h < 3; h++)
            {
                for (int i = 1; i < 8; i++)
                {
                    Console.Write($"\u001b[3{i}m=\u001b[0m");
                }
            }
            Console.WriteLine();
            Console.Write("Shutting down in \u001b[35m5\u001b[0m...");
            Thread.Sleep(1000);
            Console.Write("\rShutting down in \u001b[35m4\u001b[0m...");
            Thread.Sleep(1000);
            Console.Write("\rShutting down in \u001b[35m3\u001b[0m...");
            Thread.Sleep(1000);
            Console.Write("\rShutting down in \u001b[35m2\u001b[0m...");
            Thread.Sleep(1000);
            Console.Write("\rShutting down in \u001b[35m1\u001b[0m...");
            Thread.Sleep(1000);
            Console.Write("\rShutting down...     \n");
            ShutDown(ec);

        }
        public static void ShutDown(int ec)
        {
            Console.WriteLine("Pretend this PC has shut down.");
            Environment.Exit(ec);
        }

        public static void Log(string msg, int spaceBefore = 0, bool showDateTime = true, bool trim = true, bool logAll = false)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }
            if (trim)
            {
                msg.Trim();
                string pattern = @"\u001b\[\d*m";
                string logData = Regex.Replace(msg, pattern, "");
                logData.Trim();
                if (logAll == false && logData.Length > 300)
                {
                    logData = logData.Substring(0, 30) + "..." + "" + logData.Substring(logData.Length - 10);
                }
                if (showDateTime)
                {
                    logData = DateTime.Now.ToString("ddd, dd/MM/yyyy HH:mm:ss.fffffff") + " - " + logData;
                }
                else
                {
                    logData = logData;
                }
                logData = logData.Trim();
                for (int i = 0; i < spaceBefore; i++)
                {
                    logData = "\n" + logData;
                }
                if (!string.IsNullOrWhiteSpace(logLoc) && logInit == true)
                {
                    StreamWriter sr = new StreamWriter(logLoc, append: true);
                    sr.WriteLine(logData);
                    sr.Close();
                }
                else if (!string.IsNullOrWhiteSpace(logLoc) && logInit == false)
                {
                    StreamWriter sr = new StreamWriter(logLoc, append: true);
                    sr.Write(hold);
                    logInit = true;
                    sr.WriteLine(logData);
                    sr.Close();
                }
                else
                {
                    hold += logData + "\n";
                }
            }
            else
            {
                string pattern = @"\u001b\[\d*m";
                string logData = Regex.Replace(msg, pattern, "");
                if (logAll == false && logData.Length > 300)
                {
                    logData = logData.Substring(0, 30) + "..." + "" + logData.Substring(logData.Length - 10);
                }
                if (showDateTime)
                {
                    logData = DateTime.Now.ToString("ddd, dd/MM/yyyy HH:mm:ss.fffffff") + " - " + logData;
                }
                else
                {
                    logData = logData;
                }
                for (int i = 0; i < spaceBefore; i++)
                {
                    logData = "\n" + logData;
                }
                if (!string.IsNullOrWhiteSpace(logLoc) && logInit == true)
                {
                    StreamWriter sr = new StreamWriter(logLoc, append: true);
                    sr.WriteLine(logData);
                    sr.Close();
                }
                else if (!string.IsNullOrWhiteSpace(logLoc) && logInit == false)
                {
                    StreamWriter sr = new StreamWriter(logLoc, append: true);
                    sr.Write(hold);
                    logInit = true;
                    sr.WriteLine(logData);
                    sr.Close();
                }
                else
                {
                    hold += logData + "\n";
                }
            }
        }
    }
}
