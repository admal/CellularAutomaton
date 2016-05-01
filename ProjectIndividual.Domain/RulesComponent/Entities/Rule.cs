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

        public Rule(IList<Statement> statements, CellState retState, int priority)
        {
            this.statements = statements;
            this.retState = retState;
            this.priority = priority;
        }

        public int Priority
        {
            get { return priority; }
        }

        public CellState Apply(Grid grid, Cell currCell)
        {
            bool ruleSatisfied = false;
            foreach (var statement in statements)
            {
                switch (statement.Connector)
                {
                    case LogicalConnector.And:
                        ruleSatisfied = ruleSatisfied && statement.LogicalSentence.GetValue(grid, currCell);
                        break;
                    case LogicalConnector.Or:
                        ruleSatisfied = ruleSatisfied || statement.LogicalSentence.GetValue(grid, currCell);
                        break;
                    case null:
                        ruleSatisfied = statement.LogicalSentence.GetValue(grid, currCell);
                        break;
                }
            }
            return ruleSatisfied ? retState : currCell.State;
        }
    }
}
