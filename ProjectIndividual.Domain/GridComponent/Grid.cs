using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectIndividual.Domain.GridComponent
{
    public class Grid
    {
        private Dictionary<Position, Cell> VisitedCells;
        private Dictionary<Position, Cell> NewCellsGeneration;

        //rules

        public long Width { get { throw new NotImplementedException(); } }
        public long Height { get { throw new NotImplementedException(); } }

        public void Update() { }
    }
}
