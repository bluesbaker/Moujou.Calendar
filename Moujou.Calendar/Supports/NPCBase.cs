using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Moujou.Calendar.Supports
{
    public class NPCBase : INotifyPropertyChanged
    {
        // Implementation INotifyPropwertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
