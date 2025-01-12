using Microsoft.Extensions.Logging;

namespace FotoApp
{
    public static class MauiProgram
    {

        // CreateMauiApp method to initialize and configure the Maui app
        public static MauiApp CreateMauiApp()
        {
            SQLitePCL.Batteries_V2.Init();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register the DatabaseService as a singleton
            builder.Services.AddSingleton<DatabaseService>(sp =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyAppDatabase.db");
                return new DatabaseService(dbPath);
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }


}
