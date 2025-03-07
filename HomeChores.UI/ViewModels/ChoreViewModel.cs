using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeChores.Application.Commands;
using HomeChores.Domain.Entities;
using MediatR;

namespace HomeChores.UI.ViewModels;

public partial class ChoreViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public ChoreViewModel(IMediator mediator)
    {
        _mediator = mediator;
        // Load existing chores via a query if needed.
    }

    public ObservableCollection<Chore> Chores { get; } = new();

    public async Task AddChore(string title)
    {
        var choreId = await _mediator.Send(new CreateChoreCommand(title));
        // Optionally retrieve and update UI with new chore.
    }

    public async Task LoadChores()
    {
        Chores.Clear();
        var chores = await _mediator.Send(new GetChoresQuery());
        foreach (var chore in chores)
            Chores.Add(chore);
    }

    [RelayCommand]
    private async Task ToggleChoreAsync(Chore chore)
    {
        await _mediator.Send(new ToggleChoreCommand(chore.Id));
        await LoadChores();
    }
}