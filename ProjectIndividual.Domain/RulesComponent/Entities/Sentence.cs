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
        private int NumOfCells;
        /// <summary>
        /// Searching for that state in the given Area
        /// </summary>
        private CellState SeekState;
        /// <summary>
        /// What area should be considered for evaluation of next state of cell.
        /// </summary>
        private Area ConsideredArea;
        /// <summary>
        /// Index (from 1 to 5) of row or column, if whole neghbourhood is considered than it is 0.
        /// </summary>
        private int Num;

        public Sentence(int numOfCells, CellState seekState, Area consideredArea, int num)
        {
            NumOfCells = numOfCells;
            SeekState = seekState;
            ConsideredArea = consideredArea;
            Num = num;
        }

        public bool GetValue(Grid cells, Cell currCell)
        {
            int count = 0;
            switch (ConsideredArea)
            {
                    case Area.Column:
                    for (int i = -2; i <= 2; i++)
                    {
                        var currPos = new Position(currCell.X + Num - 3, currCell.Y + i);
                        if (Equals(currPos, currCell.Position)) //skip itself
                        {
                            continue;
                        }
                        if (cells.GetCellState( currPos ) == SeekState)
                        //Num-3 because columns are indexed from 1 and currCell is in the middle of neighbouorhood (no in the corner)
                        {
                            count++;
                        }
                    }
                    break;
                    case Area.Row:
                    for (int i = -2; i <= 2; i++)
                    {
                        var currPos = new Position(currCell.X + i, currCell.Y + Num - 3);
                        if (Equals(currPos, currCell.Position)) //skip itself
                        {
                            continue;
                        }
                        if (cells.GetCellState(currPos) == SeekState)
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
                            if (tmp == SeekState)
                            {
                                count++;
                            }
                        }
                    }
                    break;
            }
            return NumOfCells == count;
        }
    }

    public enum Area
    {
        Neghbourhood, Row, Column
    }
}
