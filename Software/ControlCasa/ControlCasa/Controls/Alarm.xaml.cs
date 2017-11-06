using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using System;
using System.Windows.Threading;

namespace ControlCasa.Controls
{
    public partial class Alarm : UserControl
    {
        DispatcherTimer timer;
        public MainWindow master { get; set; }

        public Alarm()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += CalculateAlarm;
        }

        private void CalculateAlarm(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            int hour = Convert.ToInt16(((ComboBoxItem)cmbTimeHour.SelectedValue).Content);
            int minutes = Convert.ToInt16(((ComboBoxItem)cmbTimeMin.SelectedValue).Content);
            DateTime selectedTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hour, minutes, currentTime.Second);

            if (currentTime > selectedTime)
            {
                timer.Stop();
                master.HandleLights(((ComboBoxItem)cmbSelectedLight.SelectedValue).Tag.ToString(), ((ComboBoxItem)cmbStatus.SelectedValue).Tag.ToString());
            }
        }

        public void TriggerAlarm()
        {
            Storyboard standardAnimation = (Storyboard)FindResource("showAlarm");
            standardAnimation.Begin();
        }

        private void rectClose_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnGuardarAlarma_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string desiredAlarmTime = ((ComboBoxItem)cmbTimeHour.SelectedValue).Content.ToString() + ":" + ((ComboBoxItem)cmbTimeMin.SelectedValue).Content.ToString();
            string selectedLight =  ((ComboBoxItem)cmbSelectedLight.SelectedValue).Content.ToString() + "(" + ((ComboBoxItem)cmbSelectedLight.SelectedValue).Tag.ToString() + ")";
            string selectedStatus = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();

            timer.Start();

            lstLog.Items.Add(String.Format("Alarma lista para: {0}, luz seleccionada: {1}, será {2}", desiredAlarmTime, selectedLight, selectedStatus));
        }
    }
}
