using MediatR;

namespace HomeChores.Application.Queries;

public record GetChoresDateCountQuery : IRequest<Dictionary<DateTime, int>>;