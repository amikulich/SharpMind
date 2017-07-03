using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace SharpMind.AppServices
{
    public class BotAddedResolver : IBotAddedResolver
    {
        private const string MathImgLink = "https://image.ibb.co/iPA12k/sigma.jpg";
        private const string MathDesc = "Mathematical quiz. Quick and complex. Your brain will be happy";

        public Activity CreateResponse(Activity activity)
        {
            var reply = activity.CreateReply("Hi! I have something to keep your mind sharp. ");

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = new List<Attachment>()
            {
                BuildCard("Play Math Warm Up", MathImgLink, MathDesc, Commands.GameMath, Commands.GameMathRules).ToAttachment(),
            };

            return reply;
        }

        private static ThumbnailCard BuildCard(string title, string imageUrl, string description, string playCommand, string rulesCommand)
        {
            var playAction = new CardAction("imBack", "Play", image: null, value: playCommand);
            var rulesAction = new CardAction("imBack", "Rules", image: null, value: rulesCommand);

            return new ThumbnailCard(title,
                                        subtitle: null,
                                        text: description,
                                        images: new List<CardImage>()
                                        {
                                            new CardImage(imageUrl)
                                        },
                                        buttons: new List<CardAction>()
                                        {
                                            playAction,
                                            rulesAction
                                        });
        }
    }

    public interface IBotAddedResolver
    {
        Activity CreateResponse(Activity activity);
    }
}
