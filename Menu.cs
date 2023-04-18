using Sudoku.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Sudoku
{
    public static class Menu
    {
        static int menuChoice;
        static ConsoleKeyInfo key;
        static string color = "\u001b[32m";
        static string logo = @"
░██████╗██╗░░░██╗██████╗░░█████╗░██╗░░██╗██╗░░░██╗
██╔════╝██║░░░██║██╔══██╗██╔══██╗██║░██╔╝██║░░░██║
╚█████╗░██║░░░██║██║░░██║██║░░██║█████═╝░██║░░░██║
░╚═══██╗██║░░░██║██║░░██║██║░░██║██╔═██╗░██║░░░██║
██████╔╝╚██████╔╝██████╔╝╚█████╔╝██║░╚██╗╚██████╔╝
╚═════╝░░╚═════╝░╚═════╝░░╚════╝░╚═╝░░╚═╝░╚═════╝░";
        // Displays menu interface to user and allows dynamic interaction
        public static void DisplayMenu()
        {
            bool isSelected = false;
            menuChoice = 1;

            while (!isSelected)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(logo + "\n");
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.WriteLine($"{(menuChoice == 1 ? color : "")}Start Game\u001b[0m");
                Console.WriteLine($"{(menuChoice == 2 ? color : "")}Score Board\u001b[0m");
                Console.WriteLine($"{(menuChoice == 3 ? color : "")}Game History\u001b[0m");
                Console.WriteLine($"{(menuChoice == 4 ? color : "")}Exit Game\u001b[0m");
                Console.WriteLine("\nUse Up and Down to navigate and press the Enter key to select");
                key = Console.ReadKey(true);

                // Navigating menu
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
                //If menu item selected it runs method assigned to the option
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
                        case 3:
                            GameHistory();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Some unexcepted action occured");
                            break;
                    }
                }

            }
        }
        // Displays menu for starting the game
        public static void StartGame()
        {
            bool isSelected = false;
            menuChoice = 1;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(logo + "\n");
            Console.ResetColor();
            Console.WriteLine("Please enter your username");
            Data.userName = Console.ReadLine();

            while (!isSelected)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(logo + "\n");
                Console.ResetColor();
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
                // Depending on the menu option chosen the game is started with specific options
                if (isSelected)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Data.chosenLevel = "Easy";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(10);
                            // The new grid is genenrated until it is unique
                            while (!SudokuSolver.CheckUniqueness(Data.rawBoard))
                            {
                                ResetData();
                                GridBuilder.GenerateGrid();
                                SudokuGenerator.GenerateSudoku();
                                GridBuilder.FillNumbers(10);
                            }
                            
                            Game.PlaySudoku();
                            break;
                        case 2:
                            Data.chosenLevel = "Medium";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(20);
                            while (!SudokuSolver.CheckUniqueness(Data.rawBoard))
                            {
                                ResetData();
                                GridBuilder.GenerateGrid();
                                SudokuGenerator.GenerateSudoku();
                                GridBuilder.FillNumbers(20);
                            }
                            Game.PlaySudoku();
                            break;
                        case 3:
                            Data.chosenLevel = "Hard";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(40);
                            while (!SudokuSolver.CheckUniqueness(Data.rawBoard))
                            {
                                ResetData();
                                GridBuilder.GenerateGrid();
                                SudokuGenerator.GenerateSudoku();
                                GridBuilder.FillNumbers(40);
                            }
                            Game.PlaySudoku();
                            break;
                        case 4:
                            Data.chosenLevel = "None";
                            GridBuilder.GenerateGrid();
                            SudokuGenerator.GenerateSudoku();
                            GridBuilder.FillNumbers(0);
                            Console.ReadKey();
                            DisplayMenu();
                            ResetData();
                            //Game.PlaySudoku();
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
        // This menu option displays score board to the user
        public static void ScoreBoard()
        {
            // The data is fetched from database using get method
            List<ScoreModel> scores = Database.Database.GetScores();
            int counter = 0;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(logo + "\n");
            Console.ResetColor();
            Console.CursorVisible = false;

            foreach (ScoreModel score in scores)
            {
                counter += 1;
                Console.WriteLine($"{counter}. {score.Username} {score.Level} {score.Score}");
            }


            Console.WriteLine($"Press ESC to go back to main menu...");
            key = Console.ReadKey();
            DisplayMenu();
        }
        // It allows player to see a replay of their previous games.
        public static void GameHistory()
        {
            bool isSelected = false;
            menuChoice = 1;
            List<GameModel> games = Database.Database.GetGames();

            // Displays interactive board of games
            while(!isSelected)
            {
                int counter = 0;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(logo + "\n");
                Console.ResetColor();
                Console.CursorVisible = false;
                foreach (GameModel game in games)
                {
                    counter += 1;
                    Console.WriteLine($"{(menuChoice == counter ? color : "")} {counter}. {game.Username} {game.Date}\u001b[0m");
                }
                Console.WriteLine("\nUse Up and Down to navigate and press the Enter key to select");
                Console.WriteLine("\nPress ESC to go back");
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        menuChoice = (menuChoice == counter ? 1 : menuChoice + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        menuChoice = (menuChoice == 1 ? counter : menuChoice - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                    case ConsoleKey.Escape:
                        DisplayMenu();
                        isSelected = true;
                        break;
                }
                // The chosen game is found in the database and its data is fetched
                if (isSelected)
                {
                    switch (menuChoice)
                    {
                        default:
                            GridBuilder.GenerateGrid();
                            Dictionary<string, int> test = Database.Database.GetNumberFields(games[menuChoice - 1].ID);
                            Data.numberFields = Database.Database.GetNumberFields(games[menuChoice - 1].ID);
                            GridBuilder.FillNumbers(0, Database.Database.GetIndexesToHide(games[menuChoice - 1].ID), Database.Database.GetEditableFields(games[menuChoice - 1].ID));
                            Game.PlayReplay(games[menuChoice - 1].ID);
                            break;
                    }
                }

            }
        }
        // Resets data in the static helper class
        private static void ResetData()
        {
            Data.indexesToHide.Clear();
            Data.playerGuesses.Clear();
            Data.numberFields.Clear();
            Data.editableFields.Clear();
        }
    }
}
