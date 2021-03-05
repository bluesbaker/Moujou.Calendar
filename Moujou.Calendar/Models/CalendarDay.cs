using Moujou.Calendar.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
                OnPropertyChanged("NumOfDay");
            }
        }
    }
}
