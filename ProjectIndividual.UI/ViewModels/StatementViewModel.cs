using System;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;

namespace ProjectIndividual.UI.ViewModels
{
    public class StatementViewModel
    {
        private Statement statement;
        private RuleViewModel ruleViewModel;
        private BasicCommand removeStatementCommand;
        public BasicCommand RemoveStatementCommand { get { return removeStatementCommand;} }
        public StatementViewModel(Statement statement, RuleViewModel viewModel)
        {
            this.statement = statement;
            ruleViewModel = viewModel;
            removeStatementCommand = new BasicCommand(RemoveItSelf, ()=>true );
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

    }
}