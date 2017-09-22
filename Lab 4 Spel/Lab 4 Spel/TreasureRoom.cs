using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class TreasureRoom : Room
    {
        public TreasureRoom(int x, int y) : base(x,y,'o', Program.RoomType.Treasure)
        {
            color = ConsoleColor.Yellow;
        }
    }
}
