using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuernberger.ConsoleMenu
{
    public class Block
    {
        public Position Position { get; private set; }
        public Size Size { get; private set; }

        public string[] Buffer { get; private set; }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegorundColor { get; set; }

        public Block(Position pos, Size size, ConsoleColor backColor = ConsoleColor.Black, ConsoleColor foreColor = ConsoleColor.Gray)
        {
            this.Position = pos;
            this.Size = size;

            this.BackgroundColor = backColor;
            this.ForegorundColor = foreColor;

            this.Buffer = new string[this.Size.Height -1];
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < this.Buffer.Length; i++)
            {
                this.Buffer[i] = new String(' ', this.Size.Width);
            }
        }

        public void DrawCenteredText(string text)
        {
            int centeredX = (int)((this.Size.Width - text.Length) / 2);
            int y = (int)(this.Size.Height / 2);
            this.Buffer[y] = this.Buffer[y].Remove(centeredX, text.Length).Insert(centeredX, text);
        }

        public void DrawTextAt(int x, int y, string text)
        {
            this.Buffer[y] = this.Buffer[y].Remove(x, text.Length).Insert(x, text);
        }

        internal void Draw()
        {
            ConsoleColor oldBackcolor = Console.BackgroundColor;
            ConsoleColor oldForegorundColor = Console.ForegroundColor;

            Console.BackgroundColor = this.BackgroundColor;
            Console.ForegroundColor = this.ForegorundColor;

            for (int i = 0; i < this.Buffer.Length; i++)
            {
                Console.SetCursorPosition(this.Position.X, this.Position.Y + i);
                Console.Write(this.Buffer[i]);
            }

            Console.BackgroundColor = oldBackcolor;
            Console.ForegroundColor = oldForegorundColor;
        }
    }
}
