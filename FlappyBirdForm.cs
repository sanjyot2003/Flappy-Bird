﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class FlappyBirdForm : Form
    {
        private int gravity = 13;
        private int pipeSpeed = 8;
        private int playerScore = 0;
        private int firstCloudSpeed = 6;
        private int secondCloudSpeed = 4;
        private bool startGame = false;

        public FlappyBirdForm()
        {
            InitializeComponent();
        }

        private void gameKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                if(!startGame)
                {
                    restart();
                    gameTimer.Start();
                    startGame = true;
                }
                gravity = -15;
            }
        }

        private void gameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                gravity = 15;
            }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            cloudFirst.Left -= firstCloudSpeed;
            cloudSecond.Left -= secondCloudSpeed;
            score.Text = $"Score : {playerScore}";

            if(pipeBottom.Left < -110)
            {
                playerScore++;
                pipeBottom.Left = 700;
            }

            if (pipeTop.Left < -120)
            {
                playerScore++;
                pipeTop.Left = 500;
            }

            if(cloudFirst.Left < -144)
            {
                cloudFirst.Left = 745;
            }

            if (cloudSecond.Left < -275)
            {
                cloudSecond.Left = 880;
            }

            if (flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) ||
                flappyBird.Top <= 0
                )
            {
                endGame();
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            score.Text = $"Score : {playerScore} ---Game Over---";
            startGame = false;
        }

        private void restart()
        {
            playerScore = 0;
            cloudFirst.Location = new Point(3, 47);
            cloudSecond.Location = new Point(546, 3);
            flappyBird.Location = new Point(103, 212);
            pipeTop.Location = new Point(440, -101);
            pipeBottom.Location = new Point(337, 402);
        }
    }
}
