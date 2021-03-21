using Moujou.Calendar.Supports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moujou.Calendar.Models
{
    public class CalendarDate : NPCBase
    {
        private CalendarYear _year;
        public CalendarYear Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }
    }
}
