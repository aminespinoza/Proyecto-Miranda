using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace ClienteAndroid
{
    [Activity(Label = "ClienteAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button arribaButton = null;
        Button abajoButton = null;
        Button alertaButton = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            arribaButton = FindViewById<Button>(Resource.Id.btnArriba);
            abajoButton = FindViewById<Button>(Resource.Id.btnAbajo);
            alertaButton = FindViewById<Button>(Resource.Id.btnAlerta);
        }

        protected override void OnResume()
        {
            base.OnResume();
            arribaButton.Click += ArribaButton_Click;
            abajoButton.Click += AbajoButton_Click;
            alertaButton.Click += AlertaButton_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();

            arribaButton.Click -= ArribaButton_Click;
            abajoButton.Click -= AbajoButton_Click;
            alertaButton.Click -= AlertaButton_Click;
        }

        private void AbajoButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(AbajoActivity));
            StartActivity(intent);
        }

        private void ArribaButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(ArribaActivity));
            StartActivity(intent);
        }

        private void AlertaButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}

