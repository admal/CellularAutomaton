using System;

namespace ProjectIndividual.Domain.GridComponent.Entities
{
    public class Cell
    {
        private Position position;
        private CellState state;

        public Cell(Position position, CellState state)
        {
            this.position = position;
            this.state = state;
        }

        public Position Position { get { return position; } }

        public long X { get { return position.X; } }
        public long Y { get { return position.Y; } }

        public Cell Update()
        {
            throw new NotImplementedException();
        }
    }
    public enum CellState
    {
        Alive, Dead, Unvisited
    }
}
