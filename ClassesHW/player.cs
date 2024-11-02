using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassesHW
{
    public class Player
    {
        public int playerLevel = 1;
        public int playerHp = 100;
        public int maxAttackPower = 30;
        public int playerLocationX;
        public int playerLocationY;

        public int playerShields;

        //Monster monster = new Monster();

        //for each level up, hp & power increase.
        public void PlayerLevelsUp()
        {
            Console.WriteLine("You've leveled up! Good job");
            playerLevel++;
            playerHp += 15;
            maxAttackPower += 10;
        }

        public void PlayerLoses()
        {
            Console.WriteLine("You have died.");
            SetLocation(0, 0);
        }

        public void SetLocation(int x, int y)
        {
            playerLocationX = x;
            playerLocationY = y;
        }

        public int Attack()
        {
            return new Random().Next(10, maxAttackPower);
        }

        public void TakeDamage(int damage)
        {
            if (playerShields > 0)
                playerShields--;
            else playerHp -= damage;

            if (playerHp < 0)
                playerHp = 0;

            Console.WriteLine("The monster attacked you! Your HP is now " + playerHp + " press enter to continue");
            Console.ReadKey();
        }
    }



}
