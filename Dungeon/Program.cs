using System.Media;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dungeon
{
    public class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;

        static void Main(string[] args)
        {
            if(!Directory.Exists("Saved Games"))
            {
                Directory.CreateDirectory("Saved Games");
            }
            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();
            
            while (mainLoop)
            {
                Story.StoryOne();
                Encounters.RandomEncounter();
            }
        }

        // NEW START // INTRO //
        static Player NewStart(int id)
        {
            SoundPlayer soundPlayer = new SoundPlayer("sounds/dungeon.wav");
            soundPlayer.PlayLooping();

            Console.Clear();
            Player player = new Player();

            // Welcome
            Print("Welcome to the Dungeon!");
            Console.WriteLine();
            Print("What is your name?");
            Console.WriteLine();
            player.name = Console.ReadLine();
            player.id = id;
            Console.Clear();

            // Intro
            Print("You wake up in a cold stone room. You feel dazed and are having trouble remembering why you're there, or who you are.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("There's also a really pissed off looking chicken in the corner, glaring at you.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("You didn't think a chicken could look emotionally out of sorts, and you begin to wonder what would upset a chicken.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("And, indeed, why this chicken is locked in a dungeon with you...", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("...", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("Snap out of it!", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("'Who am I', you ask yourself... And what's the deal with the chicken?", 60);
            Console.WriteLine();
            Console.WriteLine();

            // Player name recollection
            if (player.name == "")
                Print("You can't even remember your own name...", 60);
            else
                Print($"After some thought, you remember that your name is {player.name}.", 60);
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            // Escape the room
            Print("You feel your way around the sparcely lit room until you find a door handle. You feel some resistance as you turn the handle, but the rusty lock breaks with little effort.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Print("You see your captor standing with his back to you outside the door.", 60);
            Console.WriteLine();
            Console.WriteLine();
            // Goes to first encounter.

            //soundPlayer.Stop();

            return player;
        }

        // QUIT GAME //
        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }

        // SAVE GAME //
        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter(); //OBSOLETE
            //JsonSerializer jsonSerializer = new JsonSerializer(typeof(Player));
            string path = "Saved Games/" + currentPlayer.id.ToString() + ".player";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            bf.Serialize(file, currentPlayer);
            file.Close();
        }

        // LOAD OR CREATE GAME //
        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            Console.WriteLine("Choose your saved game or create a new one:");
            string[] paths = Directory.GetFiles("Saved Games");
            List<Player> list = new List<Player>();
            int idCount = 0;
            
            BinaryFormatter bf = new BinaryFormatter();
            foreach (string path in paths)
            {
                FileStream file = File.Open(path, FileMode.Open);
                Player player = (Player)bf.Deserialize(file);
                file.Close();
                list.Add(player);
            }

            idCount = list.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Chose your player:");

                foreach (Player player in list)
                {
                    Console.WriteLine($"{player.id}:{player.name}");
                }

                Console.WriteLine("Please input player ID (id:#), or (c)reate a new character.");
                //Console.WriteLine("Type player name | For ID, type: ID:<id number>");
                string[] playerInput = Console.ReadLine().Split(':');
                try
                {
                    if(playerInput[0] == "id")
                    {
                        if (int.TryParse(playerInput[1], out int id))
                        {
                            foreach (Player player in list)
                            {
                                if (player.id == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There is no player with that id. Press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Your id needs to be a number. Press any key to continue.");
                            Console.ReadKey();
                        }
                    }
                    else if (playerInput[0].ToLower() == "create" || playerInput[0] == "c")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;

                        //Encounters.FirstEncounter();
                    }
                    else
                    {
                        foreach(Player player in list)
                        {
                            if (player.name == playerInput[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There is no player with that name. Press any key to continue.");
                        Console.ReadKey();
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    Console.WriteLine("Your id needs to be a number. Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
        // PRINT TEXT //
        public static void Print(string text, int speed = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
        }
    }
}