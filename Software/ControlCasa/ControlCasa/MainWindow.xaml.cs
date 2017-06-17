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
        static string connectionString = "Tu cadena de conexión para IoT Hub";
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;
        EventHubReceiver eventHubReceiver;
        DispatcherTimer timer;

        bool luzUnoPrendida = false;
        bool luzDosPrendida = false;
        bool luzTresPrendida = false;
        bool luzCuatroPrendida = false;
        bool luzCincoPrendida = false;
        bool luzSeisPrendida = false;
        bool luzSietePrendida = false;
        bool luzOchoPrendida = false;
        bool luzNuevePrendida = false;
        bool luzDiezPrendida = false;
        bool luzOncePrendida = false;
        bool luzDocePrendida = false;

        static SerialPort puertoSerial = new SerialPort();
        public MainWindow()
        {
            InitializeComponent();

            string portName = "COM4";
            puertoSerial.PortName = portName;
            puertoSerial.BaudRate = 9600;

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
            eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver("3", DateTime.UtcNow);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await HandleReceivedInformation();
        }

        private async Task HandleReceivedInformation()
        {
            EventData eventData = await eventHubReceiver.ReceiveAsync();
            string data = Encoding.UTF8.GetString(eventData.GetBytes());

            JObject serializedObject = JObject.Parse(data);
            string lightNumber = serializedObject["lightNumber"].ToString();
            string lightStatus = serializedObject["lightStatus"].ToString();
            DateTime lastMove = Convert.ToDateTime(serializedObject["date"]);

            HandleLights(lightNumber, lightStatus);
        }

        private void HandleLights(string light, string status)
        {
            string finalCommand = string.Format("{0},{1}", light, status);

            puertoSerial.Open();
            puertoSerial.Write(finalCommand);
            puertoSerial.Close();
        }
    }
}
