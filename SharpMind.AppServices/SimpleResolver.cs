using Microsoft.Bot.Connector;
using SharpMind.DataAccess;

namespace SharpMind.AppServices
{
    public class SimpleResolver : ISimpleResolver
    {
        private readonly PredefinedCommandsQuery _predefinedCommandsQuery = new PredefinedCommandsQuery();

        public bool TryResolve(Activity activity, out string message)
        {
            message = _predefinedCommandsQuery.Execute(activity.Text.Replace("?", "").Trim().ToLower());

            return !string.IsNullOrEmpty(message);
        }
    }

    public interface ISimpleResolver
    {
        bool TryResolve(Activity activity, out string message);
    }
}
