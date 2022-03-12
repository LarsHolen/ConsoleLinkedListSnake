using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleLinkedSnake.Models
{
    public class Snake : ISnakePart
    {
        // Saving positions
        public int X { get; set; }
        public int Y { get; set; }

        public int OldX { get; set; }
        public int OldY { get; set; }


        public ISnakePart Next { get; set; }

        // Snake head does not have a Prev part 
        public ISnakePart Prev { get; set; } = null;

        public ISnakePart Last { get; set; }
        public char Character { get; set; }


        // MoveX/Y control how the snake will move.  Either up, down, left, right. Starting moving up (y-1)
        public int MoveX = 0;
        public int MoveY = -1;

        public int SnakeLength = 1;
        public int boardX;
        public int boardY;
        

        public Snake(int x, int y, int bx , int by, char c = 'O')
        {
            // Setting the start position and the head character
            X = x;
            Y = y;
            Character = c;
            boardX = bx;
            boardY = by;

            // Adding the last/erasing tail.  It will write a "space" clearing trail.
            Next = new Tail(this, null, ' ');

            // Setting the eraser to Last
            Last = Next;
        }
        /// <summary>
        /// Draws the snake head and tail to the console
        /// </summary>
        internal bool Draw()
        {
            // Start moving and drawing the head
            ISnakePart head = this;

            // Saving the old position
            head.OldX = head.X;
            head.OldY = head.Y;

            // updating the new position
            head.X += MoveX;
            head.Y += MoveY;

            // Hit tests walls and food
            if (Hittest_walls(head.X, head.Y)) return false;

            // Draw the head in the new position
            Console.SetCursorPosition(head.X, head.Y);
            Console.Write(head.Character);
            ISnakePart part = head.Next;


            // Loop though the linked list
            while (part != null)
            {
                // We must update the old position and the new
                part.OldX = part.X;
                part.OldY = part.Y;
                part.X = part.Prev.OldX;
                part.Y = part.Prev.OldY;

                // But we only need to write the first tail item to overwrite the old head/big 'O'
                // and last tail item to erase the last one with the ' ' character
                // Meaning we only write the head, the first tail and the "eraser" last tail.  So we
                // write only 3 times, nomatter how long the snake is.
                if (part.Prev == head || part.Next == null)
                {
                    // Overwriting the "old head" with a small 'o' or erasing the last with ' ' 
                    Console.SetCursorPosition(part.X, part.Y);
                    Console.Write(part.Character);
                }

                // go to next item in the linked list
                part = part.Next;
            }
            return true;
           
        }


        private bool Hittest_walls(int x, int y)
        {
            if (x == 0 || x > boardX - 1 || y == 0 || y > boardY - 1) return true;
            return false;
        }

        /// <summary>
        /// Adds a new tail instance to the snake/linkedlist
        /// </summary>
        public void AddTail()
        {
            // create a new Tail eraser object
            Tail t = new(Last, null, ' ');

            // Setting the "old" last's next to the new last(but not set to last yet)
            Last.Next = t;

            // The old eraser/last tail get a 'o' character
            Last.Character = 'o';

            // We set the new eraser to Last
            Last = t;

            // Increasing snake length 
            SnakeLength++;
        }

        internal bool HitFood(List<Point> foodList)
        {
            throw new NotImplementedException();
        }
    }
}
