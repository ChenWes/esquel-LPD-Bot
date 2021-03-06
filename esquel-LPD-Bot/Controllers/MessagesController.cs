﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using esquel_LPD_Bot.Model;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using esquel_LPD_Bot.Dialog;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Autofac;
using esquel_LPD_Bot.LuisService;

namespace esquel_LPD_Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //// calculate something for us to return
                //int length = (activity.Text ?? string.Empty).Length;

                //// return our reply to the user
                //Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters ");
                //await connector.Conversations.ReplyToActivityAsync(reply);

                await Conversation.SendAsync(activity, () => new LPDSearchLuisDialog());
            }    
            else if(activity.Type == ActivityTypes.ConversationUpdate)
            {
                //the first time will send welcome message to user
                IConversationUpdateActivity update = activity;
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                {
                    var client = scope.Resolve<IConnectorClient>();
                    if (update.MembersAdded.Any())
                    {
                        var reply = activity.CreateReply();
                        foreach (var newMember in update.MembersAdded)
                        {
                            if (newMember.Id != activity.Recipient.Id)
                            {
                                reply.Text = Common.ConfigHelper.GetConfigValue("BotWelcomeMessage")?? "Welcome To Esquel LPD Project Bot";
                            }
                            else
                            {
                                reply.Text = Common.ConfigHelper.GetConfigValue("BotWelcomeMessage")?? "Welcome To Esquel LPD Project Bot";
                            }
                            await client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
            }        
            else
            {
                HandleSystemMessage(activity);
            }

            return new HttpResponseMessage(HttpStatusCode.Accepted);

            //var response = Request.CreateResponse(HttpStatusCode.OK);
            //return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}