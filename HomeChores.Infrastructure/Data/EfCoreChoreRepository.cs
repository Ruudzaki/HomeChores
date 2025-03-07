using HomeChores.Domain.Entities;
using HomeChores.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeChores.Infrastructure.Data;

public class EfCoreChoreRepository : IChoreRepository
{
    private readonly ChoreDbContext _db;

    public EfCoreChoreRepository(ChoreDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Chore chore)
    {
        await _db.Chores.AddAsync(chore);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Chore>> GetAllAsync()
    {
        return await _db.Chores.ToListAsync();
    }

    public async Task UpdateAsync(Chore chore)
    {
        _db.Chores.Update(chore);
        await _db.SaveChangesAsync();
    }
}