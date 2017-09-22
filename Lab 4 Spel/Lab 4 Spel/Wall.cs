using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Wall : Room
    {
        public Wall(int x, int y, bool border) : base(x, y, '#', Program.RoomType.Wall, border)
        {
            this.visible = true;
        }
        public Wall(int x, int y) : base(x, y, '#', Program.RoomType.Wall) { }
    }
}
