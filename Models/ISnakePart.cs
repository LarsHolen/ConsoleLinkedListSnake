using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLinkedSnake.Models
{
    public interface ISnakePart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int OldX { get; set; }
        public int OldY { get; set; }
        public ISnakePart Next { get; set; }
        public ISnakePart Prev { get; set; }
        public char Character { get; set; }

    }
}
