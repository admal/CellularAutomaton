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
            statements = new ObservableCollection<StatementViewModel>(
                rule.Statements.Select(s=>new StatementViewModel(s,this)));

            addStatementCommand = new BasicCommand(AddStatement, ()=>true );
            removeRuleCommand = new BasicCommand(RemoveItself, ()=>true );
        }

        private void RemoveItself()
        {
            ruleSetViewModel.RemoveRule(this);
        }

        private void AddStatement()
        {
            var newStatement = new Statement(null, new Sentence(1,CellState.Any, Area.Neghbourhood, 0));
            statements.Add(new StatementViewModel(newStatement,this));
            rule.Statements.Add(newStatement);
        }

        public void RemoveStatement(StatementViewModel viewModel)
        {
            statements.Remove(viewModel);
            rule.Statements.Remove(viewModel.Statement);
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