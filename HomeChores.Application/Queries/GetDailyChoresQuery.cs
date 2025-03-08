using HomeChores.Domain.Entities;
using MediatR;

namespace HomeChores.Application.Queries;

public record GetDailyChoresQuery(DateTime Date) : IRequest<IEnumerable<Chore>>;