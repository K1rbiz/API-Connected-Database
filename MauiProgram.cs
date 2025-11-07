// MauiProgram.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using API_Connected_Database.Services;
using SQLitePCL;

namespace API_Connected_Database;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Ensure SQLite native bits are initialized before first use.
        Batteries_V2.Init(); // prevents runtime SQLite crashes

        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // REQUIRED for Blazor Hybrid
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Http + Services
        builder.Services.AddSingleton(new HttpClient());
        builder.Services.AddSingleton<ICatFactsService, CatFactsService>();

        // SQLite Repository (local DB path)
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "catfacts.db3");
        builder.Services.AddSingleton<ICatFactsRepository>(_ => new CatFactsRepository(dbPath));

        return builder.Build();
    }
}
