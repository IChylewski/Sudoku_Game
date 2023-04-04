using Sudoku.Database;

namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.Database.Initialize();
            Menu.DisplayMenu();
        }
    }
}