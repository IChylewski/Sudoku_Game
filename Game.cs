using Sudoku.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
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
            string firstField = Data.editableFields[0];
            int currentFieldIndex = 0;
            int[] firstFieldValue = Data.inputFields.First(x => x.Key == firstField).Value;
            int[] newFieldValue;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(firstFieldValue[0], firstFieldValue[1]);
            Timer.StartTimer();
            TimeSpan startTime = DateTime.Now.TimeOfDay;
            

            while (true)
            {
                key = Console.ReadKey();
                string currentCoordinate;
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
                    default:
                        {
                            switch(key.Key)
                            {
                                case ConsoleKey.D1:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 1;
                                        break;
                                    }
                                case ConsoleKey.D2:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 2;
                                        break;
                                    }
                                case ConsoleKey.D3:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 3;
                                        break;
                                    }
                                case ConsoleKey.D4:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 4;
                                        break;
                                    }
                                case ConsoleKey.D5:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 5;
                                        break;
                                    }
                                case ConsoleKey.D6:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 6;
                                        break;
                                    }
                                case ConsoleKey.D7:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 7;
                                        break;
                                    }
                                case ConsoleKey.D8:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 8;
                                        break;
                                    }
                                case ConsoleKey.D9:
                                    {
                                        currentCoordinate = Data.editableFields[currentFieldIndex];
                                        Data.playerGuesses[currentCoordinate] = 9;
                                        break;
                                    }
                            }

                            newFieldValue = Data.inputFields.First(x => x.Key == Data.editableFields[currentFieldIndex]).Value;
                            Console.SetCursorPosition(newFieldValue[0], newFieldValue[1]);

                            var test = Data.playerGuesses;

                            if (CheckIfSolved())
                            {
                                Timer.StopTimer();
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
                                        addScore = 500;
                                        break;
                                    case "Hard":
                                        addScore = 1000;
                                        break;
                                    default:
                                        addScore = 0;
                                        break;
                                }

                                double score = Math.Round(maxScore + addScore / resultTime);

                                if(Data.chosenLevel != "None")
                                {
                                    Database.Database.AddScore(Data.userName, score, Data.chosenLevel);
                                }
                                

                                Console.SetCursorPosition(0, 25);
                                Console.WriteLine($"You won in {Timer.GetTime()}");
                                Console.WriteLine("Please press enter to go back to menu");
                                Console.ReadKey();
                                ResetGame();
                            }
                            break;
                        }
                }
            }
        }

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
        public static void ResetGame()
        {
            Data.indexesToHide.Clear();
            Data.playerGuesses.Clear();
            Data.numberFields.Clear();
            Data.editableFields.Clear();
            Menu.DisplayMenu();
        }
    }
}
