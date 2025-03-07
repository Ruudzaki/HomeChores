using HomeChores.Application.Commands;
using HomeChores.Infrastructure.Interfaces;
using HomeChores.UI;
using HomeChores.UI.ViewModels;

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

        // Repository
        builder.Services.AddSingleton<IChoreRepository, InMemoryChoreRepository>();

        // ViewModels
        builder.Services.AddTransient<ChoreViewModel>();

        return builder.Build();
    }
}