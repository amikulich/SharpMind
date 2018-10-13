using System;
using System.Linq;

namespace SharpMind.Domain.Math
{
    [Serializable]
    public partial class GameMath
    {
        private readonly string _userName;
        private readonly int _entireQuestionsCount;
        private readonly DateTime _startTime;

        private int _currentQuestionNumber;

        private int QuestionsLeft =>
            _entireQuestionsCount - _currentQuestionNumber;

        private DateTime _endTime;

        private readonly Question[] _questions;

        protected GameMath()
        {
            //test row
        }

        public GameMath(string userName, int questionsCount) : this()
        {
            Require.That(questionsCount > 0, "Questions count must be more than zero");

            _userName = userName;
            _entireQuestionsCount = questionsCount;
            _currentQuestionNumber = 0;

            _startTime = DateTime.UtcNow;

            _questions = new Question[questionsCount];
            for (int i = 0; i < questionsCount; i++)
            {
                _questions[i] = Question.QuestionGenerator.CreateQuestion();
            }
        }

        public bool HasMore()
        {
            return QuestionsLeft > 0;
        }

        public Question NextQuestion()
        {
            Require.That(HasMore(), "No questions left");

            return _questions[_currentQuestionNumber];
        }

        public void ApplyAnswer(int? answer)
        {
            _questions[_currentQuestionNumber++].Answer = answer;

            if (QuestionsLeft == 0)
            {
                _endTime = DateTime.UtcNow;
            }
        }

        public GameMathSummary GetSummary()
        {
            return new GameMathSummary(_userName,
                _startTime,
                _endTime,
                _entireQuestionsCount,
                _questions.Count(q => q.CorrectAnswer == q.Answer));
        }
    }
}
