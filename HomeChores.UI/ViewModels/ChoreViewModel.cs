using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HomeChores.Application.Commands;
using HomeChores.Application.Notifications;
using MediatR;

namespace HomeChores.UI.ViewModels;

public class ChoreViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public ChoreViewModel(IMediator mediator)
    {
        _mediator = mediator;

        WeakReferenceMessenger.Default.Register<ChoreDeletedMessage>(this, (r, msg) =>
        {
            // Find the group containing the chore
            var group = GroupedChores.FirstOrDefault(g =>
                g.Any(itemVM => itemVM.Chore.Id == msg.ChoreId));

            if (group != null)
            {
                // Remove that chore from the group
                var choreItem = group.FirstOrDefault(i => i.Chore.Id == msg.ChoreId);
                if (choreItem != null)
                {
                    group.Remove(choreItem);

                    // If the group is now empty, remove the group
                    if (group.Count == 0)
                        GroupedChores.Remove(group);
                }
            }
        });
    }

    // Instead of a flat list, create a grouped collection:
    public ObservableCollection<ChoreGroup> GroupedChores { get; } = new();

    public async Task LoadChores()
    {
        var chores = await _mediator.Send(new GetChoresQuery());

        // Create ChoreItemViewModels (pass mediator along)
        var items = chores.Select(chore => new ChoreItemViewModel(chore, _mediator));

        // Group by the date (using the Date part only)
        var groups = items.GroupBy(item => item.Chore.PlannedDate.Date)
            .OrderBy(g => g.Key)
            .Select(g => new ChoreGroup(g.Key, g));

        GroupedChores.Clear();
        foreach (var group in groups) GroupedChores.Add(group);
    }

    public async Task AddChore(string title, DateTime plannedDate)
    {
        await _mediator.Send(new CreateChoreCommand(title, plannedDate));
        await LoadChores();
    }
}