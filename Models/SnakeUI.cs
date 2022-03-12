using System;


namespace ConsoleLinkedSnake.Models
{
    public class SnakeUI
    {
        private readonly int posX;
        private readonly int posY;

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
            


        }
    }
}
