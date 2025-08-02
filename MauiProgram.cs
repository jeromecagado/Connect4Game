using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Connect4Game.Splash_Feature;
using Connect4Game.SoundManag;
using Connect4Game.Logic;


namespace Connect4Game
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // register dependencies
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<SoundManager>();
            builder.Services.AddTransient<SplashPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<GameLogic>();

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
