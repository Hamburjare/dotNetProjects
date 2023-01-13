using TurboReader;
using System;

namespace TurboReaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TurboReader.TurboReader tr = new TurboReader.TurboReader();
            Console.WriteLine(tr.CheckIfOnlyLetters("123"));
            Console.WriteLine(tr.CheckIfOnlyLetters("abc"));
            Console.WriteLine(tr.CheckIfNumber("123"));
            Console.WriteLine(tr.CheckIfNumber("abc"));
            Console.WriteLine(tr.CheckIfFloat("123,456"));
            Console.WriteLine(tr.CheckIfFloat("abc"));
            Console.WriteLine(tr.CheckIfDouble("123,456"));
            Console.WriteLine(tr.CheckIfDouble("abc"));
            Console.WriteLine(tr.CheckIfDecimal("123,456"));
            Console.WriteLine(tr.CheckIfDecimal("abc"));
            Console.ReadKey();
        }
    }
}