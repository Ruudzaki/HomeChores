using HomeChores.Application.Commands;
using HomeChores.Infrastructure.Data;
using HomeChores.Infrastructure.Interfaces;
using HomeChores.UI;
using HomeChores.UI.ViewModels;
using Microsoft.EntityFrameworkCore;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        // MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateChoreCommand).Assembly));

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "chores.db");

        builder.Services.AddDbContext<ChoreDbContext>(options => { options.UseSqlite($"Data Source={dbPath}"); });

        builder.Services.AddScoped<IChoreRepository, EfCoreChoreRepository>();

        // ViewModels
        builder.Services.AddTransient<ChoreViewModel>();

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ChoreDbContext>();
            db.Database.Migrate(); // or EnsureCreated()
        }

        return builder.Build();
    }
}