using HomeChores.Domain.Entities;
using HomeChores.Infrastructure.Interfaces;

public class InMemoryChoreRepository : IChoreRepository
{
    private readonly List<Chore> _chores = new();

    public Task AddAsync(Chore chore)
    {
        _chores.Add(chore);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Chore>> GetAllAsync()
    {
        return Task.FromResult(_chores.AsEnumerable());
    }

    public Task UpdateAsync(Chore chore)
    {
        // In-memory, no special update logic. EF Core would do SaveChanges here.
        return Task.CompletedTask;
    }
}