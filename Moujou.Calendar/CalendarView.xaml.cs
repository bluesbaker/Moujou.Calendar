using Moujou.Calendar.ContentViews;
using Moujou.Calendar.Models;
using System;
using System.Globalization;
using System.Linq;
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

        #region Style BindableProperties
        public static readonly BindableProperty CellStyleProperty =
            BindableProperty.Create("CellStyle", typeof(Style), typeof(CalendarView), null);

        public Style CellStyle
        {
            get => (Style)GetValue(CellStyleProperty);
            set => SetValue(CellStyleProperty, value);
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
            // Data(date) initialization
            CurrentYear = new CalendarYear(new DateTime(Year, Month, Day));
            CurrentYear.AssignmentDays();

            cellCarousel.ItemTemplate = CreateCalendarDataTemplate();
            cellCarousel.ItemsSource = CurrentYear.Months;
            cellCarousel.CurrentItem = CurrentYear.Months.ElementAtOrDefault(Month - 1);
        }

        private DataTemplate CreateCalendarDataTemplate()
        {
            return new DataTemplate(() =>
            {
                // Generate cell grid
                Grid cellGrid = new Grid
                {
                    ColumnSpacing = 2,
                    RowSpacing = 2
                };
                for (int row = 0; row < 6; row++)
                    cellGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int column = 0; column < 7; column++)
                    cellGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                
                // Filling the cellGrid with DayCells
                int cellCount = 0;
                for (int row = 0; row < cellGrid.RowDefinitions.Count; row++)
                {
                    for (int column = 0; column < cellGrid.ColumnDefinitions.Count; column++)
                    {
                        DayCell dayCell = new DayCell();
                        dayCell.SetValue(Grid.RowProperty, row);
                        dayCell.SetValue(Grid.ColumnProperty, column);
                        dayCell.SetBinding(DayCell.DayProperty, $"Days[{cellCount}]");

                        //cellFrame.SetBinding(DayFrame.StyleProperty, new Binding(nameof(CellStyle), source: this));
                        cellGrid.Children.Add(dayCell);
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

        private async void PreviousYear_Clicked(object sender, EventArgs e)
        {
            await cellCarousel.FadeTo(0, 100);
            CurrentYear.NumOfYear--;
            CurrentYear.AssignmentDays();
            await cellCarousel.FadeTo(1, 100);
        }
        private async void NextYear_Clicked(object sender, EventArgs e)
        {
            await cellCarousel.FadeTo(0, 100);
            CurrentYear.NumOfYear++;
            CurrentYear.AssignmentDays();
            await cellCarousel.FadeTo(1, 100);
        }
    }
}