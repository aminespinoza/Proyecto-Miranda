using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ControlCasa.Controls
{
    public partial class IndexMenu : UserControl
    {
        public MainWindow master { get; set; }
        bool isMenuVisible = false;
        public IndexMenu()
        {
            InitializeComponent();
        }

        private void btnIndex_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isMenuVisible)
            {
                TriggerAnimation("showMainContainer");
            }
            else
            {
                TriggerAnimation("hideMainContainer");
            }

            isMenuVisible = !isMenuVisible;
        }

        private void btnSecurity_Click(object sender, RoutedEventArgs e)
        {
            TriggerAnimation("showSecurityMenu");
        }

        private void btnDedsec_Click(object sender, RoutedEventArgs e)
        {
            TriggerAnimation("hideSecurityMenu");
            //master.ctrlDedsec.Visibility = Visibility.Visible;
        }

        private void btnTerminal_Click(object sender, RoutedEventArgs e)
        {
            TriggerAnimation("hideSecurityMenu");
            //master.ctrlTerminal.Visibility = Visibility.Visible;
        }

        private void btnAlarma_Click(object sender, RoutedEventArgs e)
        {
            TriggerAnimation("hideSecurityMenu");
            master.ctrlAlarm.Visibility = Visibility.Visible;
        }

        private void TriggerAnimation(string animationName)
        {
            Storyboard standardAnimation = (Storyboard)FindResource(animationName);
            standardAnimation.Begin();
        }

        
    }
}
