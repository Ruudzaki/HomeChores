using HomeChores.Domain.Entities;
using MediatR;

public record GetChoresQuery : IRequest<IEnumerable<Chore>>;