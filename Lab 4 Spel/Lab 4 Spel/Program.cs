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
            Player player = new Player();
            //Map setup:
            int mapWidth = 14;
            int mapHeight = 5;
            Room[,] map = new Room[mapWidth, mapHeight];

            for (int y = 0; y < mapHeight; y++)
            {
                map[0, y] = new Wall(0, y);
                map[mapWidth - 1, y] = new Wall(mapWidth - 1, y);
            }
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, 0] = new Wall(x, 0);
                map[x, mapHeight - 1] = new Wall(x, mapHeight - 1);
            }


            bool oneKeyDone = false;
            bool oneDoorDone = false;
            for (int y = 1; y < mapHeight - 1; y++)
            {
                for (int x = 1; x < mapWidth - 1; x++)
                {
                    if ((x == 1 && y == 3) || (x == 7 && y == 1))
                        if (!oneKeyDone)
                        {
                            map[x, y] = new KeyRoom(x, y, "Red");
                            oneKeyDone = true;
                        }
                        else
                            map[x, y] = new KeyRoom(x, y, "Green");
                    else if ((x == 7 && y == 3) || (x == 6 && y == 1))
                        if (!oneDoorDone)
                        {
                            map[x, y] = new DoorRoom(x, y, "Green");
                            oneDoorDone = true;
                        }
                        else
                            map[x, y] = new DoorRoom(x, y, "Red");
                    else if ((x == 11 && y == 2) || (x == 9 && y == 1))
                        map[x, y] = new MonsterRoom(x, y);
                    else if (x >= 1 && x <= 8 && y == 2)
                        map[x, y] = new Wall(x, y);
                    else
                        map[x, y] = new Empty(x, y);
                }
            }
            map[0, 0] = new MonsterRoom(0, 0);

            /*
             01234567890123
           0 ##############
           1 #U    Dn M   #
           2 #########  M #
           3 #n     D     #
           4 ##############            
*/



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
