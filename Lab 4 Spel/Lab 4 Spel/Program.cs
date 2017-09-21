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
                                Console.Write(map[x, y].mapIcon);
                                Console.Write(map[x, y].mapIcon);
                                Console.Write(map[x, y].mapIcon);
                                Console.Write(map[x, y].mapIcon);
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
                Console.WriteLine("HP: ");
                Console.WriteLine("Keys: ");


                // Beskriv rummet (baserat på enum?):

                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                    case 'w':
                        if (map[player.X, player.Y - 1].type != RoomType.Wall)
                            player.Y--;
                        break;
                    case 'A':
                    case 'a':
                        if (map[player.X - 1, player.Y].type != RoomType.Wall)
                            player.X--;
                        break;
                    case 'S':
                    case 's':
                        if (map[player.X, player.Y + 1].type != RoomType.Wall)
                            player.Y++;
                        break;
                    case 'D':
                    case 'd':
                        if (map[player.X + 1, player.Y].type != RoomType.Wall)
                            player.X++;
                        break;
                }
                player.check(map);
                Console.Clear();
            }
        }
    }
}
