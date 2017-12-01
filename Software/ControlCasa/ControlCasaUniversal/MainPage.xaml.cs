using Microsoft.Azure.Devices.Client;
using System.Diagnostics;
using System.Text;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;

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
            this.InitializeComponent();
            
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
                Debug.WriteLine(Encoding.ASCII.GetString(receivedMessage.GetBytes()));

                await deviceClient.CompleteAsync(receivedMessage);
                bridgeDevice.Write(receivedMessage.GetBytes());
            }
        }
    }
}
