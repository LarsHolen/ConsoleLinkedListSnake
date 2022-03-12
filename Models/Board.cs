using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLinkedSnake.Models
{
    public class Board
    {
        private readonly int height;
        private readonly int width;
        public Board(int w, int h)
        {
            height = h;
            width = w;
        }

        /// <summary>
        /// Drawing a square around the playfield
        /// </summary>
        public void DrawWalls()
        {
            // Draw the vertical walls
            for (int y = 0; y < height+1; y++)
            {
                if( y==0 )
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write("+");
                    Console.SetCursorPosition(width, y);
                    Console.Write("+");
                    continue;
                }
                
                if(y == height)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write("+");
                    Console.SetCursorPosition(width, y);
                    Console.Write("+");
                    continue;
                }
                
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(width, y);
                Console.Write("|");
            }

            // Draw the horizontal walls
            for (int x = 1; x < width; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.Write("-");

                Console.SetCursorPosition(x, height);
                Console.Write("-");
            }
        }
    }
}
