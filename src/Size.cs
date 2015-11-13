using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuernberger.ConsoleMenu
{
    public class Size
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Size(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }
    }
}
