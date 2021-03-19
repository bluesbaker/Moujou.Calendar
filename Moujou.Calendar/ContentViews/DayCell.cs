using Moujou.Calendar.Converters;
using Moujou.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Moujou.Calendar.ContentViews
{
    public class DayCell : Frame
    {
        #region BindableProperties
        public static readonly BindableProperty DayProperty =
            BindableProperty.Create("Day", typeof(CalendarDay), typeof(DayCell), null);
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected", typeof(bool), typeof(DayCell), false);
        public static readonly BindableProperty HasEventProperty =
            BindableProperty.Create("HasEvent", typeof(bool), typeof(DayCell), false);

        public CalendarDay Day
        {
            get => (CalendarDay)GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }
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
        #endregion

        protected override void OnParentSet()
        {
            base.OnParentSet();
            // Cell label
            Label cellLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                // fix text aligment for Xamarin 2.3.4...
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            cellLabel.SetBinding(Label.TextProperty, new Binding("Day.NumOfDay", source: this));

            #region Triggers
            // "is selected" trigger
            Trigger selectedTrigger = new Trigger(typeof(DayCell))
            {
                Property = DayCell.IsSelectedProperty,
                Value = true
            };
            selectedTrigger.Setters.Add(new Setter
            {
                Property = DayCell.BackgroundColorProperty,
                Value = Color.Green
            });
            // "has event" trigger
            Trigger hasEventTrigger = new Trigger(typeof(DayCell))
            {
                Property = DayCell.HasEventProperty,
                Value = true
            };
            hasEventTrigger.Setters.Add(new Setter
            {
                Property = DayCell.BackgroundColorProperty,
                Value = Color.Purple
            });
            #endregion

            this.Content = cellLabel;
            this.Triggers.Add(selectedTrigger);
            this.Triggers.Add(hasEventTrigger);
            this.SetBinding(DayCell.HeightRequestProperty, new Binding(nameof(Width), source: this));
            this.SetBinding(DayCell.IsVisibleProperty, new Binding("Day.NumOfDay", source: this, converter: new NumToVisibleConverter()));
            this.SetBinding(DayCell.IsSelectedProperty, new Binding("Day.IsSelected", source: this));
            this.SetBinding(DayCell.HasEventProperty, new Binding("Day.HasEvent", source: this));
            this.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(p =>
                {
                    Day.MonthParent.YearParent.SelectedDate = 
                        new DateTime(Day.MonthParent.YearParent.NumOfYear, Day.MonthParent.NumOfMonth, Day.NumOfDay);
                })
            });
        }
    }
}
