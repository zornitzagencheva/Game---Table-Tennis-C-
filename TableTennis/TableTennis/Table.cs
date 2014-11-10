// ********************************
// <copyright file="Table.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace TableTennis
{
    using System;

    /// <summary>
    /// Responsible for drawing of table and result.
    /// </summary>
    public class Table
    {
        public static int offset = 3;
        public static int positionX = 0;
        public static int positionY = 0;
        public static bool firstPass = true;
        public static int winPoints = 11;
        public static int firstPlayerPoints;
        public static byte firstPlayerSetsWon;
        public static int secondPlayerPoints;
        public static byte secondPlayerSetsWon;
        public static byte setCount;
        public static int[,] setScore = new int[2, 5];

        /// <summary>
        /// Draws Table.
        /// </summary>
        public static void DrawTable()
        {
            positionX = 1;
            positionY = offset;

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            // draw this part of the table only once (it does not change during the game)
            if (firstPass)
            {
                // draw first horizontal line
                while (positionX < Console.WindowWidth - 1)
                {
                    Console.SetCursorPosition(positionX, positionY);
                    if ((positionX == 1) || (positionX == Console.WindowWidth - 2))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('-');
                    }

                    positionX++;
                }

                // draw second horizontal line 
                positionX = 1;
                positionY = Console.WindowHeight - 1;
                while (positionX < Console.WindowWidth - 1)
                {
                    Console.SetCursorPosition(positionX, positionY);
                    if ((positionX == 1) || (positionX == Console.WindowWidth - 2))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write('-');
                    }

                    positionX++;
                }
                
                // draw left vertical line
                positionX = 1;
                positionY = offset + 1;
                while (positionY < Console.WindowHeight - 1)
                {
                    Console.SetCursorPosition(positionX, positionY);
                    Console.Write('|');
                    positionY++;
                }

                // draw right vertical line
                positionX = Console.WindowWidth - 2;
                positionY = offset + 1;
                while (positionY < Console.WindowHeight - 1)
                {
                    Console.SetCursorPosition(positionX, positionY);
                    Console.Write('|');
                    positionY++;
                }
            }

            // miiddle line must be redraw during the game, because ball moves over it
            // draw middle vertical line
            positionX = Console.WindowWidth / 2;
            positionY = offset + 1;
            while (positionY < Console.WindowHeight - 1)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.Write("|");
                positionY++;
            }

            firstPass = false;
            Console.ResetColor();
        }

        /// <summary>
        /// Prints results from current set.
        /// </summary>
        public static void PrintResult()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            // prints sets won by second player
            positionX = 2;
            positionY = 1;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("   "); //// clear previos data
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(Table.secondPlayerSetsWon);

            // prints current set result
            positionX = (Console.WindowWidth / 2) - 4;
            positionY = 1;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("        "); //// clear previos data
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("{0,3} : {1}", Table.secondPlayerPoints, Table.firstPlayerPoints);

            // prints sets won by first player
            positionX = Console.WindowWidth - 3;
            positionY = 1;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("   "); //// clear previos data
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(Table.firstPlayerSetsWon);
        }

        /// <summary>
        /// Prints statistic for all sets.
        /// </summary>
        /// <param name="position">Starting position of printing.</param>
        public static void PrintSetsScore(int position)
        {

            int shift = 0;

            positionX = position;
            positionY = (Console.WindowHeight / 2) + 10;
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("#");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(positionX, positionY + 2);
            Console.Write("#");
            Console.ForegroundColor = ConsoleColor.Yellow;
            shift += 2;
           
            for (int j = 0; j < 5; j++)
            {
                if ((setScore[0, j] != 0) || (setScore[1, j] != 0))
                {                
                    Console.SetCursorPosition(positionX + shift, positionY);
                    Console.Write("{0,3} |", setScore[0, j]);
                    Console.SetCursorPosition(positionX + shift, positionY + 1);
                    Console.Write("-----");
                    Console.SetCursorPosition(positionX + shift, positionY + 2);
                    Console.Write("{0,3} |", setScore[1, j]);
                    shift += 5;
                }
            }
       }

        /// <summary>
        /// Checks if set is won. 
        /// </summary>
        /// <param name="pointsA">Points of the scoring player</param>
        /// <param name="pointsB">Points of the other player</param>
        /// <returns>true if scoring player has at least 15 points and difference in points is more than 1.</returns>
        public static bool CheckSetWon(int pointsA, int pointsB)
        {
            bool check = false;
            if ((pointsA >= winPoints) && ((pointsA - pointsB) >= 2))
            {
                check = true;
                Console.Clear();
                PrintResult();
                firstPass = true;
                DrawTable();
            }

            return check;
        }

        /// <summary>
        /// Checks if one of the players win the game by scoring 3 sets.
        /// </summary>
        /// <param name="sets">number of sets won by scoring player</param>
        /// <returns>true if player scores 3 sets</returns>
        public static bool CheckGameOver(int sets)
        {
            bool check = false;
            if (sets >= 3)
            {
                check = true;
                Console.Clear();
                PrintResult();
                firstPass = true;
                DrawTable();
            }

            return check;
        }

        /// <summary>
        /// Init points for both players.
        /// </summary>
        public static void InitPoints()
        {
            firstPlayerPoints = 0;
            secondPlayerPoints = 0;
        }

        /// <summary>
        /// Init sets for both players.
        /// </summary>
        public static void InitSets()
        {
            firstPlayerSetsWon = 0;
            secondPlayerSetsWon = 0;

            // init sets statistic
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                    setScore[i, j] = 0;

            setCount = 0;
        }

        /// <summary>
        /// Adds score of last set to array of played sets.
        /// </summary>
        public static void AddSetScore()
        {
            setScore[0, Table.setCount] = Table.firstPlayerPoints;
            setScore[1, Table.setCount] = Table.secondPlayerPoints;
            setCount++;
        }
    }
}
