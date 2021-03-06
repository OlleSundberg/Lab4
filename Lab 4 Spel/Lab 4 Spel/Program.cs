﻿// High scores (the lower the better)
// Olle: 34
// Viktor: 28 
// Tobias: 7

using System;
using System.Collections.Generic;
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
            Console.Title = "IndianJonas 2 - K: Key, D: Door, M: Monster, ?: Secret, o: Coin, ^: Spike E: Exit.";

            //Skärmstorlekar
            double widthScale = 9; //9
            double heightScale = 5; //5 fullscreen

            int visionMode = 0;
            Console.WriteLine("Would you like (n)ormal vision mode(1) or (H)ARDCORE VISION MODE(2)?");
            startOfVisionSwitch:
            switch (char.Parse(Console.ReadKey(true).KeyChar.ToString().ToUpper()))
            {
                case '1':
                case 'N':
                    visionMode = 1; // Player.check(map);
                    break;
                case '2':
                case 'H':
                    visionMode = 2; // Player.fow(map);
                    break;
                default:
                    goto startOfVisionSwitch;
            }
            Console.Clear();
            Console.Write("What screen resolution would you like? Leave it empty for default.\nWidth? ");

            string input = Console.ReadLine();
            widthScale = 4;
            heightScale = 3;

            if (input != "")
            {
                double.TryParse(input, out widthScale);

                Console.Write("Height? ");
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

            //Game-loop:
            int turns = -1; // -1 då den adderar 1 i början av loopen, så att den faktiskt börjar på 0.
            while (player.HP > 0)
            {
                turns++;

                if (visionMode == 1)
                    player.check(map);
                else
                    player.fow(map, mapWidth, mapHeight);


                //Rita kartan:
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
                                Console.ForegroundColor = ConsoleColor.Blue;
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

                //Visa status, inventory etc
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

                // Gå-kommandon. Kollar om man kan gå innan den försöker flytta. Alla dörrar kan endast låsas upp
                // från höger sida, det är därför som alla dörr specifika handlingar finns på 'case ConsoleKey.A:'.
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
            Console.ReadKey();
        }
    }
}
