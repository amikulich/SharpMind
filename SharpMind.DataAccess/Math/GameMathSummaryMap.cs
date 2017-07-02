using System;
using SharpMind.DataAccess.Infrastructure;
using SharpMind.DataAccess.Mappings;

namespace SharpMind.DataAccess.Math
{
    [Document(Name = "math_highscore")]
    internal class GameMathSummaryMap : MapBase
    {
        public string UserName { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int Score { get; set; }

        public int QuestionsCount { get; set; }
    }
}
