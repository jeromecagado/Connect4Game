using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Connect4Game.Splash_Feature;
using Connect4Game.SoundManag;
using Connect4Game.Logic;
using Connect4Game.Settings;
using Connect4Game.AI;



namespace Connect4Game
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Core Services
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<SoundManager>();

            // Game Settings & Logic
            builder.Services.AddSingleton<GameSettings>();
            builder.Services.AddTransient<GameLogic>();

            // App Shell & Root App
            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<AppShell>();

            // UI Pages
            builder.Services.AddTransient<SplashPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<GameMode>();


            // AI Strategies & Factory
            builder.Services.AddSingleton<EasyStrategy>();
            builder.Services.AddSingleton<MediumStrategy>();
            builder.Services.AddSingleton<HardStrategy>();
            builder.Services.AddSingleton<AiPlayerFactory>();

            // Sets the root app
            builder
                .UseMauiApp(serviceProvider => serviceProvider.GetService<App>())
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
