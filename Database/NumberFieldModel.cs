using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Database
{
    public class NumberFieldModel
    {
        public int ID { get; set; }
        public int GameId { get; set; }
        public string Coordinate { get; set; }
        public int Value { get; set; }
    }
}
