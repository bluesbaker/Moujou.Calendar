using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Moujou.Calendar
{
    public class CalendarMonth : INotifyPropertyChanged
    {
        public CalendarDay[] _days;
        public CalendarDay[] Days
        {
            get => _days;
            set
            {
                _days = value;
                OnPropertyChanged("Days");
            }
        }

        private readonly int _numOfMonth = 0;

        public string Name
        {
            get
            {
                string MonthName = DateTimeFormatInfo.CurrentInfo.MonthNames[(int)_numOfMonth - 1];
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(MonthName);
            }
        }

        public CalendarMonth(int num)
        {
            Days = new CalendarDay[42];
            _numOfMonth = num;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
