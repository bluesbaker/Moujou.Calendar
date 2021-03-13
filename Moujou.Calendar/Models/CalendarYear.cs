using Moujou.Calendar.Supports;
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
                AssignmentDays();
                OnPropertyChanged();
            }
        }

        public CalendarYear(int year)
        {
            Months = new ObservableCollection<CalendarMonth>();
            for (int month = 1; month <= 12; month++)
            {
                CalendarMonth calendarMonth = new CalendarMonth(month);
                Months.Add(calendarMonth);
            }

            NumOfYear = year;
        }

        public void AssignmentDays()
        {
            foreach (CalendarMonth month in Months)
            {
                month.InitializationDays(NumOfYear);
            }
        }
    }
}
