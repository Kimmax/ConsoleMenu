using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuernberger.ConsoleMenu
{
    public class FancyMenu
    {
        public string Title { get; set; }

        LayerManager lManager = new LayerManager();
        

        public FancyMenu(String title)
        {
            this.Title = title;
            Console.CursorVisible = false;

            Block titleBar = new Block(new Position(0, 0), new Size(1, Console.WindowWidth), ConsoleColor.Black, ConsoleColor.Gray);
            titleBar.WriteTextAt(new Position(1, 0), "Fancy menu");

            this.lManager.AddLayer(titleBar);

            Block menuBorder, menuBlock;
            Size menuSize = new Size(5, 35);
            Position menuPos = new Position((Console.WindowWidth - menuSize.Width) / 2, (Console.WindowHeight - menuSize.Height) / 2);
            
            menuBorder = new Block(new Position(menuPos.X -1, menuPos.Y -1), new Size(menuSize.Height +2, menuSize.Width +2), ConsoleColor.Gray, ConsoleColor.Gray);
            menuBlock = new Block(menuPos, menuSize);

            menuBlock.WriteTextAt(new Position(0, 0), "What would you like to do?");
            menuBlock.WriteTextAt(new Position(0, 1), "$<Black,Gray,Gray,Black>Test");
            menuBlock.WriteTextAt(new Position(0, 2), "$<Black,Gray,Gray,Black>this");
            menuBlock.WriteTextAt(new Position(0, 3), "$<Black,Gray,Gray,Black>fancy");
            menuBlock.WriteTextAt(new Position(0, 4), "$<Black,Gray,Gray,Black>thing");

            menuBlock.IsSelectableBuffer[1] = true;
            menuBlock.IsSelectableBuffer[2] = true;
            menuBlock.IsSelectableBuffer[3] = true;
            menuBlock.IsSelectableBuffer[4] = true;

            menuBlock.SetSelectedLine(1);

            //this.lManager.AddLayer(menuBorder);
            this.lManager.AddLayer(menuBlock);
            this.lManager.SetSelectedLayer(menuBlock);

            this.lManager.Draw();

            int selectedItem = -1;
            while(selectedItem == -1)
            {
                ConsoleKey pressedKey = Console.ReadKey().Key;
                if (pressedKey == ConsoleKey.DownArrow)
                {
                    int newSelectedIndex = menuBlock.GetSelectedLine() + 1;

                    if (menuBlock.IsSelectableBuffer.Length - 1 >= newSelectedIndex && menuBlock.IsSelectableBuffer[newSelectedIndex] == true)
                        menuBlock.SetSelectedLine(menuBlock.GetSelectedLine() + 1);

                }
                else if (pressedKey == ConsoleKey.UpArrow)
                {
                    int newSelectedIndex = menuBlock.GetSelectedLine() - 1;

                    if (menuBlock.IsSelectableBuffer.Length - 1 >= newSelectedIndex && menuBlock.IsSelectableBuffer[newSelectedIndex] == true)
                        menuBlock.SetSelectedLine(menuBlock.GetSelectedLine() - 1);

                }
                else if (pressedKey == ConsoleKey.Enter)
                {
                    selectedItem = menuBlock.GetSelectedLine();
                }
            }
        }
    }
}
