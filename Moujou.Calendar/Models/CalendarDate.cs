using Moujou.Calendar.Supports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moujou.Calendar.Models
{
    public class CalendarDate : NPCBase
    {
        private CalendarYear _calendarYear;
        public CalendarYear CalendarYear
        {
            get => _calendarYear;
            set
            {
                _calendarYear = value;
                OnPropertyChanged();
            }
        }
    }
}
