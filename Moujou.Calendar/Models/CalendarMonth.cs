using Moujou.Calendar.Supports;
using System;
using System.Globalization;

namespace Moujou.Calendar.Models
{
    public class CalendarMonth : NPCBase
    {
        private CalendarYear _yearParent;
        public CalendarYear YearParent
        {
            get => _yearParent;
            set
            {
                _yearParent = value;
                OnPropertyChanged();
            }
        }

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

        public CalendarMonth(int month)
        {
            NumOfMonth = month;
            Days = new CalendarDay[42];
            for (int dayIndex = 0; dayIndex < Days.Length; dayIndex++)
            {
                Days[dayIndex] = new CalendarDay
                {
                    MonthParent = this
                };
            }
        }

        public void InitializationDays()
        {
            DayOfWeek firstDayOfWeek = new DateTime(YearParent.NumOfYear, NumOfMonth, 1).DayOfWeek;
            int dayCount = DateTime.DaysInMonth(YearParent.NumOfYear, NumOfMonth);
            int currentIndex = 0;

            // "Before" incorrect days
            for (; currentIndex < (firstDayOfWeek == 0 ? 7 : (int)firstDayOfWeek) - 1; currentIndex++)
                Days[currentIndex].NumOfDay = 0;
            // Correct days
            for (int day = 1; day <= dayCount; day++, currentIndex++)
            {
                Days[currentIndex].NumOfDay = day;
            }
            // "After" incorrect days
            for (; currentIndex < Days.Length; currentIndex++)
                Days[currentIndex].NumOfDay = 0;
        }
    }
}
