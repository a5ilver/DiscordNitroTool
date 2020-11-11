using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ConsoleUtils
    {
        public static void SetTitle(string title) => Console.Title = title;

        public static bool Debug = true;

        public static void LogV2(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Discord Nitro Tool] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"({DateTime.Now.ToShortTimeString()}) ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text + "\n");
        }

        public static void LogErrorV2(string text)
        {
            if (Debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Discord Nitro Tool] ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"({DateTime.Now.ToShortTimeString()}) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(text + "\n");
            }
        }

        public static void Log(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Discord Nitro Tool] ({DateTime.Now.ToShortTimeString()}) {text}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(string text)
        {
            if (Debug)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Discord Nitro Tool] ({DateTime.Now.ToShortTimeString()}) {text}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
