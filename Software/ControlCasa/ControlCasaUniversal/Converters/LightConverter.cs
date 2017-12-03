using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ControlCasaUniversal.Converters
{
    public class LightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isDecided = (bool)value;

            if (isDecided)
            {
                return new SolidColorBrush(Color.FromArgb(255, 15, 106, 158));
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(51, 15, 106, 158));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
