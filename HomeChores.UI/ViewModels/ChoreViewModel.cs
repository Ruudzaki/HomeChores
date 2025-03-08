using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HomeChores.Application.Commands;
using MediatR;

namespace HomeChores.UI.ViewModels;

public class ChoreViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public ChoreViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Instead of a flat list, create a grouped collection:
    public ObservableCollection<ChoreGroup> GroupedChores { get; } = new();

    public async Task LoadChores()
    {
        GroupedChores.Clear();
        var chores = await _mediator.Send(new GetChoresQuery());

        // Create ChoreItemViewModels (pass mediator along)
        var items = chores.Select(chore => new ChoreItemViewModel(chore, _mediator));

        // Group by the date (using the Date part only)
        var groups = items.GroupBy(item => item.Chore.PlannedDate.Date)
            .OrderBy(g => g.Key)
            .Select(g => new ChoreGroup(g.Key, g));

        foreach (var group in groups) GroupedChores.Add(group);
    }

    public async Task AddChore(string title, DateTime plannedDate)
    {
        await _mediator.Send(new CreateChoreCommand(title, plannedDate));
        await LoadChores();
    }
}