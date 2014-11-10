// ********************************
// <copyright file="MenuSettings.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace TableTennis
{
    using System;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Contains methods used for setting game type and difficulty or load previously saved game.
    /// </summary>
    class MenuSettings
    {
        public static string gameType;
        public static int gameSpeed;
        public static int racketLength;
        public static int probability;
        public static string difficulty;
        public static int currentGameSpeed;
        public static int currentProbability;

        /// <summary>
        /// Set innitialy settings for Console Window size.
        /// </summary>        
        public static void Settings()
        {
            // all settings for the table can be added at an additional method or class Table
            Console.SetWindowSize(90, 30);
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.Title = "Table Tennis by Cruella de Vil Team";
            Console.CursorVisible = false; // makes the cursor invisible

            // ball directions
            Ball.ballHorizontalDirection = new int[3] { -1, 0, 1 }; // [0] - left, [1] - hold, [2] - right;
            Ball.ballVerticalDirection = new int[3] { -1, 0, 1 }; // [0] - up, [1] - hold, [2] - down;

            // points and sets init
            Table.InitPoints();
            Table.InitSets();
        }

        /// <summary>
        /// Show itro screen, using external txt file.
        /// </summary>        
        public static void IntroScreen()
        {
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(@"..\..\", @"Data\introScreen.txt"));
                using (reader)
                {
                    string line = string.Empty;
                    char symbol;
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }

                        for (int i = 0; i < line.Length; i++)
                        {
                            symbol = line[i];
                            if (symbol == '*')
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(symbol);
                            }
                            else if (symbol == '#')
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(symbol);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write(symbol);
                            }
                        }

                        Console.WriteLine();
                    }
                }
            }
            catch (FileNotFoundException fe)
            {
                Console.WriteLine("The txt file for intro is missing {0}", fe.Message);
            }

            Console.ReadKey();
            Intro();
        }

        /// <summary>
        /// Collect user choice about begining of the game.
        /// </summary>        
        public static void Intro()
        {
            ConsoleKeyInfo choise;
            bool commandCorrect = false;
            while (!commandCorrect)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 6, (Console.WindowHeight / 2) - 5);
                Console.WriteLine("TABLE TENNIS");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) - 3);
                Console.WriteLine("Please choose:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 11, (Console.WindowHeight / 2) - 1);
                Console.WriteLine("New game  - press [Enter]");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 11, Console.WindowHeight / 2);
                Console.WriteLine("Load game - press [L]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 11, (Console.WindowHeight / 2) + 1);
                Console.WriteLine("Exit game - press [Esc]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) + 3);
                Console.WriteLine("Enter command:");

                choise = Console.ReadKey(true);

                switch (choise.Key)
                {
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[New game]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        Console.Clear();
                        PlayersChoice();
                        commandCorrect = true;
                        break;

                    case ConsoleKey.L:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 6, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[Load game]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        Console.Clear();
                        Data.LoadData();
                        commandCorrect = true;
                        break;

                    case ConsoleKey.Escape:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 6, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[Exit game]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        Environment.Exit(0);
                        commandCorrect = true;
                        break;

                    default:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 6, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Wrong Input!");
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) + 6);
                        Console.WriteLine("Please try again!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        /// <summary>
        /// Collect user choice about Player type.
        /// </summary>        
        public static void PlayersChoice()
        {
            ConsoleKeyInfo choise;
            bool commandCorrect = false;

            while (!commandCorrect)
            {
                // intro in console
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) - 3);
                Console.WriteLine("Please choose:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 16, (Console.WindowHeight / 2) - 1);
                Console.WriteLine("Player 1 vs Player 2 - press [1]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 16, Console.WindowHeight / 2);
                Console.WriteLine("Player 1 vs Computer - press [2]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) + 2);
                Console.WriteLine("Enter command:");

                choise = Console.ReadKey(true);

                switch (choise.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 11, (Console.WindowHeight / 2) + 4);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[Player 1 vs Player 2]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        gameType = "PL1vsPL2";
                        Console.Clear();
                        Dificulties();
                        commandCorrect = true;
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 11, (Console.WindowHeight / 2) + 4);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[Player 1 vs Computer]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        gameType = "PL1vsPC";
                        Console.Clear();
                        Dificulties();
                        commandCorrect = true;
                        break; // LoadGameIncomplete(); Engine();

                    default:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 6, (Console.WindowHeight / 2) + 4);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Wrong Input!");
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) + 5);
                        Console.WriteLine("Please try again!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        /// <summary>
        /// Collect user choice and set value to
        /// global variables - difficulty, probability, gameSpeed, racketLength.
        /// </summary> 
        /// <param name="difficulty">Determine 3 type game difficulty easy/med/high.</param>
        /// <param name="probability">Determine the probability to hit the ball with different part of racket.</param>
        /// <param name="gameSpeed">Set speed of movement in game.</param>
        /// <param name="racketLength">Determine the size of players pad.</param>
        public static void Dificulties()
        {
            ConsoleKeyInfo choise;
            bool commandCorrect = false;

            while (!commandCorrect)
            {
                // intro in console
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 20, (Console.WindowHeight / 2) - 3);
                Console.WriteLine("Please choose the difficulty of the game:");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 9, (Console.WindowHeight / 2) - 1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Easy   - press [1]");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 9, Console.WindowHeight / 2);
                Console.WriteLine("Medium - press [2]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 9, (Console.WindowHeight / 2) + 1);
                Console.WriteLine("Hard   - press [3]");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) + 3);
                Console.WriteLine("Enter command:");

                choise = Console.ReadKey(true);

                switch (choise.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 3, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[Easy]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        Console.Clear();
                        difficulty = "easy";
                        probability = 80;
                        currentProbability = probability;
                        gameSpeed = 55;
                        currentGameSpeed = gameSpeed;
                        racketLength = 9;
                        Controls();
                        commandCorrect = true;
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 4, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[Medium]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        Console.Clear();
                        difficulty = "med";
                        probability = 85;
                        currentProbability = probability;
                        gameSpeed = 45;
                        currentGameSpeed = gameSpeed;
                        racketLength = 7;
                        Controls();
                        commandCorrect = true;
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 3, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[Hard]");
                        Console.ResetColor();
                        Thread.Sleep(500);

                        Console.Clear();
                        difficulty = "hard";
                        probability = 90;
                        currentProbability = probability;
                        gameSpeed = 35;
                        currentGameSpeed = gameSpeed;
                        racketLength = 5;
                        Controls();
                        commandCorrect = true;
                        break;

                    default:
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 6, (Console.WindowHeight / 2) + 5);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Wrong Input!");
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 8, (Console.WindowHeight / 2) + 6);
                        Console.WriteLine("Please try again!");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        /// <summary>
        /// Shows user the control keys of the game.
        /// </summary>        
        public static void Controls()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 16, (Console.WindowHeight / 2) - 10);
            Console.WriteLine("THIS IS THE CONTROL KEYS OF THE GAME: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) - 6);
            Console.WriteLine("PLAYER 1  -> UP    - \'UP Arrow\'");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) - 5);
            Console.WriteLine("PLAYER 1  -> DOWN  - \'DOWN Arrow\'");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) - 3);
            Console.WriteLine("PLAYER 2  -> UP    - \'W\'");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) - 2);
            Console.WriteLine("PLAYER 2  -> DOWN  - \'S\'");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, Console.WindowHeight / 2);
            Console.WriteLine("PAUSE     -> press - \'Spacebar\'");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) + 1);
            Console.WriteLine("QUIT GAME -> press - \'Esc\'");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) + 3);
            Console.WriteLine("START NEW GAME - press ANY KEY ");
            Console.CursorVisible = false;
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}
