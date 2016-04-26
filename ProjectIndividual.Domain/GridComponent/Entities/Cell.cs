namespace ProjectIndividual.Domain.GridComponent.Entities
{
    public class Cell
    {
        private Position position;

        public Position Position { get { return position; } }
    }
    public enum CellState
    {
        Alive, Dead, Unvisited
    }
}
