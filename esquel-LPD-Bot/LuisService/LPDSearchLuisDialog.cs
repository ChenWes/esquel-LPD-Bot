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
    public class LPDSearchLuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Common.ConfigHelper.GetConfigValue("UnknowLuisIntentMessage") ?? "sorry , i have no idea what you talking about.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Hello")]
        public async Task Hello(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Common.ConfigHelper.GetConfigValue("BotWelcomeMessage") ?? "hi ,Welcome To Esquel LPD Project Bot.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("SearchGarmentStyle")]
        public async Task SearchGarmentStyle(IDialogContext context, LuisResult result)
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
                await context.PostAsync($"sorry , the garment style can not found in you message [ { result.Query } ].");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("SearchFabric")]
        public async Task SearchFabric(IDialogContext context, LuisResult result)
        {
            string l_fabricNo = string.Empty;
            EntityRecommendation rec;
            if (result.TryFindEntity("FabricNo", out rec))
            {
                l_fabricNo = rec.Entity.Trim();

                var replyMessage = context.MakeMessage();
                replyMessage.Attachments = new List<Attachment>();

                await SearchFabric(replyMessage, l_fabricNo);
                await context.PostAsync(replyMessage);
            }
            else
            {
                await context.PostAsync($"sorry , the fabric can not found in you message [ { result.Query } ].");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("SearchTrim")]
        public async Task SearchTrim(IDialogContext context, LuisResult result)
        {
            string l_trimNo = string.Empty;
            EntityRecommendation rec;
            if (result.TryFindEntity("TrimNo", out rec))
            {
                l_trimNo = rec.Entity.Trim();

                var replyMessage = context.MakeMessage();
                replyMessage.Attachments = new List<Attachment>();

                await SearchTrim(replyMessage, l_trimNo);
                await context.PostAsync(replyMessage);
            }
            else
            {
                await context.PostAsync($"sorry , the trim can not found in you message [ { result.Query } ].");
            }

            context.Wait(MessageReceived);
        }

        //----------------------------------------------------------------------------------------------------------------------------------

        private static async Task SearchGarmentStyle(IMessageActivity replyMessage, string pi_GarmentStyle)
        {
            char[] charsToTrim = { ' ', '\r' };
            pi_GarmentStyle = pi_GarmentStyle.Trim(charsToTrim);
            pi_GarmentStyle = pi_GarmentStyle.Replace(" ", string.Empty);

            StyleProduct l_styleProduct = await new GarmentStyleHelper().GarmentStyleSearch(pi_GarmentStyle);

            if (l_styleProduct != null)
            {
                replyMessage.Text = l_styleProduct.linePlanProducts.productID + "(" + l_styleProduct.linePlanProducts.productVersion + l_styleProduct.linePlanProducts.productVersionSerialNo + ")";

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
                            }
                            ,
                            Buttons = new List<CardAction>()
                            {
                                new CardAction
                                {
                                    Title=$"Primary Fabric",
                                    Value=$"search fabric { getColorway.primaryFabricID }",
                                    Type=ActionTypes.ImBack,
                                    Image=getColorway.PrimaryFabricImageUrl
                                }
                            }
                        }.ToAttachment());
                    }
                }
            }
            else
            {
                replyMessage.Text = $"sorry , the garment style [ { pi_GarmentStyle } ] can not found.";
            }
        }

        private static async Task SearchFabric(IMessageActivity replyMessage, string pi_Fabric)
        {
            char[] charsToTrim = { ' ', '\r' };
            pi_Fabric = pi_Fabric.Trim(charsToTrim);
            pi_Fabric = pi_Fabric.Replace(" ", string.Empty);

            Fabric l_fabric = await new FabricHelper().FabricSearch(pi_Fabric);

            if (l_fabric != null)
            {
                replyMessage.Text = l_fabric.fabricID;

                //button
                var actions = new List<CardAction>();
                actions.Add(new CardAction
                {
                    Title = $"View Image",
                    Value = l_fabric.imageURL,
                    Type = ActionTypes.OpenUrl,
                    Image = l_fabric.imageURL
                });

                //attachment layout style
                //replyMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                replyMessage.Attachments.Add(new HeroCard
                {
                    Title = l_fabric.fabricID,
                    Subtitle = l_fabric.fabricNo,
                    Text = string.Join(" ", l_fabric.longDescriptions),
                    Images = new List<CardImage>
                            {
                                new CardImage
                                {
                                    Url=l_fabric.imageURL,
                                    Alt=l_fabric.fabricID
                                }
                            }
                    ,
                    Buttons = actions
                }.ToAttachment());
            }
            else
            {
                replyMessage.Text = $"sorry , the fabric [ { pi_Fabric } ] can not found.";
            }
        }

        private static async Task SearchTrim(IMessageActivity replyMessage, string pi_Trim)
        {
            char[] charsToTrim = { ' ', '\r' };
            pi_Trim = pi_Trim.Trim(charsToTrim);
            pi_Trim = pi_Trim.Replace(" ", string.Empty);

            ApparelTrim l_trim = await new TrimHelper().TrimSearch(pi_Trim);

            if (l_trim != null)
            {
                replyMessage.Text = l_trim.apparelTrimID;

                //button
                var actions = new List<CardAction>();
                actions.Add(new CardAction
                {
                    Title = $"View Image",
                    Value = l_trim.imageURL,
                    Type = ActionTypes.OpenUrl,
                    Image = l_trim.imageURL
                });

                //attachment layout style
                //replyMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                replyMessage.Attachments.Add(new HeroCard
                {
                    Title = l_trim.apparelTrimID,
                    Subtitle = l_trim.apparelTrimNo,
                    Text = string.Join(" ", l_trim.longDescriptions),
                    Images = new List<CardImage>
                            {
                                new CardImage
                                {
                                    Url=l_trim.imageURL,
                                    Alt=l_trim.apparelTrimID
                                }
                            }
                    ,
                    Buttons = actions
                }.ToAttachment());
            }
            else
            {
                replyMessage.Text = $"sorry , the trim [ { pi_Trim } ] can not found.";
            }
        }
    }
}