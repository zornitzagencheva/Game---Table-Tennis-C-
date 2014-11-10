// ********************************
// <copyright file="Data.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace TableTennis
{
    using System;
    using System.IO;

    /// <summary>
    /// Responsible for saving and loading data.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Saves game data to file.
        /// </summary>
        public static void SaveData()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(@"..\..\", @"Data\settings.dat"));
            using (writer)
            {
                // saves type of game (PL1vsPL2 or PL1vsPC)
                writer.WriteLine("gameType " + MenuSettings.gameType);

                // saves game speed
                writer.WriteLine("gameSpeed " + MenuSettings.gameSpeed);

                // saves current game speed (in case ball was hit with center of the racket)
                writer.WriteLine("currentGameSpeed " + MenuSettings.currentGameSpeed);

                // saves current computer probability (in case ball was hit with center of the racket)
                writer.WriteLine("currentProbability " + MenuSettings.currentProbability);

                // saves size of rackets
                writer.WriteLine("racketLength " + MenuSettings.racketLength);

                // saves probability for computer movement
                writer.WriteLine("probability " + MenuSettings.probability);

                // saves position X of the ball
                writer.WriteLine("ballPositionX " + Ball.ballPositionX);

                // saves position Y of the ball
                writer.WriteLine("ballPositionY " + Ball.ballPositionY);

                // saves direction X of the ball
                writer.WriteLine("ballCurrentDirectionX " + Ball.ballCurrentDirectionX);

                // saves direction Y of the ball
                writer.WriteLine("ballCurrentDirectionY " + Ball.ballCurrentDirectionY);

                // saves direction (left or right) of the ball (used for blocking/unblocking movement of players)
                writer.WriteLine("ballDirection " + Ball.ballDirection);

                // saves position of the first racket
                writer.WriteLine("firstRacketY " + Rackets.firtsRacketY);

                // load position of the second racket
                writer.WriteLine("secondRacketY " + Rackets.secondRacketY);

                // saves first player points
                writer.WriteLine("firstPlayerPoints " + Table.firstPlayerPoints);

                // saves sets won by first player
                writer.WriteLine("firstPlayerSetsWon " + Table.firstPlayerSetsWon);

                // saves second player points 
                writer.WriteLine("secondPlayerPoints " + Table.secondPlayerPoints);

                // saves sets won by second player
                writer.WriteLine("secondPlayerSetsWon " + Table.secondPlayerSetsWon);

                // saves which turn is to play service
                writer.WriteLine("firstPlayerService " + Rackets.firstPlayerService);

                // saves set point for first player
                for (int i = 0; i < 5; i++)
                {
                    writer.Write(Table.setScore[0, i]);
                    if (i != 4)
                    {
                        writer.Write(" ");
                    }
                }
                writer.WriteLine();

                // saves sets points for second player
                for (int i = 0; i < 5; i++)
                {
                    writer.Write(Table.setScore[1, i]);
                    if (i != 4)
                    {
                        writer.Write(" ");
                    }
                }
                writer.WriteLine();
                
                // saves how many sets were played
                writer.WriteLine("setCount " + Table.setCount);
            }
        }

        /// <summary>
        /// Loads game data from file.
        /// </summary>
        public static void LoadData()
        {
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(@"..\..\", @"Data\settings.dat"));

                using (reader)
                {
                    // load type of game (PL1vsPL2 or PL1vsPC)
                    string[] line = reader.ReadLine().Split(' ');
                    MenuSettings.gameType = line[1];

                    // load game speed
                    line = reader.ReadLine().Split(' ');
                    MenuSettings.gameSpeed = int.Parse(line[1]);

                    // load current game speed (in case ball was hit with center of the racket)
                    line = reader.ReadLine().Split(' ');
                    MenuSettings.currentGameSpeed = int.Parse(line[1]);

                    // load current computer probability (in case ball was hit with center of the racket)
                    line = reader.ReadLine().Split(' ');
                    MenuSettings.currentProbability = int.Parse(line[1]);

                    // load size of rackets
                    line = reader.ReadLine().Split(' ');
                    MenuSettings.racketLength = int.Parse(line[1]);

                    // load probability for computer movement
                    line = reader.ReadLine().Split(' ');
                    MenuSettings.probability = int.Parse(line[1]);

                    // load position X of the ball
                    line = reader.ReadLine().Split(' ');
                    Ball.ballPositionX = int.Parse(line[1]);

                    // load position Y of the ball
                    line = reader.ReadLine().Split(' ');
                    Ball.ballPositionY = int.Parse(line[1]);

                    // load direction X of the ball
                    line = reader.ReadLine().Split(' ');
                    Ball.ballCurrentDirectionX = int.Parse(line[1]);

                    // load direction Y of the ball
                    line = reader.ReadLine().Split(' ');
                    Ball.ballCurrentDirectionY = int.Parse(line[1]);

                    // load direction (left or right) of the ball (used for blocking/unblocking movement of players)
                    line = reader.ReadLine().Split(' ');
                    Ball.ballDirection = line[1];

                    // load position of the first racket
                    line = reader.ReadLine().Split(' ');
                    Rackets.firtsRacketY = int.Parse(line[1]);

                    // load position of the second racket
                    line = reader.ReadLine().Split(' ');
                    Rackets.secondRacketY = int.Parse(line[1]);

                    // load first player points
                    line = reader.ReadLine().Split(' ');
                    Table.firstPlayerPoints = int.Parse(line[1]);

                    // load sets won by first player
                    line = reader.ReadLine().Split(' ');
                    Table.firstPlayerSetsWon = byte.Parse(line[1]);

                    // load second player points 
                    line = reader.ReadLine().Split(' ');
                    Table.secondPlayerPoints = int.Parse(line[1]);

                    // load sets won by second player
                    line = reader.ReadLine().Split(' ');
                    Table.secondPlayerSetsWon = byte.Parse(line[1]);

                    // load which turn is to play service
                    line = reader.ReadLine().Split(' ');
                    Rackets.firstPlayerService = bool.Parse(line[1]);

                    // load set point for first player
                    line = reader.ReadLine().Split(' ');
                    int i = 0;
                    foreach (string number in line)
                    {
                        Table.setScore[0, i] = int.Parse(number);
                        i++;
                    }
 
                    line = reader.ReadLine().Split(' ');
                    i = 0;
                    foreach (string number in line)
                    {
                        Table.setScore[1, i] = int.Parse(number);
                        i++;
                    }
 
                    // load set count(how many sets were played)
                    line = reader.ReadLine().Split(' ');
                    Table.setCount = byte.Parse(line[1]);
                }
            }
            catch (Exception)
            {
                // if file is missing or if data is corrupted
                FileNotFound();
            }

            // Rackets.NewService();
            Table.DrawTable();
            Table.PrintResult();
            MainProgram.Engine();
        }

        /// <summary>
        /// Restarts game if data cannot be loaded.
        /// </summary>
        public static void FileNotFound()
        {
            Console.Clear();
            Table.firstPass = true;
            Table.DrawTable();
            Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) - 1);
            Console.WriteLine(' ');
            Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
            Console.WriteLine(' ');
            Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 3);
            Console.WriteLine(' ');
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 7, Console.WindowHeight / 2);
            Console.Write("File not found or it is corrupted!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 19, (Console.WindowHeight / 2) + 2);
            Console.Write("Do you want to restart game (y/n)? : ");

            string choise = Console.ReadLine();
            switch (choise)
            {
                case "y":
                    Console.Clear();
                    MainProgram.Main();
                    break;

                case "n":
                    Environment.Exit(0);
                    break;

                default:
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Handles the case when user exits game by ESC. Saves data to file if user requested it.
        /// </summary>
        public static void ExitGame()
        {
            Console.Clear();
            Table.firstPass = true;
            Table.DrawTable();
            Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) - 1);
            Console.WriteLine(' ');
            Console.SetCursorPosition(Console.WindowWidth / 2, (Console.WindowHeight / 2) + 1);
            Console.WriteLine(' ');
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 20, Console.WindowHeight / 2);
            Console.Write("Do you want to save current GAME (y/n)? : ");
            string saveGame = Console.ReadLine();
            if (saveGame == "y")
            {
                Data.SaveData();
            }

            Environment.Exit(0);
        }
    }
}
