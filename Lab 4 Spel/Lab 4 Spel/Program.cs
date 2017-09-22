// Olle:    49
// Viktor:  44
// ???

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Program
    {
        public enum RoomType { Empty, Monster, Door, Exit, Key, Wall, Spike, Secret, Treasure }
        static void Main(string[] args)
        {
            Player player = new Player(12, 3);
            Keys keys = new Keys();
            Random rnd = new Random();

            //Map setup:
            int mapWidth = 14;
            int mapHeight = 5;

            MapBuilder mb = new MapBuilder();
            Room[,] map = mb.BuildMap(mapWidth, mapHeight);

            /*
             01234567890123
           0 ##############
           1 #U D Dn  M  n#
           2 #########  M #
           3 #n     D     #
           4 ##############            
*/

            //Game-loop:
            int turns = -1;
            while (player.HP > 0)
            {
                player.check(map);
                turns++;
                for (double dy = 0; dy < mapHeight; dy += 0.34)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        for (double dx = 0; dx < mapWidth; dx += 0.25)
                        {
                            int y = (int)dy;
                            int x = (int)dx;
                            if (x == player.X && y == player.Y)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write('@');
                            }
                            else if (map[x, y].visible)
                            {
                                Console.ForegroundColor = map[x, y].color;
                                Console.Write(map[x, y].mapIcon);
                            }
                            else {
                                Console.Write(' ');
                            }
                        }
                        Console.WriteLine("");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                if (player.wasHurt)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The monster attacked you!");
                    player.wasHurt = false;
                }
                else if (player.wasSpiked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You stepped on spikes.");
                    player.wasSpiked = false;
                }
                else if (player.foundTreasure)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You found treasure!");
                    player.foundTreasure = false;
                }
                else
                    Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("HP: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(player.HP);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Keys: ");
                if (keys.getRed())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("o--m ");
                }
                if (keys.getGreen())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("o--m ");
                }
                if (keys.getBlue())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("o--m");
                }
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Turns: " + turns);


                // Beskriv rummet (baserat på enum?):

                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'W':
                    case 'w':
                        if (map[player.X, player.Y - 1].type != RoomType.Wall)
                            player.Y--;
                        else
                            turns--;
                        break;
                    case 'A':
                    case 'a':
                        if (map[player.X - 1, player.Y].type == RoomType.Door &&
                            ((map[player.X - 1, player.Y].specialColor == "Green" && keys.getGreen()) ||
                            (map[player.X - 1, player.Y].specialColor == "Red" && keys.getRed())) ||
                            (map[player.X - 1, player.Y].specialColor == "Blue" && keys.getBlue()))
                        {
                            map[player.X - 1, player.Y].type = RoomType.Empty;
                            map[player.X - 1, player.Y].mapIcon = '.';
                        }
                        if (map[player.X - 1, player.Y].type != RoomType.Wall && map[player.X - 1, player.Y].type != RoomType.Door)
                            player.X--;
                        else
                            turns--;
                        break;
                    case 'S':
                    case 's':
                        if (map[player.X, player.Y + 1].type != RoomType.Wall)
                            player.Y++;
                        else
                            turns--;
                        break;
                    case 'D':
                    case 'd':
                        if (map[player.X + 1, player.Y].type != RoomType.Wall)
                            player.X++;
                        else
                            turns--;
                        break;
                    default:
                        turns--;
                        break;
                }

                if (map[player.X, player.Y].type == RoomType.Key)
                {
                    if (map[player.X, player.Y].specialColor == "Green")
                        keys.setGreen(true);
                    else if (map[player.X, player.Y].specialColor == "Red")
                        keys.setRed(true);
                    else
                        keys.setBlue(true);
                    map[player.X, player.Y].type = RoomType.Empty;
                    map[player.X, player.Y].mapIcon = '.';
                    map[player.X, player.Y].color = ConsoleColor.Gray;
                }
                if (map[player.X, player.Y].type == RoomType.Monster)
                {
                    turns += 3;
                    player.HP -= 20;
                    player.wasHurt = true;
                }
                else if (map[player.X, player.Y].type == RoomType.Secret)
                {
                    if (rnd.Next(3) <= 1)
                    {
                        map[player.X, player.Y] = new SpikeRoom(player.X, player.Y);
                        player.HP -= 25;
                        player.wasSpiked = true;
                        turns++;
                    }
                    else
                    {
                        map[player.X, player.Y] = new TreasureRoom(player.X, player.Y);
                        turns -= 3;
                        player.foundTreasure = true;
                    }
                }
                else if (map[player.X, player.Y].type == RoomType.Spike)
                {
                    player.HP -= 25;
                    player.wasSpiked = true;
                    turns++;
                }

                if (player.X == 1 && player.Y == 1)
                    winGame(turns);
                player.check(map);
                Console.Clear();
            }
            Console.WriteLine("Game over. You died.");
        }
        static void winGame(int turns)
        {
            turns++;
            Console.Clear();
            Console.WriteLine("Congratulations, you win! Turns: " + turns);
            Environment.Exit(0);
        }
    }
}
