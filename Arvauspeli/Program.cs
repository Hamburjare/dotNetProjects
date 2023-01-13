using System;
using System.Collections.Generic;

var random = new Random();
var arvattava = random.Next(1, 101);
int arvaus = 0;
bool arvattu = false;
List<int> arvaukset = new List<int>();
arvaus = KysyLuku();
Console.Clear();

while (!arvattu)
{
    if (arvaus < arvattava)
    {
        Console.WriteLine("Luku on suurempi.");
    }
    else if (arvaus > arvattava)
    {
        Console.WriteLine("Luku on pienempi.");
    }
    else
    {
        Console.WriteLine("Arvasit oikein!");
        Console.WriteLine("Arvauksesi olivat: ");
        foreach (var item in arvaukset)
        {
            Console.WriteLine(item);
        }
        arvattu = true;
        break;
    }
    arvaus = KysyLuku();
}

Console.WriteLine("Paina mitä tahansa näppäintä lopettaaksesi.");
Console.ReadKey();

int KysyLuku()
{
    Console.WriteLine($"Arvauksesi: {String.Join(", ", arvaukset.ToArray())}");
    Console.WriteLine($"Olet arvannut {arvaukset.Count} kertaa.");
    Console.Write("Arvaa luku väliltä 1-100: ");
    int value = LueLuku();
    arvaukset.Add(value);
    Console.Clear();

    return value;
}

int LueLuku()
{
    int number;
    while (!int.TryParse(Console.ReadLine(), out number))
    {
        Console.WriteLine("Virheellinen syöte. Yritä uudelleen.");
    }
    return number;
}
