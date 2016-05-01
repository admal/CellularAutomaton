using System;
using System.Collections;
using System.Collections.Generic;
using ProjectIndividual.Domain.GridComponent;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    public class Sentence
    {
        /// <summary>
        /// Number of cells in given state that should be in the considered area to GetValue() can return true.
        /// </summary>
        private int NumOfCells;
        /// <summary>
        /// Searching for that stet in the given Area
        /// </summary>
        private CellState SeekState;
        ///// <summary>
        ///// Return state if GetValue() is true.
        ///// </summary>
        //private CellState RetState;
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
                        if (cells.GetCellState(currCell.X + i, currCell.Y + Num-1) == SeekState)
                        {
                            count++;
                        }
                    }
                    break;
                    case Area.Row:
                    for (int i = -2; i <= 2; i++)
                    {
                        if (cells.GetCellState(currCell.X + Num-1, currCell.Y+i) == SeekState)
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
                            if (i==j)
                            {
                                continue;
                            }
                            if (cells.GetCellState(currCell.X + i, currCell.Y+j) == SeekState)
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
