using Microsoft.Azure.Devices;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ControlCasa
{
    public partial class MainWindow : Window
    {
        static ServiceClient serviceClient;
        //static string connectionString = "HostName=hubcasatulancingo.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=kfSWlvhPZMFvWPs+oObC3bFAB5rlTUQ800Xo3lHGBoY=";
        static string connectionString = "HostName=secondhubcasa.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=cNJByRyM/1r5apig/TjO+bNxs25reC4hk6hvcalxnJY=";

        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;
        EventHubReceiver eventHubReceiver;
        DispatcherTimer timer;
        
        static SerialPort puertoSerial = new SerialPort();
        public MainWindow()
        {
            InitializeComponent();
            ctrlAlarm.master = this;

            string portName = "COM4";
            puertoSerial.PortName = portName;
            puertoSerial.BaudRate = 9600;

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
            eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver("3", DateTime.UtcNow);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            ctrlIndex.master = this;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await HandleReceivedInformation();
        }

        private async Task HandleReceivedInformation()
        {
            EventData eventData = await eventHubReceiver.ReceiveAsync();
            if (eventData != null)
            {
                string data = Encoding.UTF8.GetString(eventData.GetBytes());

                JObject serializedObject = JObject.Parse(data);
                string lightNumber = serializedObject["lightNumber"].ToString();
                string lightStatus = serializedObject["lightStatus"].ToString();
                DateTime lastMove = Convert.ToDateTime(serializedObject["date"]);

                HandleLights(lightNumber, lightStatus);
            }
        }

        public void HandleLights(string light, string status)
        {
            if (light == "13")
            {
                ctrlAlarm.TriggerAlarm();
                alarmSound.Source = (new Uri("../../Assets/sounds/alarm.mp3", UriKind.RelativeOrAbsolute));
                alarmSound.Play();
            }
            
            string finalCommand = string.Format("{0},{1}", light, status);

            puertoSerial.Open();
            puertoSerial.Write(finalCommand);
            puertoSerial.Close();
        }
    }
}
