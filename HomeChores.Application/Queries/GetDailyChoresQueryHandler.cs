using HomeChores.Domain.Entities;
using HomeChores.Infrastructure.Interfaces;
using MediatR;

namespace HomeChores.Application.Queries;

public class GetDailyChoresQueryHandler : IRequestHandler<GetDailyChoresQuery, IEnumerable<Chore>>
{
    private readonly IChoreRepository _repository;

    public GetDailyChoresQueryHandler(IChoreRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Chore>> Handle(GetDailyChoresQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetDailyChoresAsync(request.Date);
    }
}