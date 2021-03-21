using Moujou.Calendar.Supports;

namespace Moujou.Calendar.Models
{
    public class CalendarDay : NPCBase
    {
        private CalendarMonth _month;
        public CalendarMonth Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged();
            }
        }

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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _hasEvent;
        public bool HasEvent
        {
            get => _hasEvent;
            set
            {
                _hasEvent = value;
                OnPropertyChanged();
            }
        }
    }
}
