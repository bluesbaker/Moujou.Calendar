using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Moujou.Calendar.Converters
{
    public class MonthNumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string MonthName = DateTimeFormatInfo.CurrentInfo.MonthNames[(int)value - 1];
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(MonthName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
