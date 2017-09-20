using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using stockBotFramework.BotDialog;
using System.Collections.Generic;
using DataService;
using DataCore;

namespace stockBotFramework
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private MedicamentService _serviceMedicament;
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
                var responseMedicament = new List<Medicament>();
                if(activity.Text == ConversationDocteur.Medicaments)
                {
                    responseMedicament = (await GetMedicaments()).ToList();
                    Console.WriteLine(responseMedicament);
                }

                // return our reply to the user
                Activity reply = activity.CreateReply($"Nous avons trouvé {responseMedicament.Count()} médicaments. La liste des médicaments : {JsonConvert.SerializeObject(responseMedicament)}");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public async Task<IEnumerable<Medicament>> GetMedicaments()
        {
            _serviceMedicament = new MedicamentService();
            return await _serviceMedicament.get();
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