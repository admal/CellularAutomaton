using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;

namespace ProjectIndividual.UI.ViewModels
{
    public class StatementViewModel : INotifyPropertyChanged
    {
        private Statement statement;
        private int num;
        private RuleViewModel ruleViewModel;
        private BasicCommand removeStatementCommand;
        public BasicCommand RemoveStatementCommand { get { return removeStatementCommand;} }
        public StatementViewModel(Statement statement, RuleViewModel viewModel, int num)
        {
            this.statement = statement;
            this.ruleViewModel = viewModel;
            this.num = num;
            removeStatementCommand = new BasicCommand(RemoveItSelf, ()=>true );
        }

        public int Num
        {
            get { return num; }
            set
            {
                num = value;
                if (num == 0)
                {
                    Connector = null;
                }
                RaisePropertyChanged("ShowConnector");
                RaisePropertyChanged("Connector");
            }
        }

        public CellSeekStateViewModel SeekState
        {
            get { return (CellSeekStateViewModel) statement.LogicalSentence.SeekState; }
            set { statement.LogicalSentence.SeekState = (CellState) value; }
        }
        public Statement Statement
        {
            get { return statement; }
        }

        private void RemoveItSelf()
        {
            ruleViewModel.RemoveStatement(this);
        }

        public LogicalConnector? Connector
        {
            get { return statement.Connector; }
            set { statement.Connector = value; }
        }

        public Sentence LogicalSentence
        {
            get { return statement.LogicalSentence; }
            set { statement.LogicalSentence = value; }
        }

        public bool ShowConnector 
        {
            get { return num != 0; }
        }

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
    }
}