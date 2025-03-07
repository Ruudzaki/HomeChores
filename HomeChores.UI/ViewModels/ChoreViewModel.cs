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

    public ObservableCollection<ChoreItemViewModel> Chores { get; } = new();

    public async Task LoadChores()
    {
        Chores.Clear();
        var chores = await _mediator.Send(new GetChoresQuery());
        foreach (var chore in chores)
        {
            // Pass the mediator in
            var itemVM = new ChoreItemViewModel(chore, _mediator);
            Chores.Add(itemVM);
        }
    }

    public async Task AddChore(string title)
    {
        await _mediator.Send(new CreateChoreCommand(title));
        await LoadChores();
    }
}