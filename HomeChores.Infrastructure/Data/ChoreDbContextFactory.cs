using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeChores.Infrastructure.Data;

public class ChoreDbContextFactory : IDesignTimeDbContextFactory<ChoreDbContext>
{
    public ChoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChoreDbContext>();
        optionsBuilder.UseSqlite("Data Source=chores.db");
        return new ChoreDbContext(optionsBuilder.Options);
    }
}