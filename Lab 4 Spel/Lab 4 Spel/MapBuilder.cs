using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class MapBuilder
    {
        public Room[,] BuildMap(int mapWidth, int mapHeight)
        {
            Room[,] map = new Room[mapWidth, mapHeight];

            //Create border
            for (int y = 0; y < mapHeight; y++)
            {
                map[0, y] = new Wall(0, y, true);
                map[mapWidth - 1, y] = new Wall(mapWidth - 1, y, true);
            }
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, 0] = new Wall(x, 0, true);
                map[x, mapHeight - 1] = new Wall(x, mapHeight - 1, true);
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
            return map;
        }
    }
}
