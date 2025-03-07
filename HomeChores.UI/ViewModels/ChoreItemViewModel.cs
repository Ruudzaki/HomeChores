using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HomeChores.Application.Commands;
using HomeChores.Application.Notifications;
using HomeChores.Domain.Entities;
using MediatR;

namespace HomeChores.UI.ViewModels;

public partial class ChoreItemViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private bool _isCompleted;

    public ChoreItemViewModel(Chore chore, IMediator mediator)
    {
        Chore = chore;
        _mediator = mediator;
        IsCompleted = chore.IsCompleted;

        // Register for notifications
        WeakReferenceMessenger.Default.Register<ChoreToggledMessage>(this, (r, message) =>
        {
            if (Chore.Id == message.ChoreId) IsCompleted = message.IsCompleted;
        });
    }

    public Chore Chore { get; }
    public string Title => Chore.Title;

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }

    [RelayCommand]
    private async Task ToggleCompleteAsync()
    {
        // Persist the change
        await _mediator.Send(new ToggleChoreCommand(Chore.Id));
    }
}