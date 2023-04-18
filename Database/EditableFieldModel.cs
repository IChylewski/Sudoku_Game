using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Database
{
    public class EditableFieldModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Coordinate { get; set; }
    }
}
