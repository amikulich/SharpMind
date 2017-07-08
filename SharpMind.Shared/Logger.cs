using System;
using System.Configuration;
using System.Threading.Tasks;
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

        public static void LogMessage(string message, string userName, string activityType, string channel)
        {
            Task.Factory.StartNew(() => 
            {
                var collection = Database.GetCollection<LogEntry>("message_log");

                var logEntry = new LogEntry()
                {
                    DateTime = DateTime.UtcNow,
                    UserName = userName,
                    ActivityType = activityType,
                    Message = message
                };

                collection.InsertOneAsync(logEntry);
            });
        }

        public static void LogException(string message, string userName, string activityType, string channel, Exception exception)
        {
            try
            {
                var collection = Database.GetCollection<LogEntry>("error_log");

                var logEntry = new LogEntry()
                {
                    DateTime = DateTime.UtcNow,
                    StackTrace = exception?.ToString(),
                    UserName = userName,
                    ActivityType = activityType,
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

            public string ActivityType { get; set; }
        }
    }
}
