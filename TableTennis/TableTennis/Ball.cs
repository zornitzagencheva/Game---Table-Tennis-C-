// ********************************
// <copyright file="Ball.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace TableTennis
{
    using System;
    using System.Threading;
    using Sounds;

    /// <summary>
    /// Contains methods used for movement of the ball.
    /// </summary>
    public class Ball
    {
        public static int ballPositionX;
        public static int ballPositionY;
        public static int[] ballHorizontalDirection;
        public static int[] ballVerticalDirection;
        public static int ballCurrentDirectionX;
        public static int ballCurrentDirectionY;
        public static string ballDirection;
        public static char ballSymbol = '@';

        /// <summary>
        /// Draws ball symbol on current position.
        /// </summary>
        public static void MoveBall()
        {
            Console.SetCursorPosition(ballPositionX, ballPositionY);
            Console.Write(' ');

            ballPositionX = ballPositionX + ballHorizontalDirection[ballCurrentDirectionX];
            ballPositionY = ballPositionY + ballVerticalDirection[ballCurrentDirectionY];

            Console.SetCursorPosition(ballPositionX, ballPositionY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(ballSymbol);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes the direction of the ball when hits the first player's racket.
        /// </summary>
        public static void HitFirstRacket()
        {
            if ((ballDirection == "Right")
                && (ballPositionX + 1 == Rackets.firstRacketX
                && ballPositionY >= Rackets.firtsRacketY
                && ballPositionY <= Rackets.firtsRacketY + MenuSettings.racketLength - 1))
            {
                if (ballPositionX + 1 == Rackets.firstRacketX
                    && ballPositionY == Rackets.firtsRacketY + (MenuSettings.racketLength / 2))
                {
                    // hit center of the racket
                    ChangeBallDirX();
                    ballCurrentDirectionY = 1;
                    PowerStrike();
                    Sounds.HitCenter();
                }
                else if (ballPositionX + 1 == Rackets.firstRacketX
                    && ballPositionY >= Rackets.firtsRacketY
                    && ballPositionY <= Rackets.firtsRacketY + (MenuSettings.racketLength / 2) - 1)
                {
                    // hit upper part of the racket
                    Sounds.PadHit();
                    ChangeBallDirX();
                    ChangeBallDirYUp();
                    BackToNormalGameSpeedAndDifficulty();
                }
                else
                {
                    // hit lower part of the racket
                    Sounds.PadHit();
                    ChangeBallDirX();
                    ChangeBallDirYDown();
                    BackToNormalGameSpeedAndDifficulty();
                }

                ballDirection = "Left";
            }

            /* check diagonals
             * from above
             *          
             *          @
             *           #
             *           #
             *           #
             *           #
             *           #
             * 
             */
            if ((ballPositionX + 1 == Rackets.firstRacketX)
                && (ballPositionY == Rackets.firtsRacketY - 1)
                && (ballCurrentDirectionY == 2))
            {
                Sounds.PadHit();
                ChangeBallDirX();
                ChangeBallDirYUp();
                ballDirection = "Left";
            }

            /* from below
             *           #
             *           #
             *           #
             *           #
             *           #
             *          @
             * 
             */
            if ((ballPositionX + 1 == Rackets.firstRacketX)
                && (ballPositionY == Rackets.firtsRacketY + MenuSettings.racketLength)
                && (ballCurrentDirectionY == 0))
            {
                Sounds.PadHit();
                ChangeBallDirX();
                ChangeBallDirYDown();
                ballDirection = "Left";
            }
        }

        /// <summary>
        /// Changes the direction of the ball when hits the second player's racket.
        /// </summary>
        public static void HitSecondRacket()
        {
            if ((ballDirection == "Left")
                && (ballPositionX - 1 == Rackets.secondRacketX
                && ballPositionY >= Rackets.secondRacketY
                && ballPositionY <= Rackets.secondRacketY + MenuSettings.racketLength - 1))
            {
                if (ballPositionX - 1 == Rackets.secondRacketX
                    && ballPositionY == Rackets.secondRacketY + (MenuSettings.racketLength / 2))
                {
                    // hit center of the racket
                    ChangeBallDirX();
                    ballCurrentDirectionY = 1;
                    PowerStrike();
                    Sounds.HitCenter();
                }
                else if (ballPositionX - 1 == Rackets.secondRacketX
                    && ballPositionY >= Rackets.secondRacketY
                    && ballPositionY <= Rackets.secondRacketY + (MenuSettings.racketLength / 2) - 1)
                {
                    // hit upper part of the racket
                    Sounds.PadHit();
                    ChangeBallDirX();
                    ChangeBallDirYUp();
                    BackToNormalGameSpeedAndDifficulty();
                }
                else
                {
                    // hit lower part of the racket
                    Sounds.PadHit();
                    ChangeBallDirX();
                    ChangeBallDirYDown();
                    BackToNormalGameSpeedAndDifficulty();
                }

                ballDirection = "Right";
            }

            /* check diagonals
             * from above
             *          
             *            @
             *           #
             *           #
             *           #
             *           #
             *           #
             * 
             * */
            if ((ballPositionX - 1 == Rackets.secondRacketX)
                && (ballPositionY == Rackets.secondRacketY - 1)
                && (ballCurrentDirectionY == 2))
            {
                Sounds.PadHit();
                ChangeBallDirX();
                ChangeBallDirYUp();
                ballDirection = "Right";
            }

            /* from below
             *           #
             *           #
             *           #
             *           #
             *           #
             *            @
             * 
             * */
            if ((ballPositionX - 1 == Rackets.secondRacketX)
                && (ballPositionY == Rackets.secondRacketY + MenuSettings.racketLength)
                && (ballCurrentDirectionY == 0))
            {
                Sounds.PadHit();
                ChangeBallDirX();
                ChangeBallDirYDown();
                ballDirection = "Right";
            }
        }

        /// <summary>
        /// Changes the direction of the ball when hits a wall.
        /// </summary>
        public static void HitWall()
        {
            if (ballPositionX <= 1)
            {
                Sounds.MakePoints();
                //// first player scores
                Table.firstPlayerPoints++;
                BackToNormalGameSpeedAndDifficulty();

                if (Table.CheckSetWon(Table.firstPlayerPoints, Table.secondPlayerPoints))
                {
                    // check if set is won (player has at least 15 points and difference in points is more than 1)
                    Table.AddSetScore();
                    Table.InitPoints();
                    Table.firstPlayerSetsWon++;

                    if (Table.CheckGameOver(Table.firstPlayerSetsWon))
                    {
                        // check if player won 3 sets -> game over
                        Console.SetCursorPosition((Console.WindowWidth / 2) + 6, Console.WindowHeight / 2);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("GAME won by first Player!");
                        Table.PrintSetsScore((Console.WindowWidth / 2) + 6);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Sounds.WinThreeSets(); // Sounds Win Whole Game 10 sec
                        Thread.Sleep(10000); // old value 5000 
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition((Console.WindowWidth / 2) + 6, Console.WindowHeight / 2);
                        Console.Write("Set won by first Player!");
                        Console.SetCursorPosition((Console.WindowWidth / 2) + 6, (Console.WindowHeight / 2) + 2);
                        Console.Write("New set will begin in 5 sec...");
                        Table.PrintSetsScore((Console.WindowWidth / 2) + 6);
                        Console.ResetColor();
                        Sounds.Clapping(); // Sounds Set Win 5 sec
                        Thread.Sleep(5000);
                    }
                }

                ChangeBallDirX();
                Rackets.NewService();
            }

            if (ballPositionX >= Console.WindowWidth - 2)
            {
                // second player scores
                Table.secondPlayerPoints++;
                Sounds.MakePoints();
                BackToNormalGameSpeedAndDifficulty();

                if (Table.CheckSetWon(Table.secondPlayerPoints, Table.firstPlayerPoints))
                {
                    // check if set is won (player has at least 15 points and difference in points is more than 1)
                    Table.AddSetScore();
                    Table.InitPoints();
                    Table.secondPlayerSetsWon++;

                    if (Table.CheckGameOver(Table.secondPlayerSetsWon))
                    {
                        // check if player won 3 sets -> game over
                        Console.SetCursorPosition(8, Console.WindowHeight / 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("GAME won by second Player!");
                        Table.PrintSetsScore(8);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Sounds.WinThreeSets(); // Win Sounds Whole Game 10 sec 
                        Thread.Sleep(10000);  // old value 5000
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(8, Console.WindowHeight / 2);
                        Console.Write("Set won by second Player!");
                        Console.SetCursorPosition(8, (Console.WindowHeight / 2) + 2);
                        Console.Write("New set will begin in 5 sec...");
                        Table.PrintSetsScore(8);
                        Console.ResetColor();
                        Sounds.Clapping();  // Win Set 5 sec
                        Thread.Sleep(5000);
                    }
                }

                ChangeBallDirX();
                Rackets.NewService();
            }

            if (ballPositionY <= Table.offset + 1)
            {
                Sounds.WallHit();
                ChangeBallDirY();
            }

            if (ballPositionY >= Console.WindowHeight - 2)
            {
                Sounds.WallHit();
                ChangeBallDirY();
            }
        }

        /// <summary>
        /// Changes the direction of the ball to opposite side.
        /// </summary>
        public static void ChangeBallDirX()
        {
            if (ballCurrentDirectionX == 0)
            {
                ballCurrentDirectionX = 2;
            }
            else
            {
                ballCurrentDirectionX = 0;
            }
        }

        /// <summary>
        /// Changes the direction of the ball to opposite side.
        /// </summary>
        public static void ChangeBallDirY()
        {
            if (ballCurrentDirectionY == 0)
            {
                ballCurrentDirectionY = 2;
            }
            else
            {
                ballCurrentDirectionY = 0;
            }
        }

        /// <summary>
        /// Changes the direction of the ball when hits upper part of the racket.
        /// </summary>
        public static void ChangeBallDirYUp()
        {
            ballCurrentDirectionY = 0;
        }

        /// <summary>
        /// Changes the direction of the ball when hits lower part of the racket.
        /// </summary>
        public static void ChangeBallDirYDown()
        {
            ballCurrentDirectionY = 2;
        }

        /// <summary>
        /// Increases the speed of the ball and decreases the probability of the computer to hit the ball.
        /// </summary>
        public static void PowerStrike()
        {
            if (MenuSettings.difficulty == "easy")
            {
                MenuSettings.gameSpeed = 15;
                MenuSettings.probability = 5;
            }
            else if (MenuSettings.difficulty == "med")
            {
                MenuSettings.gameSpeed = 10;
                MenuSettings.probability = 10;
            }
            else
            {
                // hard
                MenuSettings.gameSpeed = 5;
                MenuSettings.probability = 15;
            }
        }

        /// <summary>
        /// Back to normal after Power Strike.
        /// </summary>
        public static void BackToNormalGameSpeedAndDifficulty()
        {
            MenuSettings.gameSpeed = MenuSettings.currentGameSpeed;
            MenuSettings.probability = MenuSettings.currentProbability;
        }
    }
}