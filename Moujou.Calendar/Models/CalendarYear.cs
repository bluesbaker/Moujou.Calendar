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
                ChangeSelectedDay();
            }
        }

        private CalendarDay _selectedDay;
        public CalendarDay SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                OnPropertyChanged();
            }
        }

        public CalendarYear(DateTime dateTime)
        {
            Months = new ObservableCollection<CalendarMonth>();
            for (int month = 1; month <= 12; month++)
            {
                CalendarMonth calendarMonth = new CalendarMonth(month)
                {
                    Year = this
                };
                Months.Add(calendarMonth);
            }
            SelectedDate = dateTime;
            NumOfYear = dateTime.Year;
        }

        public void AssignmentDays()
        {
            foreach (CalendarMonth month in Months)
            {
                month.InitializationDays();
            }
            ChangeSelectedDay();
        }

        private void ChangeSelectedDay()
        {
            foreach(var month in Months)
            {
                foreach(var day in month.Days)
                {
                    if (SelectedDate == new DateTime(this.NumOfYear, month.NumOfMonth, day.NumOfDay))
                        day.IsSelected = true;
                    else
                        day.IsSelected = false;
                }
            }
        }
    }
}
