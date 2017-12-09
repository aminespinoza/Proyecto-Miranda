using Android.App;
using Android.OS;
using Android.Widget;
using ClienteAndroid.Helpers;
using System;

namespace ClienteAndroid
{
    [Activity(Label = "AbajoActivity")]
    public class AbajoActivity : Activity
    {
        Button juegosButton = null, tallerButton = null, banoAbajoIntButton = null, banoAbajoExtButton = null, salaButton = null, patioButton = null;

        bool luzJuegosPrendida = false, luzBanoAbjIntPrendida = false, luzBanoAbjExtPrendida = false, luzSalaPrendida = false, luzTallerPrendida = false, luzPatioPrendida = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Abajo);

            juegosButton = FindViewById<Button>(Resource.Id.btnJuegos);
            tallerButton = FindViewById<Button>(Resource.Id.btnTaller);
            banoAbajoIntButton = FindViewById<Button>(Resource.Id.btnBanoAbajoInt);
            banoAbajoExtButton = FindViewById<Button>(Resource.Id.btnBanoAbajoExt);
            salaButton = FindViewById<Button>(Resource.Id.btnBar);
            patioButton = FindViewById<Button>(Resource.Id.btnPatio);
        }

        protected override void OnResume()
        {
            base.OnResume();
            juegosButton.Click += JuegosButton_Click;
            tallerButton.Click += TallerButton_Click;
            banoAbajoIntButton.Click += BanoAbajoIntButton_Click;
            banoAbajoExtButton.Click += BanoAbajoExtButton_Click;
            salaButton.Click += SalaButton_Click;
            patioButton.Click += PatioButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            juegosButton.Click -= JuegosButton_Click;
            tallerButton.Click -= TallerButton_Click;
            banoAbajoIntButton.Click -= BanoAbajoIntButton_Click;
            banoAbajoExtButton.Click -= BanoAbajoExtButton_Click;
            salaButton.Click -= SalaButton_Click;
            patioButton.Click -= PatioButton_Click;

        }

        private void JuegosButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("11", ref luzJuegosPrendida);
            SendToastNotification(sender, luzJuegosPrendida);
        }

        private void TallerButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("10", ref luzTallerPrendida);
            SendToastNotification(sender, luzTallerPrendida);
        }

        private void BanoAbajoIntButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("01", ref luzBanoAbjIntPrendida);
            SendToastNotification(sender, luzBanoAbjIntPrendida);
        }

        private void BanoAbajoExtButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("06", ref luzBanoAbjExtPrendida);
            SendToastNotification(sender, luzBanoAbjExtPrendida);
        }

        private void SalaButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("03", ref luzSalaPrendida);
            SendToastNotification(sender, luzSalaPrendida);
        }

        private void PatioButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("12", ref luzPatioPrendida);
            SendToastNotification(sender, luzPatioPrendida);
        }

        private void HandleLightStatus(string light, ref bool handler)
        {
            if (handler)
                FunctionHelper.SendDataToFunction(light, "0");
            else
                FunctionHelper.SendDataToFunction(light, "1");

            handler = !handler;
        }

        private void SendToastNotification(object lightIdentifier, bool lightStatus)
        {
            string buttonName = (lightIdentifier as Button).Text;
            string lightStringStatus = lightStatus ? lightStringStatus = "encendida" : lightStringStatus = "apagada";
            string finalMessage = string.Format("La luz {0}, está en modo {1}", buttonName, lightStringStatus);
            Toast.MakeText(this.ApplicationContext, finalMessage, ToastLength.Short).Show();
        }
    }
}