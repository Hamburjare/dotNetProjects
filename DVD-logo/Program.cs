using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVD
{
    class Program
    {
        private static Random _random = new Random();

        private static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            var value = (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
            if (value == ConsoleColor.Black) {
                while(value != ConsoleColor.Black) {
                    value = GetRandomConsoleColor();
                }
            }
            return value;
        }

        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            int dx = 1;
            int dy = 1;
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            string dvd = "DVD";
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(x, y);
                Console.Write(dvd);
                if (x == width - dvd.Length)
                {
                    dx = -1;
                    Console.ForegroundColor = GetRandomConsoleColor();
                }
                if (x == 0)
                {
                    dx = 1;
                    Console.ForegroundColor = GetRandomConsoleColor();
                }
                if (y == height - dvd.Length)
                {
                    dy = -1;
                    Console.ForegroundColor = GetRandomConsoleColor();
                }
                if (y == 0)
                {
                    dy = 1;
                    Console.ForegroundColor = GetRandomConsoleColor();
                }
                x += dx;
                y += dy;
                Thread.Sleep(100);
            }
        }
    }
}
