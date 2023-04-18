using Sudoku.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Database = Sudoku.Database.Database;

namespace Sudoku
{
    public static class Game
    {
        public static void PlaySudoku()
        {
            Console.CursorVisible = true;
            ConsoleKeyInfo key;
            string firstField = Data.editableFields[0];        // Index of first field to focus when the game starts
            int currentFieldIndex = 0;
            int[] firstFieldValue = Data.inputFields.First(x => x.Key == firstField).Value;   // Gets coordinates of the console cell of first editable field
            int[] newFieldValue;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(firstFieldValue[0], firstFieldValue[1]);   // Sets console cursor position to first editable field
            TimeSpan startTime = DateTime.Now.TimeOfDay;  // Stores time of the game start

            int gameId = Database.Database.GetGames().Last().ID;

            // Loop allows the console to display interactive grid, every iteration updates the values and enables functionality of the game
            while (true)
            {
                key = Console.ReadKey();
                string currentCoordinate;

                // If Left or Right allow clicked the next editable field is found and the cursor's position gets updated
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentFieldIndex == 0)
                            {
                                break;
                            }
                            currentFieldIndex -= 1;
                            newFieldValue = Data.inputFields.First(x => x.Key == Data.editableFields[currentFieldIndex]).Value;
                            Console.SetCursorPosition(newFieldValue[0], newFieldValue[1]);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentFieldIndex == Data.editableFields.Count - 1)
                            {
                                break;
                            }

                            currentFieldIndex += 1;

                            newFieldValue = Data.inputFields.First(x => x.Key == Data.editableFields[currentFieldIndex]).Value;
                            Console.SetCursorPosition(newFieldValue[0], newFieldValue[1]);
                            break;
                        }
                    case ConsoleKey.Escape:
                        ResetGame();
                        break;
                    // When number entered, the player guess is updated and his move saved in the database so it can be replayed later
                    default:
                        {
                            switch(key.Key)
                            {
                                case ConsoleKey.D1:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 1;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 1);
                                        break;
                                    }
                                case ConsoleKey.D2:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 2;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 2);
                                        break;
                                    }
                                case ConsoleKey.D3:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 3;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 3);
                                        break;
                                    }
                                case ConsoleKey.D4:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 4;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 4);
                                        break;
                                    }
                                case ConsoleKey.D5:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 5;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 5);
                                        break;
                                    }
                                case ConsoleKey.D6:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 6;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 6);
                                        break;
                                    }
                                case ConsoleKey.D7:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 7;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 7);
                                        break;
                                    }
                                case ConsoleKey.D8:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 8;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 8);
                                        break;
                                    }
                                case ConsoleKey.D9:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 9;
                                        Database.Database.AddPlayerMove(gameId, Data.userName, currentCoordinate, 9);
                                        break;
                                    }
                            }

                            // When number entered the position of the cursor resets to the new number can be entered

                            newFieldValue = Data.inputFields.First(x => x.Key == Data.editableFields[currentFieldIndex]).Value;
                            Console.SetCursorPosition(newFieldValue[0], newFieldValue[1]);

                            // Every time the new number is entered this method checks if the solution provided by player matches the generated solution
                            // If true the score is calculated saved in the database and message displayed
                            if (CheckIfSolved())
                            {
                                TimeSpan endTime = DateTime.Now.TimeOfDay;
                                double resultTime = (endTime - startTime).TotalSeconds;
                                double addScore = 0;
                                double maxScore = 1000;

                                switch(Data.chosenLevel)
                                {
                                    case "Easy":
                                        addScore = 0;
                                        break;
                                    case "Medium":
                                        addScore = 10000;
                                        break;
                                    case "Hard":
                                        addScore = 30000;
                                        break;
                                    default:
                                        addScore = 0;
                                        break;
                                }
                                // Personal idea to calculate score
                                double score = Math.Round((maxScore + addScore) / resultTime);

                                if(Data.chosenLevel != "None")
                                {
                                    Database.Database.AddScore(Data.userName, gameId, score, Data.chosenLevel);
                                }
                                

                                Console.SetCursorPosition(0, 25);
                                Console.WriteLine($"You won in {(endTime - startTime)}");
                                Console.WriteLine("Please press enter to go back to menu");
                                Console.ReadKey();
                                ResetGame();
                            }
                            break;
                        }
                }
            }
        }
        // Simply compares the guesses list to the generated puzzle grid
        public static bool CheckIfSolved()
        {
            bool solved = true;
            foreach(var element in Data.playerGuesses)
            {
                if (Data.numberFields.First(x => x.Key == element.Key).Value != element.Value)
                {
                    solved = false;
                }
            }
            return solved;
        }
        // Resets game and data fields to the initial state
        public static void ResetGame()
        {
            Data.indexesToHide.Clear();
            Data.playerGuesses.Clear();
            Data.numberFields.Clear();
            Data.editableFields.Clear();
            Menu.DisplayMenu();
        }

        // This method allows player to play "replay" of their game, it gets all player moves for the specific gameId and populates the grid move by move
        public static void PlayReplay(int gameId)
        {
            List<PlayerMoveModel> moves = Database.Database.GetPlayerMoves(gameId);
            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (var move in moves)
            {
                System.Threading.Thread.Sleep(1000);
                int[] fieldValue = Data.inputFields.First(x => x.Key == move.EditableField).Value;
                Console.SetCursorPosition(fieldValue[0], fieldValue[1]);
                Console.Write(move.Value);
            }

            Console.SetCursorPosition(0, 25);
            Console.WriteLine("Please press enter to go back to menu");
            Console.ReadKey();
            ResetGame();
        }
    }
}
