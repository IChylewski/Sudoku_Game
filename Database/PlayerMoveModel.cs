using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Database
{
    public class PlayerMoveModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public int GameID { get; set; }
        public string EditableField { get; set; }
        public int Value { get; set; }
    }
}
