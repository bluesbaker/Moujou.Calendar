using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Moujou.Calendar.Models
{
    public class CalendarDay : INotifyPropertyChanged
    {
        private int _numOfDay;
        public int NumOfDay
        {
            get => _numOfDay;
            set
            {
                _numOfDay = value;
                OnPropertyChanged("NumOfDay");
            }
        }

        // Implementation INotifyPropwertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
