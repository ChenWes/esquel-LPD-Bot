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
                replyMessage.Text = l_styleProduct.linePlanProducts.productID + "(" + l_styleProduct.linePlanProducts.productVersion + l_styleProduct.linePlanProducts.productVersionSerialNo + ")";
                
                //button
                var actions = new List<CardAction>();
                actions.Add(new CardAction
                {
                    Title = $"ViewBOM",
                    Value = $"Search Garment Style",//this action will send message to Bot
                    Type = ActionTypes.ImBack,
                    Image = "https://placeholdit.imgix.net/~text?txtsize=16&txt=WesChen&w=125&h=40&txttrack=0&txtclr=000&txtfont=bold"
                });

                if (l_styleProduct.linePlanProducts.productMaterialConfigs != null && l_styleProduct.linePlanProducts.productMaterialConfigs.Count() > 0)
                {
                    //attachment layout style
                    replyMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                    //foreach add herocard
                    foreach (var getColorway in l_styleProduct.linePlanProducts.productMaterialConfigs)
                    {
                        replyMessage.Attachments.Add(new ThumbnailCard
                        {
                            Title = getColorway.colorway + "(" + getColorway.optionNo + ")",
                            Subtitle = getColorway.primaryFabricID,
                            Text = getColorway.pluNumber,
                            Images = new List<CardImage>
                            {
                                new CardImage
                                {
                                    Url=getColorway.PrimaryFabricImageUrl,
                                    Alt=getColorway.colorway + "(" +getColorway.optionNo + ")"
                                }
                            },
                            Buttons = actions
                            //,
                            //Tap = new CardAction()
                            //{
                            //    Title="imBack title",
                            //    Type= "imBack",
                            //    Image= "https://placeholdit.imgix.net/~text?txtsize=16&txt=WesChen&w=125&h=40&txttrack=0&txtclr=000&txtfont=bold",
                            //    Value="ss"
                            //}
                        }.ToAttachment());
                    }
                }
            }
            else
            {
                replyMessage.Text = "Sorry , Can Not Found Garment Style.";
            }
        }
    }
}