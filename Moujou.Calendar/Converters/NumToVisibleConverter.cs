using System;
using System.Globalization;
using Xamarin.Forms;

namespace Moujou.Calendar.Converters
{
    public class NumToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value != 0 && value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
