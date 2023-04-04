using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class SudokuGenerator
    {
        public static void GenerateSudoku()
        {
            Random rnd = new Random();
            int[,] board = new int[9, 9];
            int[] numbers = Enumerable.Range(1, 9).ToArray();

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
            int counterX = 0;
            int counterY = 0;
            foreach (var item in Data.inputFields)
            {
                

                if(counterX == 9)
                {
                    counterY += 1;
                    counterX = 0;
                }
                Data.numberFields.Add(item.Key, board[counterX, counterY]);

                counterX += 1;

            }
            var test = Data.numberFields;
        }

        private static bool IsValid(int[,] board, int row, int col, int num)
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
    }
}
