

using ConsoleLinkedSnake.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace ConsoleLinkedSnake
{
    public class Game
    {
        // The time between every move
        private static Timer gameTimer;
        
        // The Snake head and the start of the linked list
        private static Snake Snake { get; set; }

        // The game board, walls obstacles and so on
        private static Board board;
        private static int boardSizeX;
        private static int boardSizeY;

        private static readonly int gameSpeedStart = 100;
        private static int gameSpeed = gameSpeedStart;
        private static bool scoreChanged = true;
        private static readonly int gameSpeedIncrease = 2;

        private static readonly List<Point> foodList = new();

        private static readonly Random random = new();

        private static SnakeUI ui;
        public Game()
        {
            // Set the initial console window size
            Console.WindowWidth = 50;
            Console.WindowHeight = 25;
            

            Console.Title = "       Console Snake, by Lars Holen";
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("     *******      Snake       *******");
            Console.WriteLine("                 By Lars Holen          ");
            Console.WriteLine("              Press enter to start      ");
            Console.ReadLine();

            NewGame();

        }


        /// <summary>
        /// Read keyboard input during game
        /// </summary>
        private static void KeyInputputHandler()
        {
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
                        // Using space to test stuff
                        /*
                        Snake.AddTail();
                        ui.score += 1 * (1000 / gameSpeed);
                        gameSpeed -= gameSpeedIncrease;
                        if (gameSpeed < 10) gameSpeed = 10;
                        gameTimer.Interval = gameSpeed;
                        scoreChanged = true;
                        */

                        break;

                }
            } while (cki.Key != ConsoleKey.Escape);
        }

        private static void NewGame()
        {
            // Clear the screen 
            Console.Clear();

            // Set boardSize
            // X size smaller than window to make space for UI/Score
            boardSizeX = Console.WindowWidth - 10;
            // Y size ome less than window, so there is space enough for the walls
            boardSizeY = Console.WindowHeight -1;

            // Make and draw the"ui"/score
            ui = new(boardSizeX + 2, 10);
            ui.DrawUI();
            scoreChanged = false;

            
            board = new(boardSizeX, boardSizeY);
            board.DrawWalls();
            Snake = new((boardSizeX-10)/2, boardSizeY / 2, boardSizeX, boardSizeY, 'O');
            SetGameTimer();
            KeyInputputHandler();
        }

        /// <summary>
        /// Adding food to the gameboard at random position within the bameboard.  
        /// Check that it does not add food on space occupied by the snake. 
        /// </summary>
        private static void AddFood()
        {
            // boolean set if the random x/y hit the snake
            bool hitSnake = true;
            int x = 0;
            int y = 0;
            // Keep trying to find a spot without snake in it
            while (hitSnake)
            {
                hitSnake = false;
                // random numbers within the board limits
                x = random.Next(1, boardSizeX);
                y = random.Next(1, boardSizeY);
                // Test if the snake is in the way
                ISnakePart p = Snake;
                // Looping through the snake/linked list
                while (p.Next != null)
                {
                    if (p.X == x && p.Y == y)
                    {
                        // Snake part at x,y Try again
                        hitSnake = true;
                        break;
                    }
                    p = p.Next;
                }
                // And test if there are food there allready
                foreach (var item in foodList)
                {
                    if (item.X == x && item.Y == y) hitSnake = true;
                }
            }
            foodList.Add(new Point(x, y));
            Console.SetCursorPosition(x, y);
            Console.Write("$");
        }

        /// <summary>
        /// Timer that control the game loop
        /// </summary>
        private static void SetGameTimer()
        {
            // Setting start timer to gameSpeed ms
            gameTimer = new(gameSpeed);
            gameTimer.Elapsed += OnGameTimerEvent;
            gameTimer.AutoReset = true;
            gameTimer.Enabled = true;
        }

        // Game loop/Timer
        private static void OnGameTimerEvent(object sender, ElapsedEventArgs e)
        {
            // Testing if the snake is moving horizontal or vertical
            // GameSpeed/Snake speed is adjusted since the height of the custor is about double the width
            // Now it will look like the snake moves the same speed horizontally and vertically
            if (Snake.MoveX != 0) gameTimer.Interval = gameSpeed / 2;
            if (Snake.MoveY != 0) gameTimer.Interval = gameSpeed;

            // Draw return false if it hit a wall. 
            if (!Snake.Draw())
            {
                GameOver();
                return;
            }

            // Testing if the snake hit itself
            if (Snake.EatSnake()) GameOver();

            // Testing if we hit food
            if(Snake.HitFood(foodList))
            {
                Snake.AddTail();
                ui.score += 1 * (1000 / gameSpeed);
                gameSpeed -= gameSpeedIncrease;
                if (gameSpeed < 10) gameSpeed = 10;
                gameTimer.Interval = gameSpeed;
                scoreChanged = true;
            }

            // If the score changed, redraw it
            if (scoreChanged) ui.DrawUI();

            // Add food if there are no food on the board
            if (foodList.Count < 1)
            {
                AddFood();
                return;
            }
            // If there are food on the board, add only when random....
            if (foodList.Count > 0)
            {
                if (random.Next(0, 100) > 90) AddFood();
                return;
            }
        }

        /// <summary>
        /// Game over 
        /// </summary>
        private static void GameOver()
        {
            gameTimer.Stop();

            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 - 4);
            Console.Write("Gameover");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 2) -2);
            Console.Write("Score: " + ui.score.ToString());
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, (Console.WindowHeight / 2) );
            Console.Write("Press enter to start again");
            Console.ReadLine();
            Console.Clear();
            Restart();
        }

        /// <summary>
        /// Restart
        /// </summary>
        private static void Restart()
        {
            gameSpeed = gameSpeedStart;
            ui.score = 0;
            ui.DrawUI();
            scoreChanged = false;

            board = new(boardSizeX, boardSizeY);
            board.DrawWalls();
            Snake = new(20, 20, boardSizeX, boardSizeY, 'O');
            SetGameTimer();
        }
    }
}
