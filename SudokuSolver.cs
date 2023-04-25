using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.ConstraintSolver;
using Google.OrTools.Sat;
using IntVar = Google.OrTools.ConstraintSolver.IntVar;

namespace Sudoku
{
    public static class SudokuSolver
    {
        // The way data is saved and stored is not valid for checking if the board is unique
        // This method is a helper function to change normal grid to the board that can be checked for uniqueness
        public static int[,] ChangeToSolvableGrid(int[,] board)
        {
            int[,] newBoard = board;
            foreach (string item in Data.indexesToHide.OrderBy(x => x).ToList())
            {
                int index = -1;
                for (int i = 0; i < Data.numberFields.Count; i++)
                {
                    if (Data.numberFields.ElementAt(i).Key == item)
                    {
                        index = i; break;
                    }
                }

                int row = index / 9;
                int col = index % 9;
                if (col > 9)
                {
                    col += 1;
                }
                newBoard[col, row] = 0;
            }
            return newBoard;
        }
        // Check is the generated board is unique
        public static bool CheckUniqueness(int[,] rawBoard)
        {
            int[,] board = ChangeToSolvableGrid(rawBoard);
            Solver solver = new Solver("Sudoku");

            IntVar[,] variables = new IntVar[9, 9];
            // Populates variables that represent unknowns in the grid
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    if (board[col, row] == 0)
                    {
                        variables[col, row] = solver.MakeIntVar(1, 9, $"Cell_{col}_{row}");
                    }
                    else
                    {
                        variables[col, row] = solver.MakeIntConst(board[col, row]);
                    }
                }
            }

            SetUpConstraints(solver, variables, board);

            DecisionBuilder db = solver.MakePhase(variables.Flatten(), Solver.INT_VAR_SIMPLE, Solver.ASSIGN_MIN_VALUE);

            SolutionCollector solutionCollector = solver.MakeAllSolutionCollector();

            solutionCollector.Add(variables.Flatten());

            solver.NewSearch(db, solutionCollector);

            // while the solver has next solution adds the 1 to counter
            while(solver.NextSolution())
            {
                if(solutionCollector.SolutionCount() > 1)
                {
                    return false;
                }
            }
            return true;
        }
        // It setup constraints for the solver
        private static void SetUpConstraints(Solver solver, IntVar[,] variables, int[,] board)
        {
            // rows and columns
            for (int i = 0; i < 9; i++)
            {
                IntVar[] row = new IntVar[9];
                IntVar[] col = new IntVar[9];
                for(int j = 0; j < 9; j++)
                {
                    row[j] = variables[i, j];
                    col[j] = variables[j, i];
                }
                solver.Add(row.AllDifferent());
                solver.Add(col.AllDifferent());
            }
            // Define the constraints for the 3x3 boxes
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    IntVar[] box = new IntVar[9];
                    for(int x = 0; x < 3; x++)
                    {
                        for(int y = 0; y < 3; y++)
                        {
                            box[x * 3 + y] = variables[i * 3 + x, j * 3 + y];
                        }
                    }
                    solver.Add(box.AllDifferent());

                }
            }
            // Add the initial values as constraints
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        solver.Add(variables[i, j] == board[i, j]);
                    }
                }
            }
        }
    }

}
