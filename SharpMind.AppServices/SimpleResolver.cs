using Microsoft.Bot.Connector;
using SharpMind.DataAccess;

namespace SharpMind.AppServices
{
    public class SimpleResolver : ISimpleResolver
    {
        private readonly PredefinedResponsesQuery _predefinedResponsesQuery = new PredefinedResponsesQuery();

        public bool TryResolve(Activity activity, out string message)
        {
            message = _predefinedResponsesQuery.Execute(activity.Text.Replace("?", "").Trim().ToLower());

            return !string.IsNullOrEmpty(message);
        }
    }

    public interface ISimpleResolver
    {
        bool TryResolve(Activity activity, out string message);
    }
}
