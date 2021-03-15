using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Moujou.Calendar.ContentViews
{
    public class DayFrame : Frame
    {
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected", typeof(bool), typeof(DayFrame), false);
        public static readonly BindableProperty HasEventProperty =
            BindableProperty.Create("HasEvent", typeof(bool), typeof(DayFrame), false);

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        public bool HasEvent
        {
            get => (bool)GetValue(HasEventProperty);
            set => SetValue(HasEventProperty, value);
        }
    }
}
