using Moujou.Calendar.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Moujou.Calendar.Models
{
    public class CalendarYear : NPCBase
    {
        private CalendarMonth[] _months;
        public CalendarMonth[] Months
        {
            get => _months;
            set
            {
                _months = value;
                OnPropertyChanged("Months");
            }
        }

        private int _numOfYear;
        public int NumOfYear
        {
            get => _numOfYear;
            set
            {
                _numOfYear = value;
                OnPropertyChanged("NumOfYear");
            }
        }
    }
}
