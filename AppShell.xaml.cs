using Connect4Game.SoundManag;

namespace Connect4Game
{
    public partial class AppShell : Shell
    {
        private readonly SoundManager soundManager;
        public AppShell(SoundManager soundManager, MainPage mainPage)
        {
            InitializeComponent();

            Items.Add(new ShellContent
            {
                Title = "Connect4Game",
                Content = mainPage,
                Route = "MainPage"
            });
        }
    }
}
