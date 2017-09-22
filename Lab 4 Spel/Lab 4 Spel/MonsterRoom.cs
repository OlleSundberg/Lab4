using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class MonsterRoom : Room
    {
        public MonsterRoom(int x, int y) : base(x, y, 'M', Program.RoomType.Monster)
        {
            color = ConsoleColor.DarkMagenta;
        }
        int monsterHP = 100;
    }
}
