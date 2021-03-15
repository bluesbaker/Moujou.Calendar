using Moujou.Calendar.Supports;
using System;
using System.Globalization;

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

        private int? _numOfSelectedDay;
        public int? NumOfSelectedDay
        {
            get => _numOfSelectedDay;
            set
            {
                _numOfSelectedDay = value;
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

        public CalendarMonth(int month)
        {
            Days = new CalendarDay[42];
            for (int dayIndex = 0; dayIndex < Days.Length; dayIndex++)
            {
                Days[dayIndex] = new CalendarDay();
            }

            NumOfMonth = month;
        }

        public void InitializationDays(int year)
        {
            DayOfWeek firstDayOfWeek = new DateTime(year, _numOfMonth, 1).DayOfWeek;
            int dayCount = DateTime.DaysInMonth(year, _numOfMonth);
            int currentIndex = 0;

            // "Before" incorrect days
            for (; currentIndex < (firstDayOfWeek == 0 ? 7 : (int)firstDayOfWeek) - 1; currentIndex++)
                Days[currentIndex].NumOfDay = 0;
            // Correct days
            for (int day = 1; day <= dayCount; day++, currentIndex++)
            {
                Days[currentIndex].NumOfDay = day;
                if (NumOfSelectedDay == day) Days[currentIndex].IsSelected = true;
                else Days[currentIndex].IsSelected = false;
            }
            // "After" incorrect days
            for (; currentIndex < Days.Length; currentIndex++)
                Days[currentIndex].NumOfDay = 0;
        }
    }
}
