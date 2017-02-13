using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using esquel_LPD_Bot.Model;
using esquel_LPD_Bot.LPDService;

namespace esquel_LPD_Bot.Dialog
{
    [Serializable]
    public class LPDSearchDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceiveStart);
        }

        private async Task MessageReceiveStart(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            await context.PostAsync("hi , what can i help you ??");
            context.Wait(MessageReceiveChoice);
        }

        private async Task MessageReceiveChoice(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Equals("search garment style", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("please entity garment style no ");
                context.Wait(MessageReceiveSearchGarmentStyle);
            }
            else
            {
                await context.PostAsync("sorry , i can not support you operation");
                context.Wait(MessageReceiveChoice);
            }
        }

        private async Task MessageReceiveSearchGarmentStyle(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (!string.IsNullOrEmpty(message.Text.Trim()))
            {
                var replyMessage = context.MakeMessage();
                replyMessage.Attachments = new List<Attachment>();

                await SearchGarmentStyle(replyMessage, message.Text);
                await context.PostAsync(replyMessage);
            }
            else
            {
                await context.PostAsync("sorry , can not search garment style");
            }

            context.Wait(MessageReceiveChoice);
        }

        private static async Task SearchGarmentStyle(IMessageActivity replyMessage, string pi_GarmentStyle)
        {
            StyleProduct l_styleProduct = await new GarmentStyleHelper().GarmentStyleSearch(pi_GarmentStyle);

            if (l_styleProduct != null)
            {
                List<CardImage> cardImages = new List<CardImage>();
                cardImages.Add(new CardImage(url: l_styleProduct.productStyles.imageURL));                
                List<CardAction> cardButtons = new List<CardAction>();
                CardAction plButton = new CardAction()
                {
                    Value = l_styleProduct.productStyles.imageURL,
                    Type = "openUrl",
                    Title = "View Image"
                };
                cardButtons.Add(plButton);
                HeroCard plCard = new HeroCard()
                {
                    Title = l_styleProduct.linePlanProducts.productID,
                    Subtitle = "(" + l_styleProduct.linePlanProducts.productVersion + l_styleProduct.linePlanProducts.productVersionSerialNo + ")",
                    Images = cardImages,
                    Buttons = cardButtons
                };
                Attachment plAttachment = plCard.ToAttachment();
                replyMessage.Attachments.Add(plAttachment);
            }
            else
            {
                List<CardAction> cardButtons = new List<CardAction>();
                CardAction plButton = new CardAction()
                {
                    Value = "https://<OAuthSignInURL>",
                    Type = "signin",
                    Title = "Nothing Found"
                };
                cardButtons.Add(plButton);
                SigninCard plCard = new SigninCard(text: "Please Entry Garment Style No", buttons: cardButtons);
                Attachment plAttachment = plCard.ToAttachment();
                replyMessage.Attachments.Add(plAttachment);
            }
        }
    }
}