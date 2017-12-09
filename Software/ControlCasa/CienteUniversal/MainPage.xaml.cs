using System;
using System.Net.Http;
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
        bool luzJuegosPrendida = false;
        bool luzOscarPrendida = false;
        bool luzBanoAbjIntPrendida = false;
        bool luzBanoAbjExtPrendida = false;
        bool luzBanoArrIntPrendida = false;
        bool luzBanoArrExtPrendida = false;
        bool luzSalaPrendida = false;
        bool luzGimnasioPrendida = false;
        bool luzCocinaPrendida = false;
        bool luzVentiladorPrendida = false;
        bool luzTallerPrendida = false;
        bool luzPatioPrendida = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await InitializeUi();
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

        private void btnJuegos_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("11", ref luzJuegosPrendida);
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

        private void btnGimnasio_Click(object sender, RoutedEventArgs e)
        {
            HandleLightStatus("07", ref luzGimnasioPrendida);
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
                SendDataToCloud(light, "0");
            else
                SendDataToCloud(light, "1");

            handler = !handler;
        }

        private void HandleAlarm()
        {
            SendDataToCloud("13", "0");
        }

        private async void SendDataToCloud(string light, string handler)
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
