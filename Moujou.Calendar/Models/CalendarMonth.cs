using Moujou.Calendar.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Moujou.Calendar.Models
{
    public class CalendarMonth : NPCBase
    {
        private CalendarDay[] _days;
        public CalendarDay[] Days
        {
            get => _days;
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }

        private int _numOfMonth;
        public int NumOfMonth
        {
            get => _numOfMonth;
            set
            {
                _numOfMonth = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                string MonthName = DateTimeFormatInfo.CurrentInfo.MonthNames[(int)NumOfMonth - 1];
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(MonthName);
            }
        }

        public CalendarMonth(int num)
        {
            Days = new CalendarDay[42];
            NumOfMonth = num;
        }

        public void InizializationDays(int year)
        {
            CalendarDay[] days = new CalendarDay[42];
            DayOfWeek dayOfWeek = new DateTime(year, _numOfMonth, 1).DayOfWeek;
            int currentIndex = 0;

            // "Before" incorrect days
            for (; currentIndex < (dayOfWeek == 0 ? 7 : (int)dayOfWeek) - 1; currentIndex++)
                days[currentIndex] = new CalendarDay() { NumOfDay = 0 };
            // Correct days
            for (int day = 1; day <= DateTime.DaysInMonth(year, _numOfMonth); day++, currentIndex++)
            {
                days[currentIndex] = new CalendarDay()
                {
                    NumOfDay = day
                };
            }
            // "After" incorrect days
            for (; currentIndex < Days.Length; currentIndex++)
                days[currentIndex] = new CalendarDay() { NumOfDay = 0 };

            Days = days;
        }
    }
}
