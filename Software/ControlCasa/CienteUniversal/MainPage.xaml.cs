using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CienteUniversal
{
    public sealed partial class MainPage : Page
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "nombredetuhub.azure-devices.net";
        static string deviceKey = "deviceKey";
        static string deviceId = "deviceName";

        bool luzPolliPrendida = false;
        bool luzOscarPrendida = false;
        bool luzBanoAbjIntPrendida = false;
        bool luzBanoAbjExtPrendida = false;
        bool luzBanoArrIntPrendida = false;
        bool luzBanoArrExtPrendida = false;
        bool luzSalaPrendida = false;
        bool luzCosasPrendida = false;
        bool luzCocinaPrendida = false;
        bool luzVentiladorPrendida = false;
        bool luzTallerPrendida = false;
        bool luzPatioPrendida = false;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            deviceClient = DeviceClient.Create(iotHubUri, AuthenticationMethodFactory.CreateAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey), TransportType.Http1);

        }

        private void btnPolli_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("11", ref luzPolliPrendida);
        }

        private void btnOscar_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("04", ref luzOscarPrendida);
        }

        private void btnTaller_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("10", ref luzTallerPrendida);
        }

        private void btnBanoAbajoInt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("01", ref luzBanoAbjIntPrendida);
        }

        private void btnBanoAbajoExt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("06", ref luzBanoAbjExtPrendida);
        }

        private void btnCocina_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("08", ref luzCocinaPrendida);
        }

        private void btnSala_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("03", ref luzSalaPrendida);
        }

        private void btnPatio_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("12", ref luzPatioPrendida);
        }

        private void btnBanoArribaInt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("02", ref luzBanoArrIntPrendida);
        }

        private void btnBanoArribaExt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("05", ref luzBanoArrExtPrendida);
        }

        private void btnCuartoCosas_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("07", ref luzCosasPrendida);
        }

        private void btnVentilador_Click(object sender, RoutedEventArgs e)
        {
               HandleLightStatus("09", ref luzVentiladorPrendida);
        }

        private void HandleLightStatus(string light, ref bool handler)
        {
            if (handler)
            {
                EnviarInformaciónAlHub(light, "0");
            }
            else
            {
                EnviarInformaciónAlHub(light, "1");
            }

            handler = !handler;
        }

        private async void EnviarInformaciónAlHub(string light, string handler)
        {
            var telemetryDataPoint = new
            {
                lightNumber = light,
                lightStatus = handler,
                date = DateTime.Now
            };

            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            Debug.WriteLine(messageString);
            await deviceClient.SendEventAsync(message);
        }
    }
}
