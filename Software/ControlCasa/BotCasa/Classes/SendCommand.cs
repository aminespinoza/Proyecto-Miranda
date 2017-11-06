using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace BotCasa.Classes
{
    public class SendCommand
    {
        static string iotHub = "secondhubcasa";
        static string deviceId = "maquinaCasa";
        static string api = "2016-02-03";

        public static string SendDataToHub(string light, string handler)
        {

            IotMessage body = new IotMessage { Timestamp = DateTime.Now.ToString(), lightNumber = light, lightStatus = handler };
            string restUri = String.Format("https://{0}.azure-devices.net/devices/{1}/messages/events?api-version={2}", iotHub, deviceId, api);
            string sas = "SharedAccessSignature sr=secondhubcasa.azure-devices.net%2Fdevices%2FmaquinaCasa&sig=pXYSfn9OM5PvglTCuRXvg2FMzmgt1ivPeJxhwxyGN1w%3D&se=1505405813";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", sas);

            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var result = client.PostAsync(restUri, content).Result;

            return String.Format("Tu luz {0}, tiene el estado {1}", light, handler);
        }
    }
}