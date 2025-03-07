using HomeChores.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeChores.Infrastructure.Data;

public class ChoreDbContext : DbContext
{
    public ChoreDbContext(DbContextOptions<ChoreDbContext> options) : base(options)
    {
    }

    public DbSet<Chore> Chores => Set<Chore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional config if needed
    }
}