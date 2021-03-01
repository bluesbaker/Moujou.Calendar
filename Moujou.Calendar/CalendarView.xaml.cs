using Moujou.Calendar.Converters;
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
            BindableProperty.Create("Day", typeof(int), typeof(CalendarView), DateTime.Now.Day);
        public static readonly BindableProperty MonthProperty =
            BindableProperty.Create("Month", typeof(int), typeof(CalendarView), DateTime.Now.Month);
        public static readonly BindableProperty YearProperty =
            BindableProperty.Create("Year", typeof(int), typeof(CalendarView), DateTime.Now.Year);
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

        readonly ObservableCollection<CalendarDay[]> Months = new ObservableCollection<CalendarDay[]>();
        
        public CalendarView()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            GenerateWeekDays();
            GenerateCarouselItemTemplate();
            InitializeMonthCollection(Year);
            calendarLayout.BindingContext = this;
        }       

        private void GenerateCarouselItemTemplate()
        {
            // Add template of the cells
            cellsCarousel.ItemTemplate = new DataTemplate(() =>
            {
                // Generate cells of Grid
                Grid cellsGrid = new Grid();
                for (int row = 0; row < 6; row++)
                    cellsGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int column = 0; column < 7; column++)
                    cellsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                // Filling the cellsGrid with cellButtons
                int countArrDays = 0;
                for (int row = 0; row < cellsGrid.RowDefinitions.Count; row++)
                {
                    for (int column = 0; column < cellsGrid.ColumnDefinitions.Count; column++)
                    {
                        Button cellButton = new Button();
                        cellButton.SetValue(Grid.RowProperty, row);
                        cellButton.SetValue(Grid.ColumnProperty, column);
                        cellButton.SetBinding(Button.TextProperty, $"[{countArrDays}].NumOfDay");
                        cellButton.SetBinding(Button.IsVisibleProperty, $"[{countArrDays}].NumOfDay", BindingMode.Default, new NumToVisibleConverter());
                        cellsGrid.Children.Add(cellButton);
                        countArrDays++;
                    }
                }
                return cellsGrid;
            });
            cellsCarousel.ItemsSource = Months;
            cellsCarousel.CurrentItemChanged += CellsCarousel_CurrentItemChanged;
        }

        private void CellsCarousel_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {         
            Month = Months.IndexOf((CalendarDay[])e.CurrentItem);
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

        private void InitializeMonthCollection(int year)
        {
            for (int month = 1; month <= 12; month++)
            {
                CalendarDay[] days = GetDaysOfMonth(new DateTime(year, month, 1));
                Months.Add(days);
                // Add default current month
                if (month == Month) cellsCarousel.CurrentItem = days;
            }
        }

        private CalendarDay[] GetDaysOfMonth(DateTime date)
        {
            CalendarDay[] days = new CalendarDay[42];
            DayOfWeek dayOfWeek = new DateTime(date.Year, date.Month, 1).DayOfWeek;
            int currentIndex = 0;

            // "Before" incorrect days
            for(; currentIndex < (dayOfWeek == 0 ? 7 : (int)dayOfWeek) - 1; currentIndex++)
                days[currentIndex] = new CalendarDay() { NumOfDay = 0 };           
            // Correct days
            for (int day = 1; day <= DateTime.DaysInMonth(date.Year, date.Month); day++, currentIndex++)
            {
                days[currentIndex] = new CalendarDay()
                {
                    NumOfDay = day,
                    IsSelected = (date.Year == Year && date.Month == Month & date.Day == Day)
                };
            }
            // "After" incorrect days
            for (; currentIndex < days.Length; currentIndex++)
                days[currentIndex] = new CalendarDay() { NumOfDay = 0 };

            return days;
        }

        private void PreviousYear_Clicked(object sender, EventArgs e)
        {
            Months.Clear();
            InitializeMonthCollection(--Year);
        }
        private void NextYear_Clicked(object sender, EventArgs e)
        {
            Months.Clear();
            InitializeMonthCollection(++Year);
        }
    }
}