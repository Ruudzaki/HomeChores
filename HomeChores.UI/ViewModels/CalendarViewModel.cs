using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeChores.Application.Queries;
using MediatR;

namespace HomeChores.UI.ViewModels;

public partial class CalendarViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public CalendarViewModel(IMediator mediator)
    {
        _mediator = mediator;
        CalendarDays = new ObservableCollection<CalendarDay>();
    }

    public ObservableCollection<CalendarDay> CalendarDays { get; }

    public async Task LoadCalendar()
    {
        // Show current month (could be parameterized)
        var now = DateTime.Now;
        var year = now.Year;
        var month = now.Month;
        var firstDayOfMonth = new DateTime(year, month, 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);

        // Determine offset based on day of week (assuming Sunday = 0)
        var offset = (int)firstDayOfMonth.DayOfWeek;

        // Fetch chores from the repository via MediatR
        var chores = await _mediator.Send(new GetChoresDateCountQuery());

        // Clear existing calendar days
        CalendarDays.Clear();

        // Optionally, fill preceding days from previous month
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

        // Fill days for the current month
        for (var day = 1; day <= daysInMonth; day++)
        {
            var date = new DateTime(year, month, day);
            chores.TryGetValue(date, out var count);
            CalendarDays.Add(new CalendarDay
            {
                Date = date,
                ChoreCount = count,
                IsCurrentMonth = true
            });
        }

        // Fill trailing days from next month to complete the grid (if needed)
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
        await Shell.Current.GoToAsync($"///DailyChoresPage?date={day.Date:yyyy-MM-dd}");
    }
}