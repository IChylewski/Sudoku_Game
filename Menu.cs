using Sudoku.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Menu
    {
        static int menuChoice;
        static ConsoleKeyInfo key;
        static string color = "\u001b[32m";

        public static void DisplayMenu()
        {
            bool isSelected = false;
            menuChoice = 1;

            while (!isSelected)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine($"{(menuChoice == 1 ? color : "")}Start Game\u001b[0m");
                Console.WriteLine($"{(menuChoice == 2 ? color : "")}Score Board\u001b[0m");
                Console.WriteLine($"{(menuChoice == 3 ? color : "")}Game History\u001b[0m");
                Console.WriteLine($"{(menuChoice == 4 ? color : "")}Exit Game\u001b[0m");
                Console.WriteLine("\nUse Up and Down to navigate and press the Enter key to select");
                key = Console.ReadKey(true);


                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        menuChoice = (menuChoice == 4 ? 1 : menuChoice + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        menuChoice = (menuChoice == 1 ? 4 : menuChoice - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
                if (isSelected)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            StartGame();
                            break;
                        case 2:
                            ScoreBoard();
                            break;
                        default:
                            Console.WriteLine("Some unexcepted action occured");
                            break;
                    }
                }

            }
        }

        public static void StartGame()
        {
            bool isSelected = false;
            menuChoice = 1;

            Console.Clear();
            Console.WriteLine("Please enter your username");
            Data.userName = Console.ReadLine();

            while (!isSelected)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine($"{(menuChoice == 1 ? color : "")}Easy\u001b[0m");
                Console.WriteLine($"{(menuChoice == 2 ? color : "")}Medium\u001b[0m");
                Console.WriteLine($"{(menuChoice == 3 ? color : "")}Hard\u001b[0m");
                Console.WriteLine($"{(menuChoice == 4 ? color : "")}All Visible\u001b[0m");
                Console.WriteLine($"{(menuChoice == 5 ? color : "")}Go Back To Menu\u001b[0m");
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        menuChoice = (menuChoice == 5 ? 1 : menuChoice + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        menuChoice = (menuChoice == 1 ? 5 : menuChoice - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
                if (isSelected)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Data.chosenLevel = "Easy";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(10);
                            Game.PlaySudoku();
                            break;
                        case 2:
                            Data.chosenLevel = "Medium";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(20);
                            Game.PlaySudoku();
                            break;
                        case 3:
                            Data.chosenLevel = "Hard";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(30);
                            Game.PlaySudoku();
                            break;
                        case 4:
                            Data.chosenLevel = "None";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(0);
                            Game.PlaySudoku();
                            break;
                        case 5:
                            DisplayMenu();
                            break;
                        default:
                            Console.WriteLine("Some unexpected action occured");
                            break;

                    }
                }
            }
        }
        public static void ScoreBoard()
        {
            List<ScoreModel> scores = Database.Database.GetScores();
            int counter = 0;

            Console.Clear();
            Console.CursorVisible = false;

            foreach (ScoreModel score in scores)
            {
                counter += 1;
                Console.WriteLine($"{counter}. {score.Username} {score.Level} {score.Score}");
            }


            Console.WriteLine($"Press enter to go back to main menu...");
            key = Console.ReadKey();
            DisplayMenu();
        }
    }
}
