using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SharpMind.Shared;

namespace SharpMind.AppServices
{
    public interface IActivityResolverFacade
    {
        Task ResolveActivityAsync(Activity activity);
    }

    public class ActivityResolverFacade : IActivityResolverFacade
    {
        private readonly ISimpleResolver _simpleResolver;
        private readonly IBotAddedResolver _botAddedResolver;

        public ActivityResolverFacade(ISimpleResolver simpleResolver,
            IBotAddedResolver botAddedResolver)
        {
            _simpleResolver = simpleResolver;
            _botAddedResolver = botAddedResolver;
        }

        public async Task ResolveActivityAsync(Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            try
            {
                Logger.Log(activity.Text, activity.From.Name);

                switch (activity.Type)
                {
                    case ActivityTypes.Message:
                        string message;
                        if (_simpleResolver.TryResolve(activity, out message))
                        {
                            await connector
                                    .Conversations
                                    .ReplyToActivityAsync(
                                        activity.CreateReply(message));
                            return;
                        }

                        await Conversation.SendAsync(activity, () => new DefaultDialog());

                        break;
                    case ActivityTypes.ContactRelationUpdate:
                        await connector
                                .Conversations
                                .ReplyToActivityAsync(
                                     _botAddedResolver.CreateResponse(activity));
                        break;
                }
            }
            catch (Exception e)
            {
                var reply = activity.CreateReply("I crashed, sorry");

                await connector.Conversations.ReplyToActivityAsync(reply);

                Logger.Log($"Crash on message:{activity.Text}", activity.From.Name, e);
            }
        }
    }
}
