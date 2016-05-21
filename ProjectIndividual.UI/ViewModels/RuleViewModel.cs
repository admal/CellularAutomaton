using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Annotations;
using ProjectIndividual.UI.Commands;

namespace ProjectIndividual.UI.ViewModels
{
    public class RuleViewModel : INotifyPropertyChanged
    {
        private RulesSetViewModel ruleSetViewModel;
        private Rule rule;
        private BasicCommand addStatementCommand;
        private BasicCommand removeRuleCommand;
        private ObservableCollection<StatementViewModel> statements; 
        public BasicCommand AddStatementCommand
        {
            get { return addStatementCommand; }
        }

        public BasicCommand RemoveRuleCommand
        {
            get { return removeRuleCommand; }
        }

        public Rule Rule
        {
            get { return rule; }
        }

        public RuleViewModel(Rule rule, RulesSetViewModel viewModel)
        {
            this.rule = rule;
            this.ruleSetViewModel = viewModel;
            statements = new ObservableCollection<StatementViewModel>();
            int i = 0;
            foreach (var statement in rule.Statements)
            {
                statements.Add(new StatementViewModel(statement ,this,i++));
            }
            
            addStatementCommand = new BasicCommand(AddStatement, ()=>true );
            removeRuleCommand = new BasicCommand(RemoveItself, ()=>true );
        }

        private void RemoveItself()
        {
            ruleSetViewModel.RemoveRule(this);
        }

        private void AddStatement()
        {
            Statement newStatement;
            newStatement = statements.Count == 0 ? 
                new Statement(null, new Sentence(1, CellState.Unvisited, Area.Neghbourhood, 1)) : 
                new Statement(LogicalConnector.And, new Sentence(1, CellState.Unvisited, Area.Neghbourhood, 1));
            int num = statements.Count;
            statements.Add(new StatementViewModel(newStatement,this, num));
            rule.Statements.Add(newStatement);
        }

        public void RemoveStatement(StatementViewModel viewModel)
        {
            statements.Remove(viewModel);
            rule.Statements.Remove(viewModel.Statement);
            for (int i = 0; i < statements.Count; i++)
            {
                statements[i].Num = i;
            }
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

        public CellOutStateViewModel RetState
        {
            get { return (CellOutStateViewModel) rule.RetState; }
            set { rule.RetState = (CellState) value; }
        }

        public ObservableCollection<StatementViewModel> Statements
        {
            get { return statements; }
        }

        #region IPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}