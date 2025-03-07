using HomeChores.Domain.Entities;
using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Queries;

public class GetChoresQueryHandler : IRequestHandler<GetChoresQuery, IEnumerable<Chore>>
{
    private readonly IChoreRepository _repository;

    public GetChoresQueryHandler(IChoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Chore>> Handle(GetChoresQuery request, CancellationToken ct)
    {
        return await _repository.GetAllAsync();
    }
}