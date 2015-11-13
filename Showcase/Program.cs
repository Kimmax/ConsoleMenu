using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nuernberger.ConsoleMenu;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Base myBase = new Base();
            myBase.Run();
        }
    }

    class Base
    {
        LayerManager lManager = new LayerManager();
        public void Run()
        {
            Block blueBlock = new Block(new Position(5, 5), new Size(5, 8), ConsoleColor.DarkBlue, ConsoleColor.Black);
            blueBlock.DrawCenteredText("1");

            Block redBlock = new Block(new Position(8, 8), new Size(5, 8), ConsoleColor.Red, ConsoleColor.Black);
            redBlock.DrawCenteredText("2");

            Block greenBlock = new Block(new Position(12, 10), new Size(5, 8), ConsoleColor.Green, ConsoleColor.Black);
            greenBlock.DrawCenteredText("3");

            lManager.AddLayer(blueBlock);
            lManager.AddLayer(redBlock);
            lManager.AddLayer(greenBlock, 0);

            lManager.Draw();

            Console.ReadLine();
        }
    }
}
