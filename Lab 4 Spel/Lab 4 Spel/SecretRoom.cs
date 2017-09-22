using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class SecretRoom : Room
    {
        public SecretRoom(int x, int y) : base(x,y,'?', Program.RoomType.Secret)
        {
            color = ConsoleColor.DarkGray;
        }
        
    }
}
