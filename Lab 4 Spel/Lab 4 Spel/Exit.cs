using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Exit : Room
    {
        public Exit(int x, int y) : base(x, y, 'U', Program.RoomType.Exit)
        {
            color = ConsoleColor.Cyan;
        }
    }
}
