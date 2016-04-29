using System;
using System.Collections.Generic;

namespace ProjectIndividual.Domain.GridComponent.Entities
{
    public class Grid
    {
        private const long MAX_POSITION = 100000;
        private Dictionary<Position, Cell> VisitedCells;
        private Dictionary<Position, Cell> NewCellsGeneration;

        //rules

        public long Width { get { throw new NotImplementedException(); } }
        public long Height { get { throw new NotImplementedException(); } }

        public void ComputeNextGeneration()
        {
            foreach (var entry in VisitedCells)
            {
                NewCellsGeneration[entry.Key] = entry.Value.Update();
            }
        }

        public void UpdateGrid()
        {
            ComputeNextGeneration();
            VisitedCells = NewCellsGeneration;
        }
        /// <summary>
        /// Add not added yet neighbours of visited cells.
        /// </summary>
        public void AddNeighbours()
        {
            foreach (var entry in VisitedCells)
            {
                Cell currCell = entry.Value;
                for (int i = -2; i <= 2; i++)
                {
                    for (int j = -2; j <= 2; j++)
                    {
                        var currPos = new Position(currCell.X + i, currCell.Y + j);
                        if (!VisitedCells.ContainsKey(currPos))
                        {
                            VisitedCells.Add(currPos, new Cell(currPos, CellState.Unvisited));
                        }
                    }
                }
            }
        }
    }
}
