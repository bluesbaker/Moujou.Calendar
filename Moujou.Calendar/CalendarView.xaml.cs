﻿using Moujou.Calendar.Converters;
using Moujou.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Moujou.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarView : ContentView
    {
        #region Date BindableProperties
        public static readonly BindableProperty DayProperty =
            BindableProperty.Create("Day", typeof(int), typeof(CalendarView), DateTime.Now.Day, BindingMode.TwoWay);
        public static readonly BindableProperty MonthProperty =
            BindableProperty.Create("Month", typeof(int), typeof(CalendarView), DateTime.Now.Month, BindingMode.TwoWay);
        public static readonly BindableProperty YearProperty =
            BindableProperty.Create("Year", typeof(int), typeof(CalendarView), DateTime.Now.Year, BindingMode.TwoWay);
        public static readonly BindableProperty SelectedDateProperty =
            BindableProperty.Create("SelectedDate", typeof(DateTime), typeof(CalendarView), DateTime.Now);

        public int Day
        {
            get => (int)GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }
        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }
        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }
        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }
        #endregion

        private CalendarYear _currentYear;
        public CalendarYear CurrentYear
        {
            get => _currentYear;
            set
            {
                _currentYear = value;
                OnPropertyChanged();
            }
        }

        public CalendarView()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet(); 
            GenerateWeekDays();
            calendarLayout.BindingContext = this;
            // Data initialization
            CurrentYear = new CalendarYear(Year);
            cellCarousel.ItemTemplate = CreateCalendarDataTemplate();
            cellCarousel.ItemsSource = CurrentYear.Months;
            cellCarousel.CurrentItem = CurrentYear.Months.ElementAtOrDefault(Month - 1);
        }

        private DataTemplate CreateCalendarDataTemplate()
        {
            return new DataTemplate(() =>
            {
                // Generate cells of Grid
                Grid cellGrid = new Grid
                {
                    ColumnSpacing = 2,
                    RowSpacing = 2
                };
                for (int row = 0; row < 6; row++)
                    cellGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int column = 0; column < 7; column++)
                    cellGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                
                // Filling the cellsGrid with cellButtons
                int cellCount = 0;
                for (int row = 0; row < cellGrid.RowDefinitions.Count; row++)
                {
                    for (int column = 0; column < cellGrid.ColumnDefinitions.Count; column++)
                    {
                        // Cell label
                        Label cellLabel = new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            // fix text aligment for Xamarin 2.3.4...
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        };
                        cellLabel.SetBinding(Label.TextProperty, $"Days[{cellCount}].NumOfDay");

                        // Cell frame
                        Frame cellFrame = new Frame
                        {
                            Padding = 0,
                            HasShadow = false,
                            Content = cellLabel
                        };
                        cellFrame.SetValue(Grid.RowProperty, row);
                        cellFrame.SetValue(Grid.ColumnProperty, column);
                        cellFrame.SetBinding(Frame.IsVisibleProperty, $"Days[{cellCount}].NumOfDay", BindingMode.Default, new NumToVisibleConverter());
                        cellFrame.SetBinding(Frame.HeightRequestProperty, new Binding(nameof(Width), source: cellFrame));

                        cellGrid.Children.Add(cellFrame);
                        cellCount++;
                    }
                }
                return cellGrid;
            });
        }

        private void GenerateWeekDays()
        {
            // Add the days of week
            for (int i = 0; i < 7; i++)
            {
                int indexDay = i + 1;
                Label weekLabel = new Label()
                {
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(indexDay == 7 ? DayOfWeek.Sunday : (DayOfWeek)indexDay),
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };
                weekLabel.SetValue(Grid.ColumnProperty, i);
                weekGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                weekGrid.Children.Add(weekLabel);
            }
            weekGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }

        private void PreviousYear_Clicked(object sender, EventArgs e)
        {
            CurrentYear.NumOfYear--;
        }
        private void NextYear_Clicked(object sender, EventArgs e)
        {
            CurrentYear.NumOfYear++;
        }
    }
}