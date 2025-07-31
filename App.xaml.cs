using Plugin.Maui.Audio;
using Connect4Game.SoundManag;
using Connect4Game.Splash_Feature;

namespace Connect4Game
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var audioManager = AudioManager.Current;
            var soundManager = new SoundManager(audioManager);

            MainPage = new SplashPage(soundManager);
        }
    }
}
