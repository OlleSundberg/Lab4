using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                switch(Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                    case 'w':
                        //if ( Room.N ) {
                        //Player Y++
                        Console.WriteLine("You went north.");
                        //} else {cw "you cant go this way";}
                        break;
                    case 'A':
                    case 'a':
                        //Player X--
                        break;
                    case 'S':
                    case 's':
                        //Player Y--
                        break;
                    case 'D':
                    case 'd':
                        //Player X++
                        break;
                }
            }
        }
    }
}
