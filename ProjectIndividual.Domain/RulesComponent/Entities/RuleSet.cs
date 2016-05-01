using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndividual.Domain.GridComponent;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    public class RulesSet
    {
        private IList<Rule> rules;

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
#warning Assumption that rules are already sorted by priority!
            CellState retState = currCell.State;
            foreach (var rule in rules)
            {
                retState = rule.Apply(grid, currCell);
            }
            return retState;
        }
    }
}
