using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Commands;

public class ToggleChoreCommandHandler : IRequestHandler<ToggleChoreCommand>
{
    private readonly IChoreRepository _repo;

    public ToggleChoreCommandHandler(IChoreRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(ToggleChoreCommand request, CancellationToken ct)
    {
        var chores = await _repo.GetAllAsync();
        var chore = chores.FirstOrDefault(c => c.Id == request.ChoreId);
        if (chore != null)
        {
            chore.ToggleComplete();
            await _repo.UpdateAsync(chore);
        }
    }
}