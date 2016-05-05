using System;

namespace ProjectIndividual.Domain.GridComponent.Entities
{
    [Serializable]
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

        public CellState State
        {
            get { return state; }
        }

        public CellState Update(Grid grid)
        {
            var retState = grid.Rules.Apply(grid, this);
            return retState;
        }

        public override string ToString()
        {
            return position.ToString() +" " + state;
        }
    }
    public enum CellState
    {
        Alive, Dead, Unvisited, Any
    }
}
