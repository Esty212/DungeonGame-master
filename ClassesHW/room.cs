using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesHW
{
    public class Room
    {
        public string name;
        public int roomX;
        public int roomY;



        public Room (string name, int roomX, int roomY)
        {
            this.name = name;
            this.roomX = roomX;
            this.roomY = roomY;
        }
        

        public bool IsInRoom(int x, int y)
        {
            return roomX == x && roomY == y;
        }

        public bool IsInTreasureRoom() => this is TreasureRoom;

        public bool IsInTrainingRoom() => this is TrainingRoom;

        public void OnEnteredRoom()
        {
            Console.WriteLine("You've entered the " + name);
        }
    }
}
