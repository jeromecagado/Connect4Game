namespace Connect4Game
{
    public partial class AppShell : Shell
    {
        private readonly SoundManager soundManager;
        public AppShell(SoundManager soundManager)
        {
            InitializeComponent();

            var mainPage = new MainPage(soundManager);

            Items.Add(new ShellContent
            {
                Title = "Connect4Game",
                Content = mainPage,
                Route = "MainPage"
            });
        }
    }
}
