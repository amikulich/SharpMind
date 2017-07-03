using AdaptiveCards;
using Microsoft.Bot.Connector;

namespace SharpMind.AppServices
{
    public static class Extensions
    {
        public static Attachment ToAttachment(this AdaptiveCard adaptiveCard)
        {
            return new Attachment()
            {
                Content = adaptiveCard,
                ContentType = AdaptiveCard.ContentType
            };
        }
    }
}
