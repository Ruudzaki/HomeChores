using HomeChores.Application.Commands;
using HomeChores.Domain.Entities;
using HomeChores.Infrastructure.Interfaces;
using Moq;

namespace HomeChores.Tests;

public class CreateChoreCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_AddsChore()
    {
        var repositoryMock = new Mock<IChoreRepository>();
        var handler = new CreateChoreCommandHandler(repositoryMock.Object);

        var command = new CreateChoreCommand("Clean Kitchen");
        var result = await handler.Handle(command, CancellationToken.None);

        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Chore>()), Times.Once);
        Assert.NotEqual(Guid.Empty, result);
    }
}