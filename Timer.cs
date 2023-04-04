using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Timer
    {
        public static int[] TimerLocation = new int[2] {0,21};
        public static Stopwatch stopwatch;

        public static void StartTimer()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        public static void StopTimer()
        {
            stopwatch.Stop();
        }
        public static string GetTime()
        {
            return stopwatch.Elapsed.ToString();
        }

    }
}
