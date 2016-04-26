using System;
using System.Collections.Generic;
using ProjectIndividual.Domain.GridComponent;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    public class Rule
    {
        private IList<Statement> statements;
        private CellState retState;
        private int priority;

        public CellState Apply()
        {
            throw new NotImplementedException();
        }
    }
}
