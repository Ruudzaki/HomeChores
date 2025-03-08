using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeChores.Application.Queries;
using MediatR;

namespace HomeChores.UI.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty] private int selectedMonth;

    [ObservableProperty] private int selectedYear;

    public CalendarViewModel(IMediator mediator)
    {
        _mediator = mediator;
        CalendarDays = new ObservableCollection<CalendarDay>();
        // Initialize to current month
        SelectedYear = DateTime.Now.Year;
        SelectedMonth = DateTime.Now.Month;
    }

    public ObservableCollection<CalendarDay> CalendarDays { get; }

    public string MonthYearDisplay => new DateTime(SelectedYear, SelectedMonth, 1).ToString("MMMM yyyy");

    [RelayCommand]
    private async Task PreviousMonthAsync()
    {
        var date = new DateTime(SelectedYear, SelectedMonth, 1).AddMonths(-1);
        SelectedYear = date.Year;
        SelectedMonth = date.Month;
        OnPropertyChanged(nameof(MonthYearDisplay));
        await LoadCalendar();
    }

    [RelayCommand]
    private async Task NextMonthAsync()
    {
        var date = new DateTime(SelectedYear, SelectedMonth, 1).AddMonths(1);
        SelectedYear = date.Year;
        SelectedMonth = date.Month;
        OnPropertyChanged(nameof(MonthYearDisplay));
        await LoadCalendar();
    }

    public async Task LoadCalendar()
    {
        // Use SelectedYear/SelectedMonth for current display
        var firstDayOfMonth = new DateTime(SelectedYear, SelectedMonth, 1);
        var daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

        // Calculate offset (assuming Sunday = 0)
        var offset = (int)firstDayOfMonth.DayOfWeek;

        // Fetch dictionary of chores per day from your query:
        // Key: Date, Value: count
        var chores = await _mediator.Send(new GetChoresDateCountQuery());

        // Clear existing days
        CalendarDays.Clear();

        // Preceding days (from previous month)
        for (var i = 0; i < offset; i++)
        {
            var date = firstDayOfMonth.AddDays(-offset + i);
            chores.TryGetValue(date, out var count);
            CalendarDays.Add(new CalendarDay
            {
                Date = date,
                ChoreCount = count,
                IsCurrentMonth = false
            });
        }

        // Days for the current month
        for (var day = 1; day <= daysInMonth; day++)
        {
            var date = new DateTime(SelectedYear, SelectedMonth, day);
            chores.TryGetValue(date, out var count);
            CalendarDays.Add(new CalendarDay
            {
                Date = date,
                ChoreCount = count,
                IsCurrentMonth = true
            });
        }

        // Trailing days (next month) to fill grid (if needed)
        var totalCells = CalendarDays.Count;
        var remainder = totalCells % 7;
        if (remainder != 0)
        {
            var trailing = 7 - remainder;
            var lastDate = CalendarDays.Last().Date;
            for (var i = 1; i <= trailing; i++)
            {
                var date = lastDate.AddDays(i);
                chores.TryGetValue(date, out var count);
                CalendarDays.Add(new CalendarDay
                {
                    Date = date,
                    ChoreCount = count,
                    IsCurrentMonth = false
                });
            }
        }
    }

    [RelayCommand]
    private async Task DaySelectedAsync(CalendarDay day)
    {
        if (day == null) return;
        // Navigate to the daily chores page
        await Shell.Current.GoToAsync($"///DailyChoresPage?date={day.Date:yyyy-MM-dd}");
    }
}