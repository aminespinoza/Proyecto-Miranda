using System;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace ControlAutomatico
{
    class Program
    {
        static Timer timer;
        static void Main(string[] args)
        {
            timer = new Timer(TimerCallback, null, 0, (60 * 60 * 1000));
            LogMessageInConsole("Aplicación iniciada");
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            DateTime thisMoment = DateTime.Now;

            if (thisMoment.Hour == 19)
            {
                SendLightStatusToFunction("12", "1");
                LogMessageInConsole("Garage light on");

                SendLightStatusToFunction("03", "1");
                LogMessageInConsole("Bar light on");
            }
            else if (thisMoment.Hour == 21)
            {
                SendLightStatusToFunction("08", "1");
                LogMessageInConsole("Kitchen light on");

                SendLightStatusToFunction("04", "1");
                LogMessageInConsole("Oscar light on");
            }
            else if (thisMoment.Hour == 23)
            {
                SendLightStatusToFunction("08", "0");
                LogMessageInConsole("Kitchen light off");
            }
            else if (thisMoment.Hour == 01)
            {
                SendLightStatusToFunction("03", "0");
                LogMessageInConsole("Bar light off");

                SendLightStatusToFunction("04", "0");
                LogMessageInConsole("Oscar light off");
            }
            else if (thisMoment.Hour == 07)
            {
                SendLightStatusToFunction("12", "0");
                LogMessageInConsole("Garage light off");
            }
            else
            {
                LogMessageInConsole("Any light handled this hour");
            }
        }

        public static async void SendLightStatusToFunction(string light, string handler)
        {
            HttpClient request = new HttpClient();
            var requestedLink = new Uri("https://funcionescasa.azurewebsites.net/api/IotMessenger?code=bCnOkQEqLxiLYhkxZ4xXYXzTm7Wo9NwyOQuuhM/e1Jur0yVfGj0acw==");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestedLink);

            var sendString = String.Format("{{\"DeviceId\":\"testingDevice\",\"Message\":\"{0},{1}\"}}", light, handler);

            requestMessage.Content = new StringContent(sendString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await request.SendAsync(requestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
        }

        private static void LogMessageInConsole(string message)
        {
            DateTime registeredTime = DateTime.Now;
            string finalMessage = String.Format("[{0}] New activity registered: {1}", registeredTime.TimeOfDay, message);
            Console.WriteLine(finalMessage);
        }
    }
}
