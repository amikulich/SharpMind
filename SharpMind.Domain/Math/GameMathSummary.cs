using System;

namespace SharpMind.Domain.Math
{
    public class GameMathSummary
    {
        public GameMathSummary(string userName,
            DateTime startDateTime,
            DateTime endDateTime,
            int questionsCount,
            int score)
        {
            Require.That(endDateTime > startDateTime, "Game should be completed");

            UserName = userName;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            QuestionsCount = questionsCount;
            Score = score;
        }

        public TimeSpan GameTime => EndDateTime - StartDateTime;

        public int QuestionsCount { get; }

        public int Score { get; }

        public DateTime StartDateTime { get; }

        public DateTime EndDateTime { get; }

        public string UserName { get; }
    }

}
