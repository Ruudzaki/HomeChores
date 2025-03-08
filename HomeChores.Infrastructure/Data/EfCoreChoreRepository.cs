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

    public async Task DeleteAsync(Guid choreId)
    {
        var chore = await _db.Chores.FindAsync(choreId);
        if (chore != null)
        {
            _db.Chores.Remove(chore);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Dictionary<DateTime, int>> GetDateCountAsync()
    {
        var dailyCounts = await _db.Chores
            .GroupBy(c => c.PlannedDate.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.Date, x => x.Count);

        return dailyCounts;
    }
}