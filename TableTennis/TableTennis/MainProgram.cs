// ********************************
// <copyright file="TableTennis.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace TableTennis
{
    using System;
    using System.Threading;

    /// <summary>
    /// Responsible for starting the game.
    /// </summary>
    public class MainProgram
    {
        /// <summary>
        /// Engine of table tennis game.
        /// </summary>
        public static void Engine()
        {
            while (true)
            {
                Ball.HitFirstRacket();
                Ball.HitSecondRacket();
                Ball.HitWall(); // check if the ball is in on the table
                Ball.MoveBall(); // move the ball in specific direction

                if ((Ball.ballDirection == "Left") && (MenuSettings.gameType == "PL1vsPL2"))
                {
                    Rackets.MoveSecondPlayer();
                }
                else if (Ball.ballDirection == "Right")
                {
                    Rackets.MoveFirstPlayer();
                }
                else
                {
                    Rackets.ComputerMoveSecondRacket();
                }

                Rackets.DrawFirstRacket();
                Rackets.DrawSecondRacket();
                Table.DrawTable();

                Thread.Sleep(MenuSettings.gameSpeed);
            }
        }

        /// <summary>
        /// Entry point of the game.
        /// </summary>
        public static void Main()
        {
            MenuSettings.Settings();
            MenuSettings.IntroScreen();
            Rackets.NewService();
            Engine();
        }
    }
}