using MediatR;

namespace HomeChores.Application.Commands;

public record ToggleChoreCommand(Guid ChoreId) : IRequest;