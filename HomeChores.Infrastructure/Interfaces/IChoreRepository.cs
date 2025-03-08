using HomeChores.Domain.Entities;

namespace HomeChores.Infrastructure.Interfaces;

public interface IChoreRepository
{
    Task AddAsync(Chore chore);
    Task<IEnumerable<Chore>> GetAllAsync();
    Task UpdateAsync(Chore chore);

    Task DeleteAsync(Guid choreId);
}