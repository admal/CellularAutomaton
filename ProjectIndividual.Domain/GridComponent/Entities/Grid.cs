using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.Domain.GridComponent.Entities
{
    public class Grid
    {
        private const long MAX_POSITION = 100000;
        private Dictionary<Position, Cell> visitedCells;
        private Dictionary<Position, Cell> newCellsGeneration;
        //rules
        private RulesSet rules;

        public Dictionary<Position, Cell> VisitedCells
        {
            get { return visitedCells; }
        }

        public Cell GetCell(Position position)
        {
            if (visitedCells.ContainsKey(position))
            {
                return visitedCells[position];
            }
            throw new Exception("On given position there is not visited cell and given position is not neighbour of visited cell!");
        }

        public Cell GetCell(long x, long y)
        {
            return GetCell(new Position(x,y));
        }
        public RulesSet Rules
        {
            get { return rules; }
        }

        public Grid(Cell[] initCells, RulesSet set) : this(initCells.ToList(), set)
        {
        }

        public Grid(IList<Cell> initCells, RulesSet set) : this(initCells)
        {
            rules = set;
            //foreach (var initCell in initCells)
            //{
            //    VisitedCells.Add(initCell.Position,initCell);
            //}
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


        public long Width { get { throw new NotImplementedException(); } }
        public long Height { get { throw new NotImplementedException(); } }

        private void ComputeNextGeneration()
        {
            newCellsGeneration = visitedCells;
            foreach (var entry in newCellsGeneration)
            {
                newCellsGeneration[entry.Key].Update(this);
            }
        }

        public CellState GetCellState(long x, long y)
        {
            var position = new Position(x,y);
            return GetCellState(position);
        }

        public CellState GetCellState(Position position)
        {
            if(!visitedCells.ContainsKey(position))
                return CellState.Unvisited;
            return visitedCells[position].State;
        }
        public void UpdateGrid()
        {
            AddNeighbours();
            ComputeNextGeneration();
            visitedCells = newCellsGeneration;
        }
        /// <summary>
        /// Add not added yet neighbours of visited cells.
        /// </summary>
        public void AddNeighbours()
        {
            for (int x = 0; x < visitedCells.Count; x++)
            {

                //Cell currCell = entry.Value;
                Cell currCell = visitedCells.ElementAt(x).Value;
                if (currCell.State == CellState.Unvisited)
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
                            visitedCells.Add(currPos, new Cell(currPos, CellState.Unvisited));
                        }
                    }
                }
            }
        }
    }
}
