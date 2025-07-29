using Plugin.Maui.Audio;

namespace Connect4Game
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var audioManager = AudioManager.Current;
            var soundManager = new SoundManager(audioManager);

            MainPage = new AppShell(soundManager);
        }
    }
}
