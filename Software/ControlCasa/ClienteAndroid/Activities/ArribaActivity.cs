using Android.App;
using Android.OS;
using Android.Widget;
using ClienteAndroid.Helpers;

namespace ClienteAndroid
{
    [Activity(Label = "ArribaActivity")]
    public class ArribaActivity : Activity
    {
        Button oscarButton = null;
        Button banoArribaIntButton = null;
        Button banoArribaExtButton = null;
        Button gimnasioButton = null;
        Button ventiladorButton = null;
        Button cocinaButton = null;

        bool luzOscarPrendida = false;
        bool luzBanoArrIntPrendida = false;
        bool luzBanoArrExtPrendida = false; 
        bool luzGimnasioPrendida = false;
        bool luzCocinaPrendida = false;
        bool luzVentiladorPrendida = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Arriba);

            oscarButton = FindViewById<Button>(Resource.Id.btnOscar);
            banoArribaIntButton = FindViewById<Button>(Resource.Id.btnBanoArribaInt);
            banoArribaExtButton = FindViewById<Button>(Resource.Id.btnBanoArribaExt);
            gimnasioButton = FindViewById<Button>(Resource.Id.btnGimnasio);
            ventiladorButton = FindViewById<Button>(Resource.Id.btnVentilador);
            cocinaButton = FindViewById<Button>(Resource.Id.btnCocina);
        }

        protected override void OnResume()
        {
            base.OnResume();
            oscarButton.Click += OscarButton_Click;
            banoArribaIntButton.Click += BanoArribaIntButton_Click;
            banoArribaExtButton.Click += BanoArribaExtButton_Click;
            gimnasioButton.Click += GimnasioButton_Click;
            ventiladorButton.Click += VentiladorButton_Click;
            cocinaButton.Click += CocinaButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            oscarButton.Click -= OscarButton_Click;
            banoArribaIntButton.Click -= BanoArribaIntButton_Click;
            banoArribaExtButton.Click -= BanoArribaExtButton_Click;
            gimnasioButton.Click -= GimnasioButton_Click;
            ventiladorButton.Click -= VentiladorButton_Click;
            cocinaButton.Click -= CocinaButton_Click;
        }

        private void OscarButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("04", ref luzOscarPrendida);
            SendToastNotification(sender, luzOscarPrendida);
        }

        private void BanoArribaIntButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("02", ref luzBanoArrIntPrendida);
            SendToastNotification(sender, luzBanoArrIntPrendida);
        }

        private void BanoArribaExtButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("05", ref luzBanoArrExtPrendida);
            SendToastNotification(sender, luzBanoArrExtPrendida);
        }

        private void GimnasioButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("07", ref luzGimnasioPrendida);
            SendToastNotification(sender, luzGimnasioPrendida);
        }

        private void VentiladorButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("09", ref luzVentiladorPrendida);
            SendToastNotification(sender, luzVentiladorPrendida);
        }

        private void CocinaButton_Click(object sender, System.EventArgs e)
        {
            HandleLightStatus("08", ref luzCocinaPrendida);
            SendToastNotification(sender, luzCocinaPrendida);
        }

        private void HandleLightStatus(string light, ref bool handler)
        {
            if (handler)
                FunctionHelper.SendDataToFunction(light, "0");
            else
                FunctionHelper.SendDataToFunction(light, "1");

            handler = !handler;
        }

        
    }
}