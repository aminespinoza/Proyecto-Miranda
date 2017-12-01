using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ControlCasaUniversal.CustomControls
{
    public sealed partial class Light : UserControl
    {
        private string lightName;

        public string LightName
        {
            get { return lightName; }
            set { lightName = value; txtLight.Text = lightName; }
        }

        //public bool IsOn
        //{
        //    get { return isOn; }
        //    set {
        //        isOn = value;
        //        if (isOn)
        //        {
        //            txtLight.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        //            borBackground.Background = new SolidColorBrush(Color.FromArgb(255, 14, 14, 14));
        //        }
        //        else
        //        { https://docs.microsoft.com/en-us/adaptive-cards/get-started/bots#channel-status

        //        }
        //    }
        //}



        private bool isLightOn;

        public bool IsLightOn
        {
            get { return isLightOn; }
            set { isLightOn = value; NotifyPropertyChanged("IsLightOn"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        public Light()
        {
            this.InitializeComponent();
        }
    }
}
