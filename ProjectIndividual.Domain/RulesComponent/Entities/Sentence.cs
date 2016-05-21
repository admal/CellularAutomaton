using System;
using System.Collections;
using System.Collections.Generic;
using ProjectIndividual.Domain.GridComponent;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    [Serializable]
    public class Sentence
    {
        /// <summary>
        /// Number of cells in given state that should be in the considered area to GetValue() can return true.
        /// </summary>
        private int numOfCells;
        /// <summary>
        /// Searching for that state in the given Area
        /// </summary>
        private CellState seekState;
        /// <summary>
        /// What area should be considered for evaluation of next state of cell.
        /// </summary>
        private Area consideredArea;
        /// <summary>
        /// Index (from 1 to 5) of row or column, if whole neghbourhood is considered than it is 1.
        /// </summary>
        private int num;

        public Sentence(int numOfCells, CellState seekState, Area consideredArea, int num)
        {
            this.numOfCells = numOfCells;
            this.seekState = seekState;
            this.consideredArea = consideredArea;
            this.num = num;
        }

        public int NumOfCells
        {
            get { return numOfCells; }
            set { numOfCells = value; }
        }

        public CellState SeekState
        {
            get { return seekState; }
            set { seekState = value; }
        }

        public Area ConsideredArea
        {
            get { return consideredArea; }
            set { consideredArea = value; }
        }

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        public bool GetValue(Grid cells, Cell currCell)
        {
            int count = 0;
            switch (consideredArea)
            {
                    case Area.Column:
                    for (int i = -2; i <= 2; i++)
                    {
                        var currPos = new Position(currCell.X + num - 3, currCell.Y + i);
                        if (Equals(currPos, currCell.Position)) //skip itself
                        {
                            continue;
                        }
                        if (cells.GetCellState( currPos ) == seekState)
                        //Num-3 because columns are indexed from 1 and currCell is in the middle of neighbouorhood (no in the corner)
                        {
                            count++;
                        }
                    }
                    break;
                    case Area.Row:
                    for (int i = -2; i <= 2; i++)
                    {
                        var currPos = new Position(currCell.X + i, currCell.Y + num - 3);
                        if (Equals(currPos, currCell.Position)) //skip itself
                        {
                            continue;
                        }
                        if (cells.GetCellState(currPos) == seekState)
                        //Num-3 because rows are indexed from 1 and currCell is in the middle of neighbouorhood (no in the corner)
                        {
                            count++;
                        }
                    }
                    break;
                    case Area.Neghbourhood:
                    for (int i = -2; i <= 2 ; i++)
                    {
                        for (int j = -2; j <= 2; j++)
                        {
                            if (i==0 && j ==0)
                            {
                                continue;
                            }
                            var tmp = cells.GetCellState(currCell.X + i, currCell.Y + j);
                            if (tmp == seekState)
                            {
                                count++;
                            }
                        }
                    }
                    break;
            }
            return numOfCells == count;
        }
    }

    public enum Area
    {
        Neghbourhood, Row, Column
    }
}
