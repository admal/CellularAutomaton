using System;
using System.Collections.Generic;
using ProjectIndividual.Domain.GridComponent;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    [Serializable]
    public class Rule
    {
        private IList<Statement> statements;
        private CellState retState;
        private CellState inputState;
        private int priority;

        public Rule(int priority)
        {
            this.priority = priority;
            statements = new List<Statement>();
        }
        public Rule(IList<Statement> statements, CellState retState, int priority, CellState inputState)
        {
            this.statements = statements;
            this.retState = retState;
            this.priority = priority;
            this.inputState = inputState;
        }

        public IList<Statement> Statements
        {
            get { return statements; }
            set { statements = value; }
        }

        public CellState RetState
        {
            get { return retState; }
            set { retState = value; }
        }

        public CellState InputState
        {
            get { return inputState; }
            set { inputState = value; }
        }

        public int Priority
        {
            get { return priority; } set { priority = value; }
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
