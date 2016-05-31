using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.Domain.GridComponent.Entities
{
    [Serializable]
    public class Grid
    {
        public const long MAX_POSITION = 30;
        private Dictionary<Position, Cell> visitedCells;
        private Dictionary<Position, Cell> newCellsGeneration;
        //rules
        private RulesSet rules;
        public Dictionary<Position, Cell> VisitedCells
        {
            get { return visitedCells; }
        }


        public IEnumerable<Cell> ImportantCells
        {
            get { return visitedCells.Values.Where(c => c.State != CellState.Unvisited); }
        } 
        public Cell GetCell(Position position)
        {
            Cell outCell;
            if (visitedCells.TryGetValue(position, out outCell))
                return outCell;
            throw new Exception("On given position there is not visited cell and given position is not neighbour of visited cell!");
        }

        public void ClearNewGenration()
        {
            newCellsGeneration.Clear();
        }
        public Cell GetCell(long x, long y)
        {
            return GetCell(new Position(x,y));
        }
        public RulesSet Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        public Grid(Cell[] initCells, RulesSet set) : this(initCells.ToList(), set)
        {
        }

        public Grid(IList<Cell> initCells, RulesSet set) : this(initCells)
        {
            rules = set;
        }

        public Grid(IList<Cell> initCells) : this()
        {
            foreach (var initCell in initCells)
            {
                visitedCells.Add(initCell.Position, initCell);
            }
        }
        public Grid()
        {
            visitedCells = new Dictionary<Position, Cell>();
            newCellsGeneration = new Dictionary<Position, Cell>();
        }


        public long Width
        {
            get
            {
                var xValues = visitedCells.Keys.Select(position => position.X).ToList();
                long min = xValues.Min(), max = xValues.Max();
                return max - min;
            }
        }

        public long Height
        {
            get
            {
                var yValues = visitedCells.Keys.Select(position => position.Y).ToList();
                long min = yValues.Min(), max = yValues.Max();
                return max - min;
            }
        }

        private void ComputeNextGeneration()
        {
            //newCellsGeneration = visitedCells;
            foreach (var entry in visitedCells)
            {
                Cell newCell = new Cell(entry.Value.Position, entry.Value.Update(this));
                newCellsGeneration.Add(newCell.Position, newCell);
            }
        }

        public CellState GetCellState(long x, long y)
        {
            var position = new Position(x,y);
            return GetCellState(position);
        }

        public CellState GetCellState(Position position)
        {
            Cell outCell;
            if (VisitedCells.TryGetValue(position, out outCell))
            {
                return visitedCells[position].State;
            }
            return CellState.Unvisited;
            
        }
        /// <summary>
        /// Updates VisitedCells dictionary by applaying rules to each cell
        /// </summary>
        /// <returns>List of new cells</returns>
        public List<Cell> UpdateGrid()
        {
            List<Cell> newCells= new List<Cell>();
            newCells = AddNeighbours();
            ComputeNextGeneration();
            //visitedCells = new Dictionary<Position, Cell>(newCellsGeneration);
            foreach (var cell in newCellsGeneration)
            {
                Cell outCell;
                if (visitedCells.TryGetValue(cell.Key, out outCell))
                {
                    outCell.State = cell.Value.State;
                }
                else
                {
                    visitedCells.Add(cell.Key, new Cell(cell.Value));
                }
            }
            newCellsGeneration.Clear();

            return newCells;
        }
        /// <summary>
        /// Add not added yet neighbours of visited cells.
        /// </summary>
        public List<Cell> AddNeighbours()
        {
            var newCells = new List<Cell>();
            for (int x = 0; x < visitedCells.Count; x++)
            {
                Cell currCell = visitedCells.ElementAt(x).Value;
                if (currCell.Position.X <= -MAX_POSITION || currCell.Y <= -MAX_POSITION) 
                    //do not add more neighbours for realy far cells
                {
                    continue;
                }
                if (currCell.State == CellState.Unvisited) //do not add neighbours for unvisited cells
                {
                    continue;
                }
                for (int i = -2; i <= 2; i++)
                {
                    for (int j = -2; j <= 2; j++)
                    {
                        var currPos = new Position(currCell.X + i, currCell.Y + j);
                        if (!visitedCells.ContainsKey(currPos))
                        {
                            var cell = new Cell(currPos, CellState.Unvisited);
                            visitedCells.Add(currPos, cell);
                            newCells.Add(cell);
                        }
                    }
                }
            }
            return newCells;
        }

        public CellState SwitchCellState(Position position)
        {
            var currState = GetCellState(position);
            switch (currState)
            {
                case CellState.Alive:
                    VisitedCells[position].State = CellState.Dead;
                    return  CellState.Dead;
                case CellState.Dead:
                    VisitedCells[position].State = CellState.Alive;
                    return CellState.Alive;
                case CellState.Unvisited:
                    if (VisitedCells.ContainsKey(position))
                    {
                        VisitedCells[position].State = CellState.Alive;
                    }
                    else
                    {
                        VisitedCells.Add(position, new Cell(position, CellState.Alive));
                    }
                    return CellState.Alive;
            }
            return CellState.Unvisited;
        }
        /// <summary>
        /// Removes cell from grid
        /// </summary>
        /// <param name="position">position of cell to be removed</param>
        /// <returns>true if cell was removed (it has already existed in grid)</returns>
        public bool RemoveCell(Position position)
        {
            if (VisitedCells.ContainsKey(position))
            {
                VisitedCells.Remove(position);
                return true;
            }
            return false;
        }
    }
}
