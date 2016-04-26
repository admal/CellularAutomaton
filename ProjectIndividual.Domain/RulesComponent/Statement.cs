using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectIndividual.Domain.RulesComponent
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
