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
                    for (int x = 0; x < mapWidth; x++)
                    {
                        if (x == player.X && y == player.Y)
                            Console.Write('@');
                        else
                            Console.Write(map[x, y].mapIcon);
                    }
                    Console.WriteLine("");
                }

                // Beskriv rummet (baserat på enum?):

                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                    case 'w':
                        //if ( Room.N ) {
                        //Player Y++
                        Console.WriteLine("You went north.");
                        //} else {cw "you cant go this way";}
                        break;
                    case 'A':
                    case 'a':
                        //Player X--
                        break;
                    case 'S':
                    case 's':
                        //Player Y--
                        break;
                    case 'D':
                    case 'd':
                        //Player X++
                        break;
                }

            }
        }
    }
}
