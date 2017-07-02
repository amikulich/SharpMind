using System;
using System.Configuration;
using MongoDB.Driver;

namespace SharpMind.Shared
{
    public static class Logger
    {
        static readonly IMongoDatabase Database;

        static Logger()
        {
            IMongoClient client = new MongoClient(ConfigurationManager.AppSettings["MongoConnection"]);
            Database = client.GetDatabase(ConfigurationManager.AppSettings["MongoMainDatabase"]);
        }

        public static void Log(string message, string userName, Exception exception = null)
        {
            try
            {
                var collection = Database.GetCollection<LogEntry>("error_log");

                var logEntry = new LogEntry()
                {
                    DateTime = DateTime.UtcNow,
                    StackTrace = exception?.ToString(),
                    UserName = userName,
                    Message = message
                };

                collection.InsertOneAsync(logEntry);
            }
            catch { }
        }

        internal class LogEntry
        {
            public string StackTrace { get; set; }

            public string UserName { get; set; }

            public DateTime DateTime { get; set; }

            public string Message { get; set; }
        }
    }
}
