using System;
using System.Collections.Generic;

namespace ProjectIndividual.Domain.GridComponent.Entities
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
