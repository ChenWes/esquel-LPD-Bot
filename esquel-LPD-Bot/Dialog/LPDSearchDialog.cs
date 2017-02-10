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
                await context.PostAsync("sorry , i can not ");
                context.Wait(MessageReceiveStart);
            }
        }

        private async Task MessageReceiveSearchGarmentStyle(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            StyleProduct l_styleProduct = await new GarmentStyleHelper().GarmentStyleSearch(message.Text.Trim());

            if(l_styleProduct!=null)
            {
                await context.PostAsync($"you search garment style: { l_styleProduct.linePlanProducts.productID } : { l_styleProduct.linePlanProducts.productVersion } - { l_styleProduct.linePlanProducts.productVersionSerialNo } ");
            }
            else
            {
                await context.PostAsync("sorry , can not search garment style");
            }

            context.Wait(MessageReceiveChoice);                     
        }
    }
}