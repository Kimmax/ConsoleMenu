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
        
        public void Run()
        {
            FancyMenu menu = new FancyMenu("ConsoleMenu!");

            Console.ReadLine();
        }
    }
}
