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
            set { state = value; }
        }

        public CellState Update(Grid grid)
        {
            //if (position.X > Grid.MAX_POSITION || 
            //    position.Y > Grid.MAX_POSITION || 
            //    position.X < -Grid.MAX_POSITION || 
            //    position.Y < -Grid.MAX_POSITION )
            //{
            //    grid.RemoveCell(this.position);
            //}
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
