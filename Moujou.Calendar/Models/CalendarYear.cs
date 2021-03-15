using Moujou.Calendar.Supports;
using System;
using System.Collections.ObjectModel;

namespace Moujou.Calendar.Models
{
    public class CalendarYear : NPCBase
    {
        public ObservableCollection<CalendarMonth> Months;

        private int _numOfYear;
        public int NumOfYear
        {
            get => _numOfYear;
            set
            {
                _numOfYear = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public CalendarYear(DateTime dateTime)
        {
            SelectedDate = dateTime;
            Months = new ObservableCollection<CalendarMonth>();
            for (int month = 1; month <= 12; month++)
            {
                CalendarMonth calendarMonth = new CalendarMonth(month);
                Months.Add(calendarMonth);
            }

            NumOfYear = dateTime.Year;
        }

        public void AssignmentDays()
        {
            foreach (CalendarMonth month in Months)
            {
                if (NumOfYear == SelectedDate.Year && month.NumOfMonth == SelectedDate.Month) month.NumOfSelectedDay = SelectedDate.Day;
                else month.NumOfSelectedDay = null;
                month.InitializationDays(NumOfYear);
            }
        }
    }
}
