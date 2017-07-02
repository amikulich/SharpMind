using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMind.Domain
{
    public class DomainRuleViolationException : Exception
    {
        public DomainRuleViolationException(string errorMessage) :
            base(errorMessage)
        {
        }
    }

    public class GameRulesViolationException : DomainRuleViolationException
    {
        public GamesViolationCodes ErrorCode { get; private set; }

        public GameRulesViolationException(string errorMessage,
            GamesViolationCodes errorCode) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}
