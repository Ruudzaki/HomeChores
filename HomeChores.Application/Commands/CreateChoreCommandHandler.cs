using HomeChores.Domain.Entities;
using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Commands;

public class CreateChoreCommandHandler : IRequestHandler<CreateChoreCommand, Guid>
{
    private readonly IChoreRepository _repository;

    public CreateChoreCommandHandler(IChoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = new Chore(request.Title, request.PlannedDate);
        await _repository.AddAsync(chore);
        return chore.Id;
    }
}