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
        public bool[] IsSelectedBuffer { get; private set; }
        public bool[] IsSelectableBuffer { get; set; }

        public int zIndex { get; internal set; }
        public bool BlockSelected { get; internal set; }

        public bool IsVisible { get; set; }
        public bool WasLastDrawVisible { get; set; }

        public ConsoleColor BlockBackgroundColor { get; set; }
        public ConsoleColor BlockForegorundColor { get; set; }

        public Block(Position pos, Size size, ConsoleColor foreColor = ConsoleColor.Gray, ConsoleColor backColor = ConsoleColor.Black)
        {
            this.Position = pos;
            this.Size = size;

            this.BlockBackgroundColor = backColor;
            this.BlockForegorundColor = foreColor;

            this.Buffer = new string[this.Size.Height];
            this.IsSelectedBuffer = new bool[this.Size.Height];
            this.IsSelectableBuffer = new bool[this.Size.Height];

            this.IsVisible = true;

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

        public void WriteTextAt(Position pos, string text, bool clearString = true)
        {
            string clearedText = Regex.Replace(text, @"\$<.*?>", "");
            this.Buffer[pos.Y] = new String(' ', this.Size.Width);
            this.Buffer[pos.Y] = this.Buffer[pos.Y].Remove(pos.X, clearedText.Length).Insert(pos.X, text);
        }

        public void SetSelectedLine(int line)
        {
            for (int i = 0; i < this.IsSelectedBuffer.Length; i++)
                this.IsSelectedBuffer[i] = false;

            this.IsSelectedBuffer[line] = true;
        }

        public int GetSelectedLine()
        {
            for (int i = 0; i <= this.IsSelectedBuffer.Length; i++)
                if(this.IsSelectedBuffer[i])
                    return i;

            throw new InvalidOperationException();
        }

        public string GetTextAt(int line)
        {
            if (line > this.Buffer.Length || line > this.Buffer.Length)
                throw new ArgumentOutOfRangeException();

            string clearedText = Regex.Replace(this.Buffer[line], @"\$<.*?>", "");
            return clearedText.Trim();
        }

        internal void Draw()
        {
            if (this.IsVisible)
            {
                this.WasLastDrawVisible = true;

                ConsoleColor oldBackcolor = Console.BackgroundColor;
                ConsoleColor oldForegorundColor = Console.ForegroundColor;

                Console.BackgroundColor = this.BlockBackgroundColor;
                Console.ForegroundColor = this.BlockForegorundColor;

                for (int i = 0; i < this.Buffer.Length; i++)
                {
                    Console.SetCursorPosition(this.Position.X, this.Position.Y + i);

                    WriteColorised(this.Buffer[i], i);
                }

                Console.BackgroundColor = oldBackcolor;
                Console.ForegroundColor = oldForegorundColor;
            }
            else
            {
                if (this.WasLastDrawVisible)
                {
                    for (int i = 0; i < this.Buffer.Length; i++)
                    {
                        Console.SetCursorPosition(this.Position.X, this.Position.Y + i);

                        WriteColorised(new String(' ', this.Size.Width), i);
                    }

                    this.WasLastDrawVisible = false;
                }
            }
        }

        private void WriteColorised(string text, int currentLine)
        {
            string[] textParts = Regex.Split(text, @"(\$<.*?>)", RegexOptions.Compiled);

            Regex selectableSelector = new Regex(@"\$<(?<HighliteForegorund>.*?),(?<HighliteBackgorund>.*?),(?<Foregorund>.*?),(?<Backgorund>.*?)>(?<Text>.*)", RegexOptions.Compiled);
            Regex backAndForeColorSelector = new Regex(@"\$<(?<Foregorund>.*?),(?<Backgorund>.*?)>(?<Text>.*)", RegexOptions.Compiled);
            Regex foreColorSelector = new Regex(@"\$<(?<Foregorund>.*?)>(?<Text>.*)", RegexOptions.Compiled);
            Regex resetColorSelector = new Regex(@"\$</>", RegexOptions.Compiled);

            foreach (string s in textParts)
            {
                if (resetColorSelector.IsMatch(s))
                {
                    Console.BackgroundColor = this.BlockBackgroundColor;
                    Console.ForegroundColor = this.BlockForegorundColor;
                }
                else if (selectableSelector.IsMatch(s))
                {
                    foreach (Match match in selectableSelector.Matches(s))
                    {
                        if (this.BlockSelected && this.IsSelectableBuffer[currentLine] && this.IsSelectedBuffer[currentLine])
                        {
                            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${HighliteForegorund}"), true);
                            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${HighliteBackgorund}"), true);
                        }
                        else
                        {
                            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${Foregorund}"), true);
                            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${Backgorund}"), true);
                        }
                        
                    }
                }
                else if (backAndForeColorSelector.IsMatch(s))
                {
                    foreach (Match match in backAndForeColorSelector.Matches(s))
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${Foregorund}"), true);
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${Backgorund}"), true);
                    }
                }
                else if (foreColorSelector.IsMatch(s))
                {
                    foreach (Match match in foreColorSelector.Matches(s))
                    {
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), match.Result("${Foregorund}"), true);
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
