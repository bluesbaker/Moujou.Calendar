using Moujou.Calendar.Supports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Moujou.Calendar.Models
{
    public class CalendarYear : NPCBase
    {
        public ObservableCollection<CalendarMonth> Months = new ObservableCollection<CalendarMonth>();

        private int _numOfYear;
        public int NumOfYear
        {
            get => _numOfYear;
            set
            {
                _numOfYear = value;
                OnPropertyChanged("NumOfYear");
            }
        }

        public CalendarYear(int year)
        {
            NumOfYear = year;
        }

        public void InitializationMonths()
        {
            Months.Clear();
            for (int month = 1; month <= 12; month++)
            {
                CalendarMonth calendarMonth = new CalendarMonth(month);
                calendarMonth.InizializationDays(NumOfYear);
                Months.Add(calendarMonth);
            }
        }
    }
}
