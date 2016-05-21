using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    [Serializable]
    public class RulesSet
    {
        private IList<Rule> rules;
        public string Name { get; set; }

        public IList<Rule> Rules
        {
            get { return rules; }
        }

        public RulesSet()
        {
            rules = new List<Rule>();
        }

        public RulesSet(IList<Rule> rules)
        {
            this.rules = rules.OrderByDescending(r => r.Priority).ToList();
        }

        public CellState Apply(Grid grid, Cell currCell)
        {
            CellState retState = currCell.State;
            foreach (var rule in rules)
            {
                //skip rule if it is not possible to apply it to the current cell
                if (rule.InputState != currCell.State && rule.InputState != CellState.Any)
                    continue;

                var appliedState = rule.Apply(grid, currCell);

                //we assign new state only when new state actually does change the state of the cell, 
                //if 2 rules chenges state the one with higher priority is chosen
                retState = appliedState != currCell.State ? appliedState : retState;

            }
            return retState;
        }
    }
}
