// Olle:    49
// Viktor:  44
// ???

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Program
    {
        public enum RoomType { Empty, Monster, Door, Exit, Key, Wall }
        static void Main(string[] args)
        {
            Player player = new Player(12, 3);
            Keys keys = new Keys();

            //Map setup:
            int mapWidth = 14;
            int mapHeight = 5;

            MapBuilder mb = new MapBuilder();
            Room[,] map = mb.BuildMap(mapWidth, mapHeight);

            /*
             01234567890123
           0 ##############
           1 #U    Dn M   #
           2 #########  M #
           3 #n     D     #
           4 ##############            
*/

            //Game-loop:
            int turns = 0;
            while (true)
            {
                player.check(map);
                turns++;
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int x = 0; x < mapWidth; x++)
                        {
                            if (x == player.X && y == player.Y)
                            {
                                Console.Write('@');
                                Console.Write('@');
                                Console.Write('@');
                                Console.Write('@');
                            }
                            else if (map[x, y].visible)
                            {
                                Console.ForegroundColor = map[x, y].color;
                                Console.Write(map[x, y].mapIcon);
                                Console.Write(map[x, y].mapIcon);
                                Console.Write(map[x, y].mapIcon);
                                Console.Write(map[x, y].mapIcon);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            else {
                                Console.Write(' ');
                                Console.Write(' ');
                                Console.Write(' ');
                                Console.Write(' ');
                            }
                        }
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine("");
                Console.Write("HP: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(player.HP);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Keys: ");
                if (keys.hasRed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("o--m ");
                }
                if (keys.hasGreen)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("o--m");
                }
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Turns: " + turns);


                // Beskriv rummet (baserat på enum?):

                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                    case 'w':
                        if (map[player.X, player.Y - 1].type != RoomType.Wall)
                            player.Y--;
                        else
                            turns--;
                        break;
                    case 'A':
                    case 'a':
                        if (map[player.X-1,player.Y].type == RoomType.Door &&
                            ((map[player.X - 1, player.Y].specialColor == "Green" && keys.hasGreen) ||
                            (map[player.X - 1, player.Y].specialColor == "Red" && keys.hasRed)))
                        {
                            map[player.X - 1, player.Y].type = RoomType.Empty;
                            map[player.X - 1, player.Y].mapIcon = '.';
                        }
                        if (map[player.X - 1, player.Y].type != RoomType.Wall && map[player.X-1,player.Y].type != RoomType.Door)
                            player.X--;
                        else
                            turns--;
                        if (map[player.X, player.Y].type == RoomType.Key)
                        {
                            if (map[player.X, player.Y].specialColor == "Green")
                                keys.hasGreen = true;
                            else
                                keys.hasRed = true;
                            map[player.X, player.Y].type = RoomType.Empty;
                            map[player.X, player.Y].mapIcon = '.';
                            map[player.X, player.Y].color = ConsoleColor.Gray;
                        }
                        break;
                    case 'S':
                    case 's':
                        if (map[player.X, player.Y + 1].type != RoomType.Wall)
                            player.Y++;
                        else
                            turns--;
                        break;
                    case 'D':
                    case 'd':
                        if (map[player.X + 1, player.Y].type != RoomType.Wall)
                            player.X++;
                        else
                            turns--;
                        break;
                }
                if (player.X == 1 && player.Y == 1)
                    winGame(turns);
                player.check(map);
                Console.Clear();
            }
        }
        static void winGame(int turns)
        {
            Console.Clear();
            Console.WriteLine("Congratulations, you win! Turns: " + turns);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
