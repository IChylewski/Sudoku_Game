using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    static internal class GridBuilder
    {
        static string boxColor = "\u001b[32m";
        public static void GenerateGrid()
        {
            Console.Clear();

            for(int k = 0; k < 9; k++)
            {
                char letter = 'A';
                Console.Write($"     {(char)(((int)letter) + k)}");
                
            }

            for(int j = 0; j < 9; j++)
            {
                if(j == 0 || j == 3 || j == 6 || j == 9)
                {
                    Console.WriteLine($"\n{boxColor}  -------------------------------------------------------\u001b[0m");
                }
                else
                {
                    Console.WriteLine("\n  -------------------------------------------------------");
                }

                for (int i = 0; i < 55; i += 1)
                {
                    switch(i)
                    {
                        case 0:
                            Console.Write($"{j+1} {boxColor}|\u001b[0m");
                            break;
                        case 6:
                            Console.Write('|');
                            break;
                        case 12:
                            Console.Write('|');
                            break;
                        case 18:
                            Console.Write($"{boxColor}|\u001b[0m");
                            break;
                        case 24:
                            Console.Write('|');
                            break;
                        case 30:
                            Console.Write('|');
                            break;
                        case 36:
                            Console.Write($"{boxColor}|\u001b[0m");
                            break;
                        case 42:
                            Console.Write('|');
                            break;
                        case 48:
                            Console.Write('|');
                            break;
                        case 54:
                            Console.Write($"{boxColor}|\u001b[0m");
                            break;
                        default:
                            Console.Write(' ');
                            break;
                    }
                }
            }
            Console.WriteLine($"\n{boxColor}  -------------------------------------------------------\u001b[0m");

            Console.WriteLine("\n\nUse Arrows to change cells, the game finishes when all numbers are correct!");
            Console.WriteLine("Press Esc to go back to main menu.");
        }

        public static void FillNumbers(int numbersToHide)
        {
            Data.indexesToHide = GenerateRandomNumbersToHide(numbersToHide); // here change level
            Data.editableFields = Data.indexesToHide.OrderBy(x => x).ToList();
            foreach(string cord in Data.indexesToHide)
            {
                Data.playerGuesses.Add(cord, 0);
            }
            
            foreach(var item in Data.inputFields)
            {
                if(Data.indexesToHide.Contains(item.Key))
                {
                    Console.SetCursorPosition(item.Value[0], item.Value[1]);
                    Console.Write(' ');
                }
                else
                {
                    Console.SetCursorPosition(item.Value[0], item.Value[1]);
                    Console.Write(Data.numberFields.First(x => x.Key == item.Key).Value);
                }
            }

            Console.SetCursorPosition(0, 20);
        }

        public static List<string> GenerateRandomNumbersToHide(int count)
        {
            List<int> numbers = new List<int>();
            List<string> cords = new List<string>();
            Random random = new Random();

            while (numbers.Count < count - 1)
            {
                int number = random.Next(0, 73); // generate a random number between 1 and 10
                if (!numbers.Contains(number))
                {
                    numbers.Add(number); // add the number to the list if it is not already in the list
                    cords.Add(Data.numberFields.ElementAt(number).Key);
                }
            }

            return cords;
        }
    }
}
