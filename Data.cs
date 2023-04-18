using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    // This is helper class that holds the data that is accessible for the entire solution

    public static class Data
    {
        public static string userName = ""; 
        public static string chosenLevel = "";
        public static int[,] rawBoard = new int[9, 9];
        public static Dictionary<string, int> numberFields = new Dictionary<string, int>();     //  Coordinates and number they contain
        public static Dictionary<string, int[]> inputFields = new Dictionary<string, int[]>()
        {
            {"A1", new int[] {5,2} },
            {"A2", new int[] {11,2} },
            {"A3", new int[] {17,2} },
            {"A4", new int[] {23,2} },
            {"A5", new int[] {29,2} },
            {"A6", new int[] {35,2} },
            {"A7", new int[] {41,2} },
            {"A8", new int[] {47,2} },
            {"A9", new int[] {53,2} },
            {"B1", new int[] {5,4} },
            {"B2", new int[] {11,4} },
            {"B3", new int[] {17,4} },
            {"B4", new int[] {23,4} },
            {"B5", new int[] {29,4} },
            {"B6", new int[] {35,4} },
            {"B7", new int[] {41,4} },
            {"B8", new int[] {47,4} },
            {"B9", new int[] {53,4} },
            {"C1", new int[] {5,6} },
            {"C2", new int[] {11,6} },
            {"C3", new int[] {17,6} },
            {"C4", new int[] {23,6} },
            {"C5", new int[] {29,6} },
            {"C6", new int[] {35,6} },
            {"C7", new int[] {41,6} },
            {"C8", new int[] {47,6} },
            {"C9", new int[] {53,6} },
            {"D1", new int[] {5,8} },
            {"D2", new int[] {11,8} },
            {"D3", new int[] {17,8} },
            {"D4", new int[] {23,8} },
            {"D5", new int[] {29,8} },
            {"D6", new int[] {35,8} },
            {"D7", new int[] {41,8} },
            {"D8", new int[] {47,8} },
            {"D9", new int[] {53,8} },
            {"E1", new int[] {5,10} },
            {"E2", new int[] {11,10} },
            {"E3", new int[] {17,10} },
            {"E4", new int[] {23,10} },
            {"E5", new int[] {29,10} },
            {"E6", new int[] {35,10} },
            {"E7", new int[] {41,10} },
            {"E8", new int[] {47,10} },
            {"E9", new int[] {53,10} },
            {"F1", new int[] {5,12} },
            {"F2", new int[] {11,12} },
            {"F3", new int[] {17,12} },
            {"F4", new int[] {23,12} },
            {"F5", new int[] {29,12} },
            {"F6", new int[] {35,12} },
            {"F7", new int[] {41,12} },
            {"F8", new int[] {47,12} },
            {"F9", new int[] {53,12} },
            {"G1", new int[] {5,14} },
            {"G2", new int[] {11,14} },
            {"G3", new int[] {17,14} },
            {"G4", new int[] {23,14} },
            {"G5", new int[] {29,14} },
            {"G6", new int[] {35,14} },
            {"G7", new int[] {41,14} },
            {"G8", new int[] {47,14} },
            {"G9", new int[] {53,14} },
            {"H1", new int[] {5,16} },
            {"H2", new int[] {11,16} },
            {"H3", new int[] {17,16} },
            {"H4", new int[] {23,16} },
            {"H5", new int[] {29,16} },
            {"H6", new int[] {35,16} },
            {"H7", new int[] {41,16} },
            {"H8", new int[] {47,16} },
            {"H9", new int[] {53,16} },
            {"I1", new int[] {5,18} },
            {"I2", new int[] {11,18} },
            {"I3", new int[] {17,18} },
            {"I4", new int[] {23,18} },
            {"I5", new int[] {29,18} },
            {"I6", new int[] {35,18} },
            {"I7", new int[] {41,18} },
            {"I8", new int[] {47,18} },
            {"I9", new int[] {53,18} },
        };      // Coordinates and their locations on the console
        public static List<string> editableFields = new List<string>();   //  Fields that can be edited/updated
        public static List<string> indexesToHide = new List<string>();   // Coordinates of hidden numbers
        public static Dictionary<string, int> playerGuesses = new Dictionary<string, int>(); // Contains player guess for specific cell
    }
}
