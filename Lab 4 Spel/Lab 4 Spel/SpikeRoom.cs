using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class SpikeRoom : Room
    {
        public SpikeRoom(int x, int y) : base(x, y, '^', Program.RoomType.Spike) {
            color = ConsoleColor.Red;
        }
    }
}
