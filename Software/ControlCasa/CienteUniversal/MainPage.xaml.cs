using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CienteUniversal
{
    public sealed partial class MainPage : Page
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "secondhubcasa.azure-devices.net";
        static string deviceKey = "Z0a9xNB1qPd1AXKyJHmvRVa3JQ51/au7wf/4b9yzD5A=";
        static string deviceId = "maquinaCasa";

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
            InitializeUi();
        }

        private async Task InitializeUi()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
            }
        }

        private void btnArriba_Click(object sender, RoutedEventArgs e)
        {
            displayUp.Begin();
        }

        private void btnAbajo_Click(object sender, RoutedEventArgs e)
        {
            displayDown.Begin();
        }

        private void btnAlarma_Click(object sender, RoutedEventArgs e)
        {
            HandleAlarm();
        }

        private void btnPolli_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("11", ref luzPolliPrendida);
            hideDown.Begin();
        }

        private void btnOscar_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("04", ref luzOscarPrendida);
            hideUp.Begin();
        }

        private void btnTaller_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("10", ref luzTallerPrendida);
            hideDown.Begin();
        }

        private void btnBanoAbajoInt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("01", ref luzBanoAbjIntPrendida);
            hideDown.Begin();
        }

        private void btnBanoAbajoExt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("06", ref luzBanoAbjExtPrendida);
            hideDown.Begin();
        }

        private void btnCocina_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("08", ref luzCocinaPrendida);
            hideUp.Begin();
        }

        private void btnSala_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("03", ref luzSalaPrendida);
            hideDown.Begin();
        }

        private void btnPatio_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("12", ref luzPatioPrendida);
            hideDown.Begin();
        }

        private void btnBanoArribaInt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("02", ref luzBanoArrIntPrendida);
            hideUp.Begin();
        }

        private void btnBanoArribaExt_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("05", ref luzBanoArrExtPrendida);
            hideUp.Begin();
        }

        private void btnCuartoCosas_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("07", ref luzCosasPrendida);
            hideUp.Begin();
        }

        private void btnVentilador_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("09", ref luzVentiladorPrendida);
            hideUp.Begin();
        }

        private void HandleLightStatus(string light, ref bool handler)
        {
            if (handler)
            {
                SendDataToHub(light, "0");
            }
            else
            {
                SendDataToHub(light, "1");
            }

            handler = !handler;
        }

        private void HandleAlarm()
        {
            SendDataToHub("13", "0");
        }

        private async void SendDataToHub(string light, string handler)
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
