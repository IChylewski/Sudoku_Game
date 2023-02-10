using System;
using System.Linq;

namespace Sudoku_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawSudoku(GenerateSudoku());
        }
        static int[,] GenerateSudoku()
        {
            Random rnd = new Random();
            int[,] board = new int[9, 9];
            int[] numbers = Enumerable.Range(1, 9).ToArray();

            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    int[] possibleNumbers = numbers.Where(x => IsValid(board, i, j, x)).ToArray();

                    if(possibleNumbers.Length == 0)
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
        static bool IsValid(int[,] board, int row, int col, int num)
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

            for(int i = startRow; i < startRow + 3; i++)
            {
                for(int j = startColumn; j < startColumn + 3; j++)
                {
                    if(board[i, j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        static void DrawSudoku(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
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
                if(i == 2 || i == 5)
                {
                    Console.WriteLine("-------------------");
                }
            }
            Console.ReadLine();
        }

    }
}