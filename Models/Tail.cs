using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLinkedSnake.Models
{
    public class Tail : ISnakePart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int OldX { get; set; }
        public int OldY { get; set; }
        public ISnakePart Next { get; set; }
        public ISnakePart Prev { get; set; }
        public char Character { get; set; } = '_';

        public Tail(ISnakePart prev, ISnakePart next, char c)
        {
            X = prev.OldX;
            Y = prev.OldY;
            //OldX = X;
            //OldY = Y;
            Prev = prev;
            Next = next;
            Character = c;
        }
    }
}
