// Olle: 34
// Viktor: 28 
// 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Spel
{
    class Program
    {
        static Player player;

        public enum RoomType { Empty, Monster, Door, Exit, Key, Wall, Spike, Secret, Treasure }
        static void Main(string[] args)
        {
            double widthScale = 9; //9
            double heightScale = 5; //5 fullscreen

            int visionMode = 0;
            Console.WriteLine("Would you like (n)ormal vision mode(1) or (H)ARDCORE VISION MODE(2)?");
            startOfSwitch:
            switch (char.Parse(Console.ReadKey(true).KeyChar.ToString().ToUpper()))
            {
                case '1':
                case 'N':
                    visionMode = 1;
                    break;
                case '2':
                case 'H':
                    visionMode = 2;
                    break;
                default:
                    goto startOfSwitch;
            }
            Console.Clear();
            Console.WriteLine("What screen resolution would you like? Leave it empty for default. Width? ");

            string input = Console.ReadLine();
            widthScale = 4;
            heightScale = 3;

            if (input != "")
            {
                double.TryParse(input, out widthScale);

                Console.WriteLine("Height? ");
                input = Console.ReadLine();
                double.TryParse(input, out heightScale);
            }
            Console.Clear();

            player = new Player(12, 3);
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
                if (visionMode == 1)
                    player.check(map);
                else
                    player.fow(map, mapWidth, mapHeight);

                turns++;
                for (double dy = 0; dy < mapHeight; dy += 1 / heightScale + 0.01)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        for (double dx = 0; dx < mapWidth; dx += 1 / widthScale + 0.01)
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
               
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        if (map[player.X, player.Y - 1].type != RoomType.Wall)
                            player.Y--;
                        else
                            turns--;
                        break;
                    case ConsoleKey.A:
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
                    case ConsoleKey.S:
                        if (map[player.X, player.Y + 1].type != RoomType.Wall)
                            player.Y++;
                        else
                            turns--;
                        break;
                    case ConsoleKey.D:
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

                if (visionMode == 1)
                    player.check(map);
                else
                    player.fow(map, mapWidth, mapHeight);

                Console.Clear();
            }
            Console.WriteLine("Game over. You died.");
        }
        static void winGame(int turns)
        {
            turns++;
            Console.Clear();
            Console.WriteLine("Congratulations, you win! Score: " + (turns - player.HP - 4) + ". (The lower the better, '1' is the best)");
            Environment.Exit(0);
        }
    }
}
