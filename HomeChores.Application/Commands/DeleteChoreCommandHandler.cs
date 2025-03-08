using CommunityToolkit.Mvvm.Messaging;
using HomeChores.Application.Notifications;
using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Commands;

public class DeleteChoreCommandHandler : IRequestHandler<DeleteChoreCommand>
{
    private readonly IChoreRepository _repo;

    public DeleteChoreCommandHandler(IChoreRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteChoreCommand request, CancellationToken ct)
    {
        await _repo.DeleteAsync(request.ChoreId);
        WeakReferenceMessenger.Default.Send(new ChoreDeletedMessage(request.ChoreId));
    }
}