using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class KeyRoom : Room
    {
        public KeyRoom(int x, int y, string keyColor) : base(x, y, 'n', Program.RoomType.Key)
        {
            specialColor = keyColor;
            if (keyColor == "Green")
                color = ConsoleColor.Green;
            else if (keyColor == "Red")
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Blue;
        }
    }
}
