using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HomeChores.Domain.Entities;
using MediatR;

namespace HomeChores.UI.ViewModels;

[QueryProperty(nameof(DateParam), "date")]
public partial class DailyChoresViewModel : ObservableObject, IQueryAttributable
{
    private readonly IMediator _mediator;

    [ObservableProperty] private string title;

    public DailyChoresViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public ObservableCollection<Chore> Chores { get; } = new();

    // The parameter from the URI
    public string DateParam { get; set; }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // date=yyyy-MM-dd from the Shell navigation
        if (query.TryGetValue("date", out var dateObj) && dateObj is string dateString)
            if (DateTime.TryParse(dateString, out var parsedDate))
            {
                Title = $"Chores for {parsedDate:MMM dd}";
                await LoadChoresForDate(parsedDate);
            }
    }

    private async Task LoadChoresForDate(DateTime date)
    {
        // e.g. fetch from repo or via MediatR query
        var chores = await _mediator.Send(new GetChoresQuery());
        var filtered = chores.Where(c => c.PlannedDate.Date == date.Date);

        Chores.Clear();
        foreach (var chore in filtered)
            Chores.Add(chore);
    }
}