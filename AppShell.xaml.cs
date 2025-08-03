using Connect4Game.SoundManag;

namespace Connect4Game
{
    public partial class AppShell : Shell
    {
        private readonly SoundManager _soundManager;
        public AppShell(SoundManager soundManager, Page gamePage)
        {
            InitializeComponent();

            Items.Add(new ShellContent
            {
                Title = "Connect4Game",
                Content = gamePage
            });
        }
    }
}
