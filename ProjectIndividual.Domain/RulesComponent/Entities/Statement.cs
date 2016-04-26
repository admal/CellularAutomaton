namespace ProjectIndividual.Domain.RulesComponent.Entities
{
    public class Statement
    {
        private LogicalConnector connector;
        private Sentence LogicalSentence;
    }

    public enum LogicalConnector
    {
        And, Or
    }
}
