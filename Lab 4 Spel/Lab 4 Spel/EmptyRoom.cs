﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class EmptyRoom : Room
    {
        public EmptyRoom(int x, int y) : base(x, y, '.', Program.RoomType.Empty) { }
    }
}
