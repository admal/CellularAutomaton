namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    public class Statement
    {
        private LogicalConnector? connector;
        private Sentence logicalSentence;

        public Statement(LogicalConnector? connector, Sentence logicalSentence)
        {
            this.connector = connector;
            this.logicalSentence = logicalSentence;
        }

        public LogicalConnector? Connector
        {
            get { return connector; }
            set { connector = value; }
        }

        public Sentence LogicalSentence
        {
            get { return logicalSentence; }
        }
    }

    public enum LogicalConnector
    {
        And, Or
    }
}
