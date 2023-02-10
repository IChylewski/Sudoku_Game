using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Final_Project
{
    internal class SudokuGenerator
    {
        public int[,] GenerateSudoku(string difficulty)
        {
            Random rnd = new Random();
            int[,] board = new int[9, 9];
            int[] numbers = Enumerable.Range(1, 9).ToArray();
            int numbersToHide = SetDifficultyLevel(difficulty);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int[] possibleNumbers = numbers.Where(x => IsValid(board, i, j, x)).ToArray();

                    if (possibleNumbers.Length == 0)
                    {
                        i = 0;
                        j = -1;
                        board = new int[9, 9];
                        continue;
                    }
                    int randomIndex = rnd.Next(possibleNumbers.Length);
                    board[i, j] = possibleNumbers[randomIndex];
                }
            }
            return board;
        }

        private bool IsValid(int[,] board, int row, int col, int num)
        {

            // Check if row or column contains number
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] == num || board[i, col] == num)
                {
                    return false;
                }
            }

            // Set 3x3 box range to check
            int startRow = (row / 3) * 3;
            int startColumn = (col / 3) * 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startColumn; j < startColumn + 3; j++)
                {
                    if (board[i, j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        public void DrawSudoku(int[,] board)
        {
            Console.Clear();
            Console.WriteLine("    1 2 3 4 5 6 7 8 9\n");
            for (int i = 0; i < 9; i++)
            {
                Console.Write((i+1) + "  ");
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                    {
                        Console.Write("|");
                    }

                    if (j == 1 || j == 4 || j == 7)
                    {
                        Console.Write(" " + board[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(board[i, j]);
                    }

                    if (j == 8)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                if (i == 2 || i == 5)
                {
                    Console.WriteLine("    -------------------");
                }
            }
            PlaySudoku();
        }
        private int SetDifficultyLevel(string difficulty)
        {
            int numbersToHide;

            switch(difficulty)
            {
                case "Easy":
                    numbersToHide = 10;
                    break;
                case "Medium":
                    numbersToHide = 20;
                    break;
                case "Hard":
                    numbersToHide = 30;
                    break;
                default:
                    numbersToHide = 0;
                    break;
            }
            return numbersToHide;
        }
        private void PlaySudoku()
        {
            Console.WriteLine("\nFill cell using [x,y] = number                                                       Enter exit to go back to menu");
            Console.ReadLine();
        }
    }
}
