using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nuernberger.ConsoleMenu
{
    public class Block
    {
        public Position Position { get; private set; }
        public Size Size { get; private set; }

        public string[] Buffer { get; private set; }
        public int zIndex { get; internal set; }

        public ConsoleColor BlockBackgroundColor { get; set; }
        public ConsoleColor BlockForegorundColor { get; set; }

        public Block(Position pos, Size size, ConsoleColor foreColor = ConsoleColor.Gray, ConsoleColor backColor = ConsoleColor.Black)
        {
            this.Position = pos;
            this.Size = size;

            this.BlockBackgroundColor = backColor;
            this.BlockForegorundColor = foreColor;

            this.Buffer = new string[this.Size.Height];

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < this.Buffer.Length; i++)
            {
                this.Buffer[i] = new String(' ', this.Size.Width);
            }
        }

        public void WriteCenteredText(string text)
        {
            string clearedText = Regex.Replace(text, @"\$<.*?>", "");

            int centeredX = (int)((this.Size.Width - clearedText.Length) / 2);
            int y = (int)(this.Size.Height / 2);
            this.Buffer[y] = this.Buffer[y].Remove(centeredX, clearedText.Length).Insert(centeredX, text);
        }

        public void WriteTextAt(Position pos, string text)
        {
            this.Buffer[pos.Y] = this.Buffer[pos.Y].Remove(pos.X, text.Length).Insert(pos.X, text);
        }

        internal void Draw()
        {
            ConsoleColor oldBackcolor = Console.BackgroundColor;
            ConsoleColor oldForegorundColor = Console.ForegroundColor;

            Console.BackgroundColor = this.BlockBackgroundColor;
            Console.ForegroundColor = this.BlockForegorundColor;

            for (int i = 0; i < this.Buffer.Length; i++)
            {
                Console.SetCursorPosition(this.Position.X, this.Position.Y + i);

                WriteColorised(this.Buffer[i]);
            }

            Console.BackgroundColor = oldBackcolor;
            Console.ForegroundColor = oldForegorundColor;
        }

        private void WriteColorised(string text)
        {
            string[] textParts = Regex.Split(text, @"(\$<.*?>)", RegexOptions.Compiled);
            
            Regex backAndForeColorSelector = new Regex(@"\$<(.*?),(.*?)>(.*)", RegexOptions.Compiled);
            Regex foreColorSelector = new Regex(@"\$<(.*?)>(.*)", RegexOptions.Compiled);
            Regex resetColorSelector = new Regex(@"\$</>", RegexOptions.Compiled);

            foreach (string s in textParts)
            {
                if (resetColorSelector.IsMatch(s))
                {
                    Console.BackgroundColor = this.BlockBackgroundColor;
                    Console.ForegroundColor = this.BlockForegorundColor;
                }
                else if (backAndForeColorSelector.IsMatch(s))
                {
                    foreach (Match match in backAndForeColorSelector.Matches(s))
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("$1"));
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("$2"));
                    }
                }
                else if (foreColorSelector.IsMatch(s))
                {
                    foreach (Match match in foreColorSelector.Matches(s))
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("$1"));
                        Console.BackgroundColor = this.BlockBackgroundColor;
                    }
                }
                else
                {
                    Console.Write(s);
                }
            }
        }
    }
}
