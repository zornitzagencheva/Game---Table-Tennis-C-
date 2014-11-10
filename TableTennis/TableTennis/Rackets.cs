// ********************************
// <copyright file="Rackets.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace TableTennis
{
    using System;
    using System.Threading;

    /// <summary>
    /// Responsible for drawing and moving rackets.
    /// </summary>
    public class Rackets
    {
        public static int firtsRacketY;
        public static int secondRacketY;
        public static readonly int firstRacketX = Console.WindowWidth - 4;
        public static readonly int secondRacketX = 3;
        public static bool firstPlayerService = true;
        public static ConsoleKeyInfo key;
        public static Random randomGenerator = new Random(); //// Move second racket from computer.

        /// <summary>
        /// Move second rackets from computer if it is user choice. The movement principles is based on the Random Generator.
        /// Probability variable is responsible for the accuracy of movement powered by computer - if the level is easy the 
        /// probability is 80, it is 'med' the probality is 85 and it is hard the probability is 90.
        /// </summary>  
        public static void ComputerMoveSecondRacket()
        {
            int randomNumber = randomGenerator.Next(1, 101);

            if (randomNumber <= MenuSettings.probability)
            {
                if (secondRacketY >= Ball.ballPositionY)
                {
                    if (secondRacketY >= Table.offset + 2)
                    {
                        MoveSecondRacketUp();
                    }
                }
                else if ((secondRacketY + (MenuSettings.racketLength / 2)) < Ball.ballPositionY)
                {
                    if ((secondRacketY + MenuSettings.racketLength) < (Console.WindowHeight - 1))
                    {
                        MoveSecondRacketDown();
                    }
                }
            }

            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
                //// check for ESC and Spacebar
                SpecialKeys();
            }
        }

        /// <summary>
        /// Move second rackets down.
        /// </summary>  
        public static void MoveSecondRacketDown()
        {
            secondRacketY++;
            Console.SetCursorPosition(secondRacketX, secondRacketY - 1);
            Console.Write(' ');
            DrawSecondRacket();
        }

        /// <summary>
        /// Move second rackets up.
        /// </summary>  
        public static void MoveSecondRacketUp()
        {
            secondRacketY--;
            Console.SetCursorPosition(secondRacketX, secondRacketY + MenuSettings.racketLength);
            Console.Write(' ');
            DrawSecondRacket();
        }

        /// <summary>
        /// Draw first racket.
        /// </summary>  
        public static void DrawFirstRacket()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = firtsRacketY; i < firtsRacketY + MenuSettings.racketLength; i++)
            {
                // change symbol color for the center of the racket
                if (i == firtsRacketY + (MenuSettings.racketLength / 2))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(firstRacketX, i);
                    Console.Write('#');
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.SetCursorPosition(firstRacketX, i);
                    Console.Write('#');
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Draw second racket.
        /// </summary>  
        public static void DrawSecondRacket()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = secondRacketY; i < secondRacketY + MenuSettings.racketLength; i++)
            {
                // change symbol color for the center of the racket
                if (i == secondRacketY + (MenuSettings.racketLength / 2))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(secondRacketX, i);
                    Console.Write('#');
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.SetCursorPosition(secondRacketX, i);
                    Console.Write('#');
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Move first racket.
        /// </summary> 
        public static void MoveFirstPlayer()
        {
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                FirstPlayerExecuteKey(key);
            }
        }

        /// <summary>
        /// Move second racket.
        /// </summary> 
        public static void MoveSecondPlayer()
        {
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                SecondPlayerExecuteKey(key);
            }
        }

        /// <summary>
        /// Rull first racket with keys: UpArrow and DownArrow.
        /// </summary> 
        public static void FirstPlayerExecuteKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow)
            {
                if (firtsRacketY >= Table.offset + 2)
                {
                    firtsRacketY--;
                    Console.SetCursorPosition(firstRacketX, firtsRacketY + MenuSettings.racketLength);
                    Console.Write(' ');
                    DrawFirstRacket();
                }
            }

            if (key.Key == ConsoleKey.DownArrow)
            {
                if (firtsRacketY + MenuSettings.racketLength < Console.WindowHeight - 1)
                {
                    firtsRacketY++;
                    Console.SetCursorPosition(firstRacketX, firtsRacketY - 1);
                    Console.Write(' ');
                    DrawFirstRacket();
                }
            }

            //// check for ESC and Spacebar
            SpecialKeys();
        }

        /// <summary>
        /// Move second racket with keys: W and S (if user choise is player-player game).
        /// </summary> 
        public static void SecondPlayerExecuteKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.W)
            {
                if (secondRacketY >= Table.offset + 2)
                {
                    secondRacketY--;
                    Console.SetCursorPosition(secondRacketX, secondRacketY + MenuSettings.racketLength);
                    Console.Write(' ');
                    //// MoveSecondRacketUp();
                    DrawSecondRacket(); 
                }
            }

            if (key.Key == ConsoleKey.S)
            {
                if (secondRacketY + MenuSettings.racketLength < Console.WindowHeight - 1)
                {
                    secondRacketY++;
                    Console.SetCursorPosition(secondRacketX, secondRacketY - 1);
                    Console.Write(' ');
                    //// MoveSecondRacketDown();
                    DrawSecondRacket(); 
                }
            }

            //// check for ESC or Spacebar
            SpecialKeys();
        }

        /// <summary>
        /// Plays service. Poistioning two rackets in both corners of the table.
        /// Draws ball in front of the player who will serve.
        /// </summary>
        public static void NewService()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Table.firstPass = true;
            Table.DrawTable();
            Table.PrintResult();
            firtsRacketY = Table.offset + 1; //// rackets are in top right and bottom left corners
            secondRacketY = Console.WindowHeight - MenuSettings.racketLength - 1;
            DrawFirstRacket();
            DrawSecondRacket();

            if (firstPlayerService)
            {
                // player one service
                Ball.ballPositionX = Console.WindowWidth - 5;
                Ball.ballPositionY = Table.offset + (MenuSettings.racketLength / 2) + 1;
                Ball.ballCurrentDirectionX = 0; // left
                Ball.ballCurrentDirectionY = 2; // down
                firstPlayerService = false; // next time player two will start
                Ball.ballDirection = "Left";
            }
            else
            {
                // player two service
                Ball.ballPositionX = 4;
                Ball.ballPositionY = Console.WindowHeight - (MenuSettings.racketLength / 2) - 2;
                Ball.ballCurrentDirectionX = 2; // right
                Ball.ballCurrentDirectionY = 0; // up
                firstPlayerService = true; // next time player one will start
                Ball.ballDirection = "Right";
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Ball.ballPositionX, Ball.ballPositionY);
            Console.Write(Ball.ballSymbol);
            Thread.Sleep(2000); //// wait 2 seconds before each new service starts  // old value 4000
        }

        /// <summary>
        /// Checks if special key is pressed.
        /// Spacebar for pause, ESC for exit.
        /// </summary>
        public static void SpecialKeys()
        {
            if (key.Key == ConsoleKey.Escape)
            {
                // exit game with escape
                Data.ExitGame();
            }

            if (key.Key == ConsoleKey.Spacebar)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 2, Console.WindowHeight / 2);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("PAUSE");
                do
                {
                    key = Console.ReadKey(true);
                }
                while (key.Key != ConsoleKey.Spacebar);
                Console.SetCursorPosition((Console.WindowWidth / 2) - 2, Console.WindowHeight / 2);
                Console.Write("     ");
                Console.ResetColor();
            }
        }
    }
}
