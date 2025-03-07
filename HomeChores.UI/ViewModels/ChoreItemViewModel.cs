using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeChores.Application.Commands;
using HomeChores.Domain.Entities;
using MediatR;

namespace HomeChores.UI.ViewModels;

public partial class ChoreItemViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty] private bool isCompleted;

    public ChoreItemViewModel(Chore chore, IMediator mediator)
    {
        Chore = chore;
        _mediator = mediator;

        IsCompleted = chore.IsCompleted;
    }

    public Chore Chore { get; }
    public string Title => Chore.Title;

    [RelayCommand]
    private async Task ToggleCompleteAsync()
    {
        // Toggle locally
        Chore.ToggleComplete();
        IsCompleted = Chore.IsCompleted;

        // Persist the change
        await _mediator.Send(new ToggleChoreCommand(Chore.Id));
    }
}