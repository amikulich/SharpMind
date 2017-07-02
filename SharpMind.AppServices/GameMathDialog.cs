using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SharpMind.DataAccess.Math;
using SharpMind.Domain.Math;

namespace SharpMind.AppServices
{
    [Serializable]
    public class GameMathDialog : IDialog<GameMathStats>
    {
        private GameMath _gameInstance;

        [NonSerialized]
        private IGameMathRankingsRepository _gameMathRankingsRepository;

        private const int DefaultQuestionsCount = 10;

        public GameMathDialog()
        {
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("let's start...");

            _gameInstance = new GameMath(context.Activity.From.Name, DefaultQuestionsCount);

            var question = _gameInstance.NextQuestion();

            await context.PostAsync(question.QuestionText);

            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context,
            IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            int answer;
            if (int.TryParse(message.Text, out answer))
            {
                _gameInstance.ApplyAnswer(answer);
            }
            else
            {
                _gameInstance.ApplyAnswer(null);
            }

            if (_gameInstance.HasMore())
            {
                var nextQuestion = _gameInstance.NextQuestion();

                await context.PostAsync(nextQuestion.QuestionText);
            }
            else
            {
                await context.PostAsync("Game is over");

                var summary = _gameInstance.GetSummary();

                _gameMathRankingsRepository = new GameMathRankingsRepository();

                _gameMathRankingsRepository.Save(summary);

                var rankings = _gameMathRankingsRepository.GetAll().ToList();

                context.Done(new GameMathStats((int)summary.GameTime.TotalSeconds,
                    summary.QuestionsCount,
                    summary.Score,
                    rankings
                        .Select(r => new GameMathRanking()
                        {
                            Score = r.Score,
                            Seconds = (int)(r.EndDateTime - r.StartDateTime).TotalSeconds,
                            UserName = r.UserName,
                            Date = r.EndDateTime
                        })
                       .OrderByDescending(r => r.Score)
                       .ThenBy(r => r.Seconds)
                       .Take(10)));
            }
        }
    }

    public class GameMathStats
    {
        internal GameMathStats(int seconds,
            int questionsCount,
            int score,
            IEnumerable<GameMathRanking> highscores)
        {
            Seconds = seconds;
            QuestionsCount = questionsCount;
            Score = score;
            Highscores = highscores;
        }

        public int Seconds { get; }

        public int QuestionsCount { get; private set; }

        public int Score { get; private set; }

        public IEnumerable<GameMathRanking> Highscores { get; private set; }
    }

    public class GameMathRanking
    {
        public float Score { get; set; }

        public int Seconds { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }
    }
}
