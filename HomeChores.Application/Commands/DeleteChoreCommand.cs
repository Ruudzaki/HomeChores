using MediatR;

namespace HomeChores.Application.Commands;

public record DeleteChoreCommand(Guid ChoreId) : IRequest;