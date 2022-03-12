using ConsoleLinkedSnake.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace ConsoleLinkedSnake
{
    class Program
    {
        private static Timer gameTimer;
        private static Snake Snake { get; set; }

        private static Board board;
        private static int boardSizeX;
        private static int boardSizeY;

        private static readonly int gameSpeedStart = 300;
        private static int gameSpeed = gameSpeedStart;
        private static bool gameSpeedChanged = true;
        private static readonly int gameSpeedIncrease = 10;

        private static List<Point> foodList = new();

        private static SnakeUI ui;
        static void Main()
        {
            Console.Title = "Console Snake, by Lars Holen";
            Console.Clear();
            Console.CursorVisible = false;


            boardSizeX = 70;
            boardSizeY = 25;
            ui = new(boardSizeX + 2, 10);
            ui.DrawUI();
            gameSpeedChanged = false;

            board = new(boardSizeX, boardSizeY);
            board.DrawWalls();
            Snake = new(20, 20, boardSizeX, boardSizeY, 'O');
            SetGameTimer();
            AddFood();


            // Test for key input
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                switch (cki.Key)
                {

                    case ConsoleKey.LeftArrow:
                        Snake.MoveX = -1;
                        Snake.MoveY = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        Snake.MoveX = 0;
                        Snake.MoveY = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        Snake.MoveX = 1;
                        Snake.MoveY = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        Snake.MoveX = 0;
                        Snake.MoveY = 1;
                        break;

                    case ConsoleKey.A:
                        Snake.MoveX = -1;
                        Snake.MoveY = 0;
                        break;
                    case ConsoleKey.D:
                        Snake.MoveX = 1;
                        Snake.MoveY = 0;
                        break;
                    case ConsoleKey.S:
                        Snake.MoveX = 0;
                        Snake.MoveY = 1;
                        break;
                    case ConsoleKey.W:
                        Snake.MoveX = 0;
                        Snake.MoveY = -1;
                        break;
                    case ConsoleKey.Spacebar:

                        // TOODOOOO Test tailing
                   
                        Snake.AddTail();
                        ui.score += 1 * (1000 / gameSpeed);
                        gameSpeed -= gameSpeedIncrease;
                        if (gameSpeed < 10) gameSpeed = 10;
                        gameTimer.Interval = gameSpeed;
                        gameSpeedChanged = true;
                        break;

                }

            } while (cki.Key != ConsoleKey.Escape);
        }

        private static void AddFood()
        {
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Timer that control the game loop
        /// </summary>
        private static void SetGameTimer()
        {
            // Setting start timer to 0.3 sec/300ms
            gameTimer = new(gameSpeed);
            gameTimer.Elapsed += OnGameTimerEvent;
            gameTimer.AutoReset = true;
            gameTimer.Enabled = true;
        }
        private static void OnGameTimerEvent(object sender, ElapsedEventArgs e)
        {
            
            if (!Snake.Draw())
            {
                GameOver();
                return;
            }
            if (Snake.HitFood(foodList)) Snake.AddTail();
            if(gameSpeedChanged) ui.DrawUI();
            //snake.AddTail();
        }

        private static void GameOver()
        {
            gameTimer.Stop();
            
            Console.Clear();
            Console.SetCursorPosition(boardSizeX / 2 - 4, boardSizeY / 2);
            Console.Write("Gameover");
            Console.SetCursorPosition((boardSizeX / 2) - 4, (boardSizeY / 2) + 2);
            Console.Write("Score: " + ui.score.ToString());
            Console.SetCursorPosition((boardSizeX / 2) - 4, (boardSizeY / 2) + 4);
            Console.Write("Press enter to start again");
            Console.ReadLine();
            Console.Clear();
            Restart();
        }

        private static void Restart()
        {
            gameSpeed = gameSpeedStart;
            ui.score = 0;
            ui.DrawUI();
            gameSpeedChanged = false;

            board = new(boardSizeX, boardSizeY);
            board.DrawWalls();
            Snake = new(20, 20, boardSizeX, boardSizeY, 'O');
            SetGameTimer();
        }
    }
}
