using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Commands;

public class CompleteChoreCommandHandler : IRequestHandler<CompleteChoreCommand>
{
    private readonly IChoreRepository _repository;

    public CompleteChoreCommandHandler(IChoreRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CompleteChoreCommand request, CancellationToken ct)
    {
        var chores = await _repository.GetAllAsync();
        var chore = chores.FirstOrDefault(c => c.Id == request.ChoreId);
        chore?.MarkCompleted();
        if (chore != null) await _repository.UpdateAsync(chore);
    }
}