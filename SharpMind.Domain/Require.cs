using System;

namespace SharpMind.Domain
{
    public class Require
    {
        public static void That(bool condition, string errorMessage)
        {
            if (!condition)
            {
                throw new DomainRuleViolationException(errorMessage);
            }
        }

        public static void That(Func<bool> condition, string errorMessage)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (!condition())
            {
                throw new DomainRuleViolationException(errorMessage);
            }
        }

        public static void NotNullOrWhitespace(string property, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(property))
            {
                throw new DomainRuleViolationException($"{propertyName} cannot be null or whitespace");
            }
        }

        public static void ArgNotNull(object property, string propertyName)
        {
            if (property == null)
            {
                throw new DomainRuleViolationException($"{propertyName} cannot be null");
            }
        }
    }

    public class GameRequire
    {
        public static void That(Func<bool> condition,
            string errorMesssage,
            GamesViolationCodes violationCode)
        {
            if (condition == null)
            {
                throw new InvalidOperationException("Game rule condition must be set");
            }

            if (!condition())
            {
                throw new GameRulesViolationException(errorMesssage, violationCode);
            }
        }
    }

}
