﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Moujou.Calendar.Supports
{
    public class NPCBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
