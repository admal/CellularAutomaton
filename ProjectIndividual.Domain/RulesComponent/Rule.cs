using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectIndividual.Domain.GridComponent;

namespace ProjectIndividual.Domain.RulesComponent
{
    public class Rule
    {
        private IList<Statement> statements;
        private Cell.CellState retState;
        private int priority;

        public Cell.CellState Apply()
        {
            throw new NotImplementedException();
        }
    }
}
