// ********************************
// <copyright file="Sounds.cs" company="Telerik Academy">
// Copyright (c) 2013 Telerik Academy. All rights reserved.
// </copyright>
// <author>Cruella de Vil Team</author>
// ********************************
namespace Sounds
{
    using System;
    using System.IO;
    using System.Media;

    /// <summary>
    /// Responsible for sounds.
    /// </summary>
    public class Sounds
    {
        static string path = @"..\..\";

        public static void PadHit()
        {
            string ballPath = @"Sounds\Ball.wav";
            using (SoundPlayer padHit = new SoundPlayer(Path.Combine(path, ballPath)))
            {
                padHit.Play();
            }
        }

        /// <summary>
        /// Plays sound when one of the players scores.
        /// </summary>
        public static void MakePoints()
        {
            string pointsPath = @"Sounds\MakePoint3.wav";
            using (SoundPlayer makePoint3 = new SoundPlayer(Path.Combine(path, pointsPath)))
            {
                makePoint3.Play();
            }
        }

        /// <summary>
        /// Plays sound when ball hits wall.
        /// </summary>
        public static void WallHit()
        {
            string wallPath = @"Sounds\Wall.wav";
            using (SoundPlayer wallHit = new SoundPlayer(Path.Combine(path, wallPath)))
            {
                wallHit.Play();
            }
        }

        /// <summary>
        /// Plays sound when one of the players win set.
        /// </summary>
        public static void Clapping()
        {
            string clapPath = @"Sounds\Clapping.wav";
            using (SoundPlayer clapSound = new SoundPlayer(Path.Combine(path, clapPath)))
            {
                clapSound.Play();
            }
        }

        /// <summary>
        /// Plays sound when one of the players hits ball with center of the racket.
        /// </summary>
        public static void HitCenter()
        {
            string hitPath = @"Sounds\CenterHit.wav";
            using (SoundPlayer centerHit = new SoundPlayer(Path.Combine(path, hitPath)))
            {
                centerHit.Play();
            }
        }

        /// <summary>
        /// Plays sound when one of the players wins three sets.
        /// </summary>
        public static void WinThreeSets()
        {
            string hitPath = @"Sounds\WinWholeGame.wav";
            using (SoundPlayer winThreeSets = new SoundPlayer(Path.Combine(path, hitPath)))
            {
                winThreeSets.Play();
            }
        }
    }
}