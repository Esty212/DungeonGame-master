using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesHW
{
    internal class TreasureRoom : Room
    {


        public TreasureRoom(string name, int roomX, int roomY) : base(name, roomX, roomY)
        {

        }

        public int RandomizeReward()
        {
            Random rnd = new Random();
            return rnd.Next(1, 3);

        }
    }
}
