using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using esquel_LPD_Bot.Model;
using esquel_LPD_Bot.LPDService;

namespace esquel_LPD_Bot.LuisService
{    
    [LuisModel("a9dca225-cf60-4d88-ba19-873f008c204d", "f21ea5607c6b43589c12e6099b0a9614")]
    [Serializable]
    public class LPDSearchLuisDialog: LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("sorry , i have no idea what you talking about.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("SearchGarmentStyle")]
        public async Task SearchGarmentStyle(IDialogContext context,LuisResult result)
        {
            string l_garmentStyleNo = string.Empty;
            EntityRecommendation rec;
            if (result.TryFindEntity("GarmentStyleNo", out rec))
            {
                l_garmentStyleNo = rec.Entity.Trim();                

                var replyMessage = context.MakeMessage();
                replyMessage.Attachments = new List<Attachment>();

                await SearchGarmentStyle(replyMessage, l_garmentStyleNo);
                await context.PostAsync(replyMessage);
            }
            else
            {
                await context.PostAsync("sorry , the garment style can not found.");
            }

            context.Wait(MessageReceived);
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
                        }.ToAttachment());
                    }
                }
            }
            else
            {
                replyMessage.Text = "sorry , the garment style can not found.";
            }
        }
    }
}