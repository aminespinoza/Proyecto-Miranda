using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ControlCasaUniversal
{
    public sealed partial class MainPage : Page
    {
        static DeviceClient deviceClient;
        private I2cDevice bridgeDevice;
        static string iotHubUri = "secondhubcasa.azure-devices.net";
        static string deviceKey = "aBfxq985YI8L880Z/sTDrckgu2PTpflmC6y9nltl280=";
        static string deviceId = "testingDevice";
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            deviceClient = DeviceClient.Create(iotHubUri, AuthenticationMethodFactory.CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey), TransportType.Http1);
            IniciateMonitoring();
            ReceiveMessage();
        }

        private async void IniciateMonitoring()
        {
            var settings = new I2cConnectionSettings(0x40);
            settings.BusSpeed = I2cBusSpeed.StandardMode;
            string aqs = I2cDevice.GetDeviceSelector();
            IReadOnlyList<DeviceInformation> devices = await DeviceInformation.FindAllAsync(aqs);
            bridgeDevice = await I2cDevice.FromIdAsync(devices[0].Id, settings);

        }

        private async void ReceiveMessage()
        {
            while (true)
            {
                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                HandleButtons(Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                await deviceClient.CompleteAsync(receivedMessage);
                bridgeDevice.Write(receivedMessage.GetBytes());
            }
        }

        private void HandleButtons(string receivedMessage)
        {
            string[] lightArray = receivedMessage.Split(',');
            string lightNumber = lightArray[0];
            string lightStatus = lightArray[1];

            bool decision = Convert.ToBoolean(Convert.ToInt32(lightStatus)); ;

            switch (lightNumber)
            {
                case "01":
                    ctrlBanoAbjInt.IsLightOn = decision;
                    break;
                case "02":
                    ctrlBanoArrInt.IsLightOn = decision;
                    break;
                case "03":
                    ctrlSala.IsLightOn = decision;
                    break;
                case "04":
                    ctrlOscar.IsLightOn = decision;
                    break;
                case "05":
                    ctrlBanoArrExt.IsLightOn = decision;
                    break;
                case "06":
                    ctrlBanoAbjExt.IsLightOn = decision;
                    break;
                case "07":
                    ctrlGimnasio.IsLightOn = decision;
                    break;
                case "08":
                    ctrlBanoAbjInt.IsLightOn = decision;
                    break;
                case "09":
                    ctrlVentilador.IsLightOn = decision;
                    break;
                case "10":
                    ctrlTaller.IsLightOn = decision;
                    break;
                case "11":
                    ctrlJuegos.IsLightOn = decision;
                    break;
                case "12":
                    ctrlPatio.IsLightOn = decision;
                    break;
            }
        }
    }
}
