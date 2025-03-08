using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeChores.Application.Queries;
using MediatR;

namespace HomeChores.UI.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    private readonly IMediator _mediator;


    [ObservableProperty] private bool isBusy;

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
        IsBusy = true;
        // build your calendar
        // Step 1: do the heavy lifting in a Task.Run
        var calendarDaysList = await Task.Run(() =>
        {
            var list = new List<CalendarDay>();

            // Build your dictionary, offset logic, etc.
            // (Optionally do the DB fetch here, or store in a local variable)
            var chores = _mediator.Send(new GetChoresDateCountQuery()).Result;
            // .Result inside Task.Run is okay, or you can do it asynchronously with .ConfigureAwait(false).

            var firstDayOfMonth = new DateTime(SelectedYear, SelectedMonth, 1);
            var daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
            var offset = (int)firstDayOfMonth.DayOfWeek;

            // preceding days
            for (var i = 0; i < offset; i++)
            {
                var date = firstDayOfMonth.AddDays(-offset + i);
                chores.TryGetValue(date, out var count);
                list.Add(new CalendarDay { Date = date, ChoreCount = count, IsCurrentMonth = false });
            }

            // current month
            for (var day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(SelectedYear, SelectedMonth, day);
                chores.TryGetValue(date, out var count);
                list.Add(new CalendarDay { Date = date, ChoreCount = count, IsCurrentMonth = true });
            }

            // trailing
            var remainder = list.Count % 7;
            if (remainder != 0)
            {
                var trailing = 7 - remainder;
                var lastDate = list.Last().Date;
                for (var i = 1; i <= trailing; i++)
                {
                    var date = lastDate.AddDays(i);
                    chores.TryGetValue(date, out var count);
                    list.Add(new CalendarDay { Date = date, ChoreCount = count, IsCurrentMonth = false });
                }
            }

            IsBusy = false;
            return list;
        });

        // Step 2: update the UI-bound collection on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CalendarDays.Clear();
            foreach (var day in calendarDaysList)
                CalendarDays.Add(day);
        });
    }

    [RelayCommand]
    private async Task DaySelectedAsync(CalendarDay day)
    {
        if (day == null) return;
        // Navigate to the daily chores page
        await Shell.Current.GoToAsync($"///DailyChoresPage?date={day.Date:yyyy-MM-dd}");
    }
}