using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectIndividual.Domain.GridComponent;

namespace ProjectIndividual.Domain.RulesComponent
{
    public class Sentence
    {
        /// <summary>
        /// Number of cells in given state that should be in the considered area to GetValue() can return true.
        /// </summary>
        private int NumOfCells;
        /// <summary>
        /// Return state if GetValue() is true.
        /// </summary>
        private Cell.CellState State;
        /// <summary>
        /// What area should be considered for evaluation of next state of cell.
        /// </summary>
        private Area ConsideredArea;
        /// <summary>
        /// Index (from 1 to 5) of row or column, if whole neghbourhood is considered than it is 0.
        /// </summary>
        private int Num;
        public bool GetValue()
        {
            throw new NotImplementedException();
        }
    }

    public enum Area
    {
        Neghbourhood, Row, Column
    }
}
