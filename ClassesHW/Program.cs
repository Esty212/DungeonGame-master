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
            TreasureRoom room5 = new TreasureRoom("Treasure Room", 4, 2);
            TreasureRoom room6 = new TreasureRoom("Treasure Room", 5, 1);
            TrainingRoom room7 = new TrainingRoom("Training Room", 2, 5);


            program.rooms.Add(room1);
            program.rooms.Add(room2);
            program.rooms.Add(room3);
            program.rooms.Add(room4);
            program.rooms.Add(room5);
            program.rooms.Add(room6);
            program.rooms.Add(room7);

            Monster monster1 = new Monster(60, 15, "Soul Eater", 2, 3);
            Monster monster2 = new Monster(80, 20, "Blood Drinker", 3, 1);
            Monster monster3 = new Monster(70, 30, "Shadow Beast", 0, 2);
            Monster monster4 = new Monster(50, 20, "Death Wolf", 2, 0);
            Monster monster5 = new Monster(90, 30, "Black Claw", 4, 5);
            Monster monster6 = new Monster(90, 30, "Black Claw", 5, 4);
            EliteMonster monster7 = new EliteMonster(100, 35, "Elite Monster", 1, 3);
            EliteMonster monster8 = new EliteMonster(100, 30, "Elite Monster", 4, 1);
            RageMonster monster9 = new RageMonster(100, 30, "Rage Monster", 3, 2);
            RageMonster monster10 = new RageMonster(100, 30, "Rage Monster", 2, 4);


            program.monsters.Add(monster1);
            program.monsters.Add(monster2);
            program.monsters.Add(monster3);
            program.monsters.Add(monster4);
            program.monsters.Add(monster5);
            program.monsters.Add(monster6);
            program.monsters.Add(monster7);
            program.monsters.Add(monster8);
            program.monsters.Add(monster9);
            program.monsters.Add(monster10);


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
                    {
                        room.OnEnteredRoom();

                        if (room.IsTreasureRoom())
                        {
                            TreasureRoom treasureRoom = room as TreasureRoom;
                            int randomReward = treasureRoom.RandomizeReward();
                            if (randomReward == 1) // Increase XP
                            {
                                player.playerLevel++;
                                Console.WriteLine("You received XP and leveled up!");
                            }

                            if (randomReward == 2) // receive one shield
                            {
                                player.playerShields++;
                                Console.WriteLine("You received a shield! Your shield will absorb one monster attack.");
                            }

                        }

                        if (room.IsInTrainingRoom())
                        {
                            TrainingRoom trainingRoom = room as TrainingRoom;
                            player.maxAttackPower += 30;
                            Console.WriteLine("You just received extra 30 power points from the training room!");
                        }
                    }
                }

                // Check if player encountered monster
                foreach (Monster monster in monsters)
                {
                    if (monster.HasEncounteredMonster(player.playerLocationX, player.playerLocationY))
                    {
                        StartFight(monster);
                        if (failCount == 10)
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
                    monster.TakeDamage(player.Attack());
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
                    player.TakeDamage(monster.MonsterAttack());
                }
                else
                {
                    if (monster.IsEliteMonter())
                    {
                        EliteMonster encounteredMonster = (EliteMonster)monster;
                        encounteredMonster.Revive();
                    }
                    else player.PlayerLevelsUp();

                }
            }
        }
    }
}
