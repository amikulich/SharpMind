using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SharpMind.AppServices
{
    [Serializable]
    public class DefaultDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            switch (message.Text.ToLower())
            {
                case Commands.GameMath:
                    context.Call(new GameMathDialog(), MathGameFinishedCallback);
                    break;
            }
        }

        public async Task MathGameFinishedCallback(IDialogContext context,
            IAwaitable<GameMathStats> result)
        {
            var mathGameResult = await result;
            await context.PostAsync($"Total time: {mathGameResult.Seconds} seconds. Score - {mathGameResult.Score}");

            var reply = ((Activity)context.Activity).CreateReply();
            reply.Text = "Highscores";
            reply.AttachmentLayout = AttachmentLayoutTypes.List;
            reply.Attachments = new List<Attachment>()
            {
                GetCard(mathGameResult.Highscores).ToAttachment()
            };

            await context.PostAsync(reply);
        }

        private AdaptiveCard GetCard(IEnumerable<GameMathRanking> highscores)
        {
            var columnSet = new ColumnSet()
            {
                Speak = "Highscores"
            };

            var colRank = new Column();
            colRank.Items.Add(new TextBlock()
            {
                Text = "Rank",
                Size = TextSize.Medium
            });

            var colUser = new Column();
            colUser.Items.Add(new TextBlock()
            {
                Text = "User",
                Size = TextSize.Medium
            });

            var colScore = new Column();
            colScore.Items.Add(new TextBlock()
            {
                Text = "Score",
                Size = TextSize.Medium
            });

            var colTime = new Column();
            colTime.Items.Add(new TextBlock()
            {
                Text = "Time",
                Size = TextSize.Medium
            });

            int pos = 1;
            foreach (var ranking in highscores)
            {
                colRank.Items.Add(new TextBlock()
                {
                    Text = pos++.ToString(),
                });
                colUser.Items.Add(new TextBlock()
                {
                    Text = ranking.UserName,
                });
                colScore.Items.Add(new TextBlock()
                {
                    Text = ranking.Score.ToString(),
                });
                colTime.Items.Add(new TextBlock()
                {
                    Text = ranking.Seconds.ToString() + "s",
                });
            }

            columnSet.Columns.Add(colRank);
            columnSet.Columns.Add(colUser);
            columnSet.Columns.Add(colScore);
            columnSet.Columns.Add(colTime);

            return new AdaptiveCard()
            {
                Body = new List<CardElement>()
                {
                    columnSet
                }
            };
        }
    }
}
