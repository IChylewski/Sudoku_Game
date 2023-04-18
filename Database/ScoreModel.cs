using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Database
{
    public class ScoreModel
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public string Username { get; set; }
        public double Score { get; set; }
        public string Level { get; set; }
        
    }
}
