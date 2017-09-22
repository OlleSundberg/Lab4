using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class DoorRoom : Room
    {
        public DoorRoom(int x, int y, string doorColor) : base(x, y, 'D', Program.RoomType.Door)
        {
            specialColor = doorColor;
            if (doorColor == "Green")
                color = ConsoleColor.Green;
            else if (doorColor == "Red")
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Blue;
        }
    }
}
