using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class Story
    {
        public static void StoryOne()
        {
            Console.Clear();
            Program.Print("You look around after you defeated the guard, and you notice that the chicken is poking its head through the door.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Program.Print("It looks less annoyed and more inquisitive.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Program.Print("As you take stock of your surroundings, you notice another door.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Program.Print("You open it and see a long corridor.", 60);
            Console.WriteLine();
            Console.WriteLine();
            Program.Print("You decide to walk down it, you notice that the chicken is following you.", 60);
            Console.WriteLine();
            Console.WriteLine();

            int coin = Program.currentPlayer.GetCoins();
            Program.Print($"You have a new companion! The chicken gives you {coin} coins.", 60);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
