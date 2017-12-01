using AdaptiveCards;
using BotCasa.Classes;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BotCasa.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var reply = context.MakeMessage();

            if (activity.Value != null)
            {
                var activityValue = activity.Value.ToString();
                IotMessage serializedEntity = JsonConvert.DeserializeObject<IotMessage>(activityValue);
                SendDataToFunction(serializedEntity.lightName, serializedEntity.lightStatus);
                reply.Text = "Listo! Tu comando está hecho";
            }
            else
            {
                if (activity.Text.ToLower() == "arriba" || activity.Text.ToLower() == "abajo")
                {
                    string levelSelector = activity.Text;

                    HttpClient client = new HttpClient();
                    HttpResponseMessage response;
                    AdaptiveCard card = new AdaptiveCard();
                    response = await client.GetAsync(String.Format("http://aminespinoza.com/docs/tarjeta{0}.json", levelSelector));
                    var json = await response.Content.ReadAsStringAsync();
                    AdaptiveCardParseResult resultString = AdaptiveCard.FromJson(json);
                    card = resultString.Card;
                    IList<AdaptiveWarning> warnings = resultString.Warnings;


                    Attachment attachment = new Attachment()
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = card
                    };
                    reply.Attachments.Add(attachment);
                }
                else
                {
                    reply.Text = "Por favor, dime a qué piso quieres acceder";
                }
            }
            await context.PostAsync(reply);
            context.Wait(MessageReceivedAsync);
        }

        

        private async void SendDataToFunction(string light, string handler)
        {
            HttpClient request = new HttpClient();
            var requestedLink = new Uri("https://funcionescasa.azurewebsites.net/api/IotMessenger?code=bCnOkQEqLxiLYhkxZ4xXYXzTm7Wo9NwyOQuuhM/e1Jur0yVfGj0acw==");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestedLink);

            var sendString = String.Format("{{\"DeviceId\":\"testingDevice\",\"Message\":\"{0},{1}\"}}", light, handler);

            requestMessage.Content = new StringContent(sendString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await request.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}