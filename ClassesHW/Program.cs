using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassesHW
{
    class Program
    {
        List<Monster> monsters = new List<Monster>();
        Player player = new Player();
        List<Room> rooms = new List<Room>();
        int failCount = 0;

        static void Main()
        {
            Program program = new Program();

            Room room1 = new Room("Hall of Whispers", 0, 2);
            Room room2 = new Room("Maze of Shattered Dreams", 2, 0);
            Room room3 = new Room("Inferno's Heart", 3, 1);
            Room room4 = new Room("Vault of Broken Souls", 2, 3);


            program.rooms.Add(room1);
            program.rooms.Add(room2);
            program.rooms.Add(room3);
            program.rooms.Add(room4);

            Monster monster1 = new Monster(60, 15, "Soul Eater", 2, 3);
            Monster monster2 = new Monster(80, 20, "Blood Drinker", 3, 1);
            Monster monster3 = new Monster(70, 30, "Shadow Beast", 0, 2);
            Monster monster4 = new Monster(50, 20, "Death Wolf", 2, 0);
            Monster monster5 = new Monster(90, 30, "Black Claw", 4, 5);
            Monster monster6 = new Monster(90, 30, "Black Claw", 5, 4);

            program.monsters.Add(monster1);
            program.monsters.Add(monster2);
            program.monsters.Add(monster3);
            program.monsters.Add(monster4);
            program.monsters.Add(monster5);
            program.monsters.Add(monster6);


            Console.WriteLine("Welcome to the Dungeon Game! Defeat all the monsters to win. Press enter to start. ");
            Console.ReadKey();

            int[,] grid = new int[6, 6];


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }


            program.PlayerMoves(grid);
        }


        public void PlayerMoves(int[,] grid)
        {
            while (true)
            {

                Console.WriteLine("Which way do you want to go? Use 'w'/'a'/'s'/'d' to move");
                string playerInput = Console.ReadLine();

                //player moves right.
                if (playerInput == "d" && player.playerLocationX < grid.GetLength(1) - 1)
                {
                    player.SetLocation(player.playerLocationX + 1, player.playerLocationY);


                }

                //player moves down.
                else if (playerInput == "s" && player.playerLocationY < grid.GetLength(0) - 1)
                {
                    player.SetLocation(player.playerLocationX, player.playerLocationY + 1);

                }

                //player moves left.
                else if (playerInput == "a" && player.playerLocationX > 0)
                {
                    player.SetLocation(player.playerLocationX - 1, player.playerLocationY);

                }

                //player moves up.
                else if (playerInput == "w" && player.playerLocationY > 0)
                {
                    player.SetLocation(player.playerLocationX, player.playerLocationY - 1);

                }
                else
                {
                    Console.WriteLine("Oops, You can't go any further. Please try other direction.");
                }

                foreach (Room room in rooms)
                {
                    if (room.IsInRoom(player.playerLocationX, player.playerLocationY))
                        room.OnEnteredRoom();
                }

                // Check if player encountered monster
                foreach (Monster monster in monsters)
                {
                    if (monster.HasEncounteredMonster(player.playerLocationX, player.playerLocationY))
                    {
                        StartFight(monster);
                        if(failCount == 10)
                        {
                            Console.WriteLine("Game over");
                            return;
                        }
                    }
                }

                //When player reaches bottom right corner = he wins.
                if (player.playerLocationX == grid.GetLength(1) - 1 && player.playerLocationY == grid.GetLength(0) - 1)
                {
                    Console.WriteLine("You won!");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Your current location is " + player.playerLocationX + ", " + player.playerLocationY);
            }


        }

        public void StartFight(Monster monster)
        {
            Console.WriteLine("You've encountered " + monster.name + "! Press enter to start the battle");
            Console.ReadKey();

            bool hasPlayerDied = player.playerHp <= 0;
            bool hasMonsterDied = monster.monsterHp <= 0;
            while (!hasPlayerDied && !hasMonsterDied)
            {
                hasPlayerDied = player.playerHp <= 0;
                if (!hasPlayerDied)
                {
                    monster.monsterHp -= player.Attack();
                    if(monster.monsterHp < 0)
                        monster.monsterHp = 0;
                    Console.WriteLine("You attacked the monster! monster's hp is now " + monster.monsterHp + " press enter to continue");
                    Console.ReadKey();
                }
                else
                {
                    player.PlayerLoses();
                    failCount++;
                    return;
                }

                hasMonsterDied = monster.monsterHp <= 0;
                if (!hasMonsterDied)
                {
                    player.playerHp -= monster.MonsterAttack();
                    if(player.playerHp < 0)
                        player.playerHp = 0;
                    Console.WriteLine("The monster attacked you! Your HP is now " + player.playerHp + " press enter to continue");
                    Console.ReadKey();
                }
                else
                {
                    player.PlayerLevelsUp();
                }
            }
        }
    }
}
