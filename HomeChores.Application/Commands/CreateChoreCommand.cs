using MediatR;

namespace HomeChores.Application.Commands;

public record CreateChoreCommand(string Title, DateTime PlannedDate) : IRequest<Guid>;