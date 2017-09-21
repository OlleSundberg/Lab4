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
    class MonsterRoom : Room
    {
        public MonsterRoom(int x, int y) : base(x, y, 'M', Program.RoomType.Monster) { }
        int monsterHP = 100;
    }
    class DoorRoom : Room
    {
        public DoorRoom(int x, int y, string doorColor) : base(x, y, 'D', Program.RoomType.Door)
        {
            this.doorColor = doorColor;
        }
        string doorColor;
    }
    class KeyRoom : Room
    {
        public KeyRoom(int x, int y, string keyColor) : base(x, y, 'n', Program.RoomType.Key)
        {
            this.keyColor = keyColor;
        }
        string keyColor;
    }
    class Wall : Room
    {
        public Wall(int x, int y, bool border) : base(x, y, '#', Program.RoomType.Wall, border)
        {
            this.visible = true;
        }
        public Wall(int x, int y) : base(x, y, '#', Program.RoomType.Wall) { }
    }
    class Empty : Room
    {
        public Empty(int x, int y) : base(x, y, '.', Program.RoomType.Empty) { }
    }
}