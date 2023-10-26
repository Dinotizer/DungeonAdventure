using System.Media;

namespace Dungeon
{
    [Serializable]
    public class Player
    {
        Random rand = new Random();

        public string name;
        public int id;
        public int coins = 300;
        public int health = 10;
        public int damage = 1;
        public int armourValue = 0;
        public int potion = 5;
        public int weaponValue = 1;

        public int mods = 0;

        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return rand.Next(lower,upper);
        }
        public int GetPower()
        {
            int upper = (2 * mods + 3);
            int lower = (mods + 1);
            return rand.Next(lower, upper);
        }
        public int GetCoins()
        {
            SoundPlayer soundPlayer = new SoundPlayer("sounds/achievement.wav");
            soundPlayer.Play();
            int upper = (15 * mods + 50);
            int lower = (10 * mods + 10);
            return rand.Next(lower, upper);
        }
    }
}
