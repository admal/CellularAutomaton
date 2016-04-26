using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectIndividual.Domain.GridComponent
{
    public class Cell
    {
        private Position position;

        public Position Position { get { return position; } }

        public enum CellState
        {
            Alive, Dead, Unvisited
        }
    }
}
