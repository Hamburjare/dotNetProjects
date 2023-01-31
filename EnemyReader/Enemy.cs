using Newtonsoft.Json;

namespace EnemyReader;

class Enemy
{
    public string name;
    public int hitpoints;
    public int stamina;
    public int attack_damage;

    public Enemy JSON_to_Enemy(string filename)
    {
        using (StreamReader r = new StreamReader(filename))
        {
            string json = r.ReadToEnd();

            Enemy enemy = JsonConvert.DeserializeObject<Enemy>(
                json,
                new JsonSerializerSettings
                {
                    Error = delegate(
                        object sender,
                        Newtonsoft.Json.Serialization.ErrorEventArgs args
                    )
                    {
                        Console.WriteLine(
                            "Error converting JSON to Enemy object. File: " + filename
                        );
                        Console.WriteLine(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                }
            );

            return enemy;
        }
    }

    public string ToString()
    {
        Console.WriteLine(new string('-', Console.WindowWidth));

        return String.Format(
            "Name: {0}\nHit Points: {1}\nStamina: {2}\nAttack Damage: {3}",
            name,
            hitpoints,
            stamina,
            attack_damage
        );
    }

    static void Main(string[] args)
    {
        Enemy enemy = new Enemy();
        enemy = enemy.JSON_to_Enemy("./json/Goblin_Archer.json");
        Console.WriteLine(enemy.ToString());
        enemy = enemy.JSON_to_Enemy("./json/Goblin_Mage.json");
        Console.WriteLine(enemy.ToString());
        enemy = enemy.JSON_to_Enemy("./json/Goblin_Warrior.json");
        Console.WriteLine(enemy.ToString());
        Console.ReadKey();
    }
}
