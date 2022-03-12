using ConsoleLinkedSnake.Models;
using System;
using System.Timers;

namespace ConsoleLinkedSnake
{
    class Program
    {
        private static Timer gameTimer;
        private static Snake snake { get; set; }

        private static Board board;
        private static int boardSizeX;
        private static int boardSizeY;

        private static int gameSpeed = 300;
        private static bool gameSpeedChanged = true;
        private static int gameSpeedIncrease = 10;

        private static SnakeUI ui;
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;


            boardSizeX = 70;
            boardSizeY = 25;
            ui = new(boardSizeX + 2, 10);
            //ui.speed = gameSpeed;
            ui.DrawUI();
            gameSpeedChanged = false;

            board = new(boardSizeX, boardSizeY);
            board.DrawWalls();
            snake = new(20,20,'O');
            SetGameTimer();


            // Test for key input
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                switch (cki.Key)
                {

                    case ConsoleKey.LeftArrow:
                        snake.MoveX = -1;
                        snake.MoveY = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        snake.MoveX = 0;
                        snake.MoveY = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        snake.MoveX = 1;
                        snake.MoveY = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        snake.MoveX = 0;
                        snake.MoveY = 1;
                        break;

                    case ConsoleKey.A:
                        snake.MoveX = -1;
                        snake.MoveY = 0;
                        break;
                    case ConsoleKey.D:
                        snake.MoveX = 1;
                        snake.MoveY = 0;
                        break;
                    case ConsoleKey.S:
                        snake.MoveX = 0;
                        snake.MoveY = 1;
                        break;
                    case ConsoleKey.W:
                        snake.MoveX = 0;
                        snake.MoveY = -1;
                        break;
                    case ConsoleKey.Spacebar:

                        // TOODOOOO Test tailing
                        snake.AddTail();
                        ui.score += 1 * (1000 / gameSpeed);
                        gameSpeed -= gameSpeedIncrease;
                        if (gameSpeed < 10) gameSpeed = 10;
                        gameTimer.Interval = gameSpeed;
                        gameSpeedChanged = true;
                        break;

                }

            } while (cki.Key != ConsoleKey.Escape);
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

            snake.Draw();
            if(gameSpeedChanged) ui.DrawUI();
            //snake.AddTail();
        }
    }
}
