using MediatR;

namespace HomeChores.Application.Commands;

public record CompleteChoreCommand(Guid ChoreId) : IRequest;