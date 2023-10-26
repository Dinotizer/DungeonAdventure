namespace Dungeon
{
    public class Shop
    {
        public static void LoadShop(Player p)
        {
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionPrice;
            int armourPrice;
            int weaponPrice;
            int difPrice;

            while (true)
            {
                potionPrice = 20 + 10 * p.mods;
                armourPrice = 100 * (p.armourValue + 1);
                weaponPrice = 100 * p.weaponValue;
                difPrice = 300 + 100 * p.mods;

                Console.Clear();
                Console.WriteLine("==========SHOP=========");
                Console.WriteLine($"|(P)otions:     {potionPrice} coins|");
                Console.WriteLine($"|(W)eapons:     {weaponPrice} coins|");
                Console.WriteLine($"|(A)rmour:      {armourPrice} coins|");
                Console.WriteLine($"|(D)ifficulty Mod: {difPrice} coins|");
                Console.WriteLine("=======================");
                Console.WriteLine("(E)xit Shop");
                Console.WriteLine("(Q)uit Game");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"===={p.name} STATS====");
                Console.WriteLine($"|Coins: {p.coins}");
                Console.WriteLine($"|Cuurrent Health: {p.health}");
                Console.WriteLine($"|Weapon Strength: {p.weaponValue}");
                Console.WriteLine($"|Armour Toughness: {p.armourValue}");
                Console.WriteLine($"|Potions: {p.potion}");
                Console.WriteLine($"|Difficulty Mods: {p.mods}");
                Console.WriteLine("=======================");
                // Wait for input

                string input = Console.ReadLine().ToLower();
                if (input == "p" || input == "potions")
                {
                    TryBuy("potions", potionPrice, p);
                }
                else if (input == "w" || input == "weapons")
                {
                    TryBuy("weapons", weaponPrice, p);
                }
                else if (input == "a" || input == "armour")
                {
                    TryBuy("armour", armourPrice, p);
                }
                else if (input == "d" || input == "difficulty mod")
                {
                    TryBuy("difficulty mod", difPrice, p);
                }
                else if (input == "q" || input == "quit game")
                {
                    Program.Quit();
                }
                else if (input == "e" || input == "exit shop")
                    break;
            }
        }
        static void TryBuy(string item, int cost, Player p)
        {
            if(p.coins >= cost)
            {
                if (item == "potions")
                    p.potion++;
                else if(item == "weapons")
                    p.weaponValue++;
                else if(item == "armour")
                    p.armourValue++;
                else if (item == "dif")
                    p.mods++;

                p.coins -= cost;
            }
            else
            {
                Program.Print("You don't have enough gold, peon!", 60);
                Console.WriteLine("Press any key.");
                Console.ReadKey();
            }
        }
    }
}
