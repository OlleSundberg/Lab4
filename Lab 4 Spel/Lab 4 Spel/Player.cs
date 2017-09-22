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
        private int hitPoints = 100;
        public int HP
        {
            get { return hitPoints; }
            set { hitPoints = value; }
        }
        public bool wasHurt = false;
        public bool wasSpiked = false;
        public bool foundTreasure = false;
        public char playerIcon = '@';

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
        public void fow(Room[,] map, int w, int h)
        {
            for (int x = 1; x < w - 1; x++)
            {
                for (int y = 1; y < h - 1; y++)
                {
                    map[x, y].visible = false;
                }
            }
            map[X - 1, Y - 1].visible = true;
            map[X + 1, Y - 1].visible = true;
            map[X - 1, Y + 1].visible = true;
            map[X + 1, Y + 1].visible = true;
            check(map);
        }
    }
}
