using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    interface IMapIcon
    {
        char mapIcon { get; set; }
    }
    abstract class Room : IMapIcon
    {
        public int x, y;
        public char mapIcon { get; set; }
        public Program.RoomType type;
        public bool visible = false;
        public ConsoleColor color = ConsoleColor.Gray;
        public string specialColor = "";

        public Room(int x, int y, char mapIcon, Program.RoomType type)
        {
            this.x = x;
            this.y = y;
            this.mapIcon = mapIcon;
            this.type = type;
        }
        public Room(int x, int y, char mapIcon, Program.RoomType type, bool border)
        {
            this.x = x;
            this.y = y;
            this.mapIcon = mapIcon;
            this.type = type;
        }
    }
}