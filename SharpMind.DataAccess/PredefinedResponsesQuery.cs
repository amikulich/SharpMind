﻿using System;
using System.Linq;
using SharpMind.DataAccess.Infrastructure;
using SharpMind.DataAccess.Mappings;

namespace SharpMind.DataAccess
{
    public class PredefinedResponsesQuery
    {
        private static readonly Random Random = new Random();

        public string Execute(string key)
        {
            using (var dbContext = new MongoDriverWrapper())
            {
                var map = dbContext
                    .All<PredefinedResponseMap>()
                    .FirstOrDefault(x => x.Keys.Contains(key));

                if (map != null && map.AlternativeResponses.Any())
                {
                    var number = Random.Next(-1, map.AlternativeResponses.Count());

                    return number == -1 ? map.Response : map.AlternativeResponses.ElementAt(number);
                }

                return map?.Response;
            }
        }
    }
}
