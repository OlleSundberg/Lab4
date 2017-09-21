using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Player
    {
        public int X;
        public int Y;
        public int HP = 100;

        public Player(int x, int y)
        {
            X = x;
            Y = y;            
        }
        public void check(Room[,] map)
        {
            map[X - 1, Y].visible = true;
            map[X + 1, Y].visible = true;
            map[X, Y + 1].visible = true;
            map[X, Y - 1].visible = true;
        }
    }
}
