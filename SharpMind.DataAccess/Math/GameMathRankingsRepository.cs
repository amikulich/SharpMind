using System.Collections.Generic;
using System.Linq;
using SharpMind.DataAccess.Infrastructure;
using SharpMind.Domain.Math;

namespace SharpMind.DataAccess.Math
{
    public interface IGameMathRankingsRepository
    {
        IEnumerable<GameMathSummary> GetAll();

        void Save(GameMathSummary summary);
    }

    public class GameMathRankingsRepository : IGameMathRankingsRepository
    {
        public IEnumerable<GameMathSummary> GetAll()
        {
            using (var dbContext = new MongoDriverWrapper())
            {
                var results = dbContext.All<GameMathSummaryMap>()
                    .OrderBy(x => x.Score);

                return results.Select(x => new GameMathSummary(
                                                x.UserName,
                                                x.StartDateTime,
                                                x.EndDateTime,
                                                x.QuestionsCount,
                                                x.Score));
            }
        }

        public void Save(GameMathSummary summary)
        {
            using (var dbContext = new MongoDriverWrapper())
            {
                var map = new GameMathSummaryMap()
                {
                    StartDateTime = summary.StartDateTime,
                    EndDateTime = summary.EndDateTime,
                    Score = summary.Score,
                    QuestionsCount = summary.QuestionsCount,
                    UserName = summary.UserName
                };

                dbContext.Save(map);
            }
        }
    }
}
