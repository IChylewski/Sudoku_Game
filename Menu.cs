using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Sudoku_Final_Project;

namespace Sudoku_Final_Project
{
    internal class Menu
    {
        SudokuGenerator generator = new SudokuGenerator();
        public void DisplayMenu()
        {
            int menuChoice = 0;

            while(menuChoice != 3)
            {
                Console.Clear();
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Game History");
                Console.WriteLine("3. Exit Game");
                Console.WriteLine("\nPlease enter number and press enter");
                
                if(!int.TryParse(Console.ReadLine(), out menuChoice))
                {
                    Console.Clear();
                    Console.WriteLine("Please enter integer value!\n\n");
                }

                switch(menuChoice)
                {
                    case 1:
                        StartGame();
                        break;
                    default:
                        Console.WriteLine("Please enter number 1-3");
                        break;
                }

            }
        }
        public void StartGame()
        {
            int menuChoice = 0;
            while (menuChoice != 5)
            {
                Console.Clear();
                Console.WriteLine("1. Easy");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. Hard");
                Console.WriteLine("4. All Visible");
                Console.WriteLine("5. Go To Menu");
                Console.WriteLine("\nPlease enter number and press enter");

                if (!int.TryParse(Console.ReadLine(), out menuChoice))
                {
                    Console.Clear();
                    Console.WriteLine("Please enter integer value!\n\n");
                }

                switch(menuChoice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        generator.DrawSudoku(generator.GenerateSudoku(""));
                        break;
                    case 5:
                        DisplayMenu();
                        break;
                    default:
                        Console.WriteLine("Please enter number 1-5");
                        break;

                }
            }
        }
    }
}
