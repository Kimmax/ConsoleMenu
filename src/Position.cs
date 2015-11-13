using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuernberger.ConsoleMenu
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
