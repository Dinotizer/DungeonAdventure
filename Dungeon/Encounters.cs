using System.Media;

namespace Dungeon
{
    public class Encounters
    {
        static Random rand = new Random();
        // Encounter Generic

        
        // Encounters
        public static void FirstEncounter()
        {
            Program.Print("You fall unceremoniously through the door and grab a rusty sword, while charging toward your captor.", 60);
            Console.WriteLine();
            Program.Print("He turns...", 60);
            Console.WriteLine("Press any key to enter battle!");
            Console.ReadKey();
            Combat(false, "Human Rogue", 1, 4);
        }
        public static void BasicEncounter()
        {
            Program.Print("You run into an enemy!", 60);
            Console.ReadKey();
            Combat(false, "", 0, 0);
        }

        // Encounter Tools
        public static void RandomEncounter()
        {
            switch(rand.Next(0,1))
            {
                case 0:
                    BasicEncounter();
                    break;
            }
        }
        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int pwr = 0;
            int hlth = 0;
            if (random)
            {
                n = GetName();
                pwr = Program.currentPlayer.GetPower();
                hlth = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                pwr = power;
                hlth = health;
            }
            while(hlth > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine($"Power: {pwr} / Health: {hlth}");
                Console.WriteLine("=====================");
                Console.WriteLine("|------ACTIONS------|");
                Console.WriteLine("|(A)ttack (D)efend  |");
                Console.WriteLine("|(R)un    (H)eal    |");
                Console.WriteLine("=====================");
                Console.WriteLine("PLAYER:");
                Console.WriteLine($"Potions: {Program.currentPlayer.potion} | Health: {Program.currentPlayer.health}");
                Console.WriteLine("=====================");
                Console.WriteLine("What do you want to do?");
                
                string input1 = Console.ReadLine();
                if (input1.ToLower() == "a" || input1 == "attack")
                {
                    // Attack
                    SoundPlayer soundPlayer = new SoundPlayer("sounds/sword1.wav");
                    soundPlayer.Play();

                    Program.Print("You attack!", 60);
                    int damage = pwr - Program.currentPlayer.armourValue;

                    if(damage<0)
                        damage = 0;

                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1,4);
                    Program.Print($"You lose {damage} health and deal {attack} damage.", 60);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key.");
                    Console.ReadKey();
                    Program.currentPlayer.health -= damage;
                    hlth -= attack;

                    soundPlayer.Stop();
                }
                else if (input1.ToLower() == "d" || input1.ToLower() == "defend")
                {
                    // Defend
                    Console.WriteLine("You defend!");
                    int damage = (pwr/4) - Program.currentPlayer.armourValue;

                    if (damage < 0)
                        damage = 0;

                    int attack = rand.Next(0, Program.currentPlayer.weaponValue)/2;
                    Program.Print($"You lose {damage} health and deal {attack} damage.", 60);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key.");
                    Console.ReadKey();
                    Program.currentPlayer.health -= damage;
                    hlth -= attack;
                }
                else if (input1.ToLower() == "r" || input1.ToLower() == "run")
                {
                    // Run
                    Program.Print("You choose to run!", 60);
                    if(rand.Next(0, 2) == 0)
                    {
                        Program.Print("You try to run away, but your are attacked as you attempt to do so and are knocked down by the enemy.", 60);
                        int damage = pwr - Program.currentPlayer.armourValue;

                        if (damage < 0)
                            damage = 0;

                        Program.Print($"You lose {damage} health and are unable to escape", 60);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Program.Print($"You successfully escape the {n}.", 60);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key.");
                        Console.ReadKey();
                        Shop.LoadShop(Program.currentPlayer);
                    }
                }
                else if (input1.ToLower() == "h" || input1.ToLower() == "heal")
                {
                    // Heal
                    if (Program.currentPlayer.potion == 0)
                    {
                        Console.WriteLine("You have no potions.");
                        int damage = pwr - Program.currentPlayer.armourValue;

                        if (damage < 0)
                            damage = 0;

                        Program.Print($"The creature strikes you and you lose {damage} health", 60);
                    }
                    else
                    {
                        Program.Print("You heal successfully.", 60);
                        int potionValue = 5;
                        Program.Print($"You gain {potionValue} health.", 60);
                        Program.currentPlayer.health += potionValue;
                        Program.Print($"While you were taking your potion, the {n} advanced and struck you!", 60);
                        int damage = (pwr/2)-Program.currentPlayer.armourValue;
                        if (damage < 0)
                            damage = 0;
                        Program.Print($"You lose {damage} health.", 60);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key.");
                    }
                }
                Console.ReadKey();
                Console.Clear();
            }
            int coin = Program.currentPlayer.GetCoins();
            Program.Print($"As you stand victorious over {n}, its body disolves into {coin} gold coins.", 60);
            Console.WriteLine("");
            Console.WriteLine("");
            //Console.Clear();
            Console.WriteLine("Press any key.");
            Console.ReadKey();
            Console.Clear();
        }
        public static string GetName()
        {
            switch(rand.Next(0, 5))
            {
                case 0:
                    return "Skeleton";
                case 1:
                    return "Zombie";
                case 2:
                    return "Cultist";
                case 3:
                    return "Evil wizard";
                case 4:
                    return "Ogre";
            }
            return "Goblin";
        }
    }
}
