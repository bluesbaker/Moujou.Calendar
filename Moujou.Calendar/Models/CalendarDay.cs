using Moujou.Calendar.Supports;

namespace Moujou.Calendar.Models
{
    public class CalendarDay : NPCBase
    {
        private int _numOfDay;
        public int NumOfDay
        {
            get => _numOfDay;
            set
            {
                _numOfDay = value;
                OnPropertyChanged();
            }
        }
    }
}
