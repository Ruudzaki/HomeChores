using MediatR;

namespace HomeChores.Application.Commands;

public record CreateChoreCommand(string Title) : IRequest<Guid>;