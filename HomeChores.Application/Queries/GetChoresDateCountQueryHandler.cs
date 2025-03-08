using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Queries;

public class GetChoresDateCountQueryHandler : IRequestHandler<GetChoresDateCountQuery, Dictionary<DateTime, int>>
{
    private readonly IChoreRepository _repository;

    public GetChoresDateCountQueryHandler(IChoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Dictionary<DateTime, int>> Handle(GetChoresDateCountQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetDateCountAsync();
    }
}