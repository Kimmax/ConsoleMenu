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
            Block blueBlock = new Block(new Position(5, 5), new Size(10, 16), ConsoleColor.Blue, ConsoleColor.Black);
            blueBlock.DrawCenteredText("$<White>Test1 $<Black>Test2");

            Block redBlock = new Block(new Position(13, 13), new Size(10, 16), ConsoleColor.Red, ConsoleColor.Black);
            redBlock.DrawCenteredText("$<Black>Test3 $<White>Test4");

            Block greenBlock = new Block(new Position(25, 5), new Size(10, 16), ConsoleColor.Green, ConsoleColor.Black);
            greenBlock.DrawCenteredText("$<White>Test5 $<Black>Test6");

            lManager.AddLayer(blueBlock);
            lManager.AddLayer(redBlock);
            lManager.AddLayer(greenBlock, 0);

            lManager.Draw();

            Console.ReadLine();
        }
    }
}
