using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLinkedSnake.Models
{
    public class SnakeUI
    {
        private int posX;
        private int posY;

        public int score = 0;
        public int speed = 0;

        /// <summary>
        /// Constructor.  Takes x,y parameters for position in the console
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SnakeUI(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void DrawUI()
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write("Score: " + score.ToString());
            Console.SetCursorPosition(posX, posY+1);
            Console.Write("Speed: " + speed.ToString());


        }
    }
}
