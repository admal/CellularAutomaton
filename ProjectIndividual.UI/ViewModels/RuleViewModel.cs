using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.UI.ViewModels
{
    public class RuleViewModel
    {
        private Rule rule;

        public RuleViewModel(Rule rule)
        {
            this.rule = rule;
            
        }

        public CellState InputState
        {
            get { return rule.InputState; }
            set { rule.InputState = value; }
        }

        public uint Priority
        {
            get { return (uint)rule.Priority; }
            set { rule.Priority = (int) value; }
        }

        public CellState RetState
        {
            get { return rule.RetState; }
            set { rule.RetState = value; }
        }

        public ObservableCollection<Statement> Statements
        {
            get { return new ObservableCollection<Statement>(rule.Statements);}
            set { rule.Statements = new List<Statement>(value);}
        } 
    }
}