using Plugin.Maui.Audio;
using Connect4Game.SoundManag;
using Connect4Game.Splash_Feature;

namespace Connect4Game
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            var splashPage = serviceProvider.GetService<SplashPage>();
            MainPage = splashPage;
        }
    }
}
