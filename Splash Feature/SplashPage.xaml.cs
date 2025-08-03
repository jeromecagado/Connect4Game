using Connect4Game.Settings;
using Connect4Game.SoundManag;

namespace Connect4Game.Splash_Feature;

public partial class SplashPage : ContentPage
{
	private readonly IServiceProvider _serviceProvider;
	private readonly SoundManager _soundManager;
	private readonly GameSettings _gameSettings;
	private readonly GameMode _gameMode;
	private bool _hasStarted = false;
	public SplashPage(IServiceProvider serviceProvider, SoundManager soundManager, GameSettings gameSettings, GameMode gameMode)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
		_soundManager = soundManager;
		_gameSettings = gameSettings;
		_gameMode = gameMode;
	}

	private async void OnPlayButtonClicked(object sender, EventArgs e)
	{
		// Tested duplicate bug.
		//Console.WriteLine("Play Button clicked");
		if (_hasStarted)
		{
			Console.WriteLine("Splash already started - ignoring extra tap.");
			return;
		}

		_hasStarted = true;
		PlayButton.IsEnabled = false; // prevent further interaction

		Console.WriteLine("Starting splash sequence...");

		await DropTokensAsync();
        
		var _gameMode = _serviceProvider.GetService<GameMode>();
		Application.Current.MainPage = _gameMode;
	}

	private async Task DropTokensAsync()
	{
		var layout = Content as Layout;

		for (int i = 0; i < 4; i++)
		{
			var token = new BoxView
			{
				Color = i % 2 == 0 ? Colors.Red : Colors.Yellow,
				WidthRequest = 40,
				HeightRequest = 40,
				CornerRadius = 20,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Start
			};

			layout.Children.Add(token);

			token.TranslationY = -100;

            await _soundManager.PlayDropSound();
            await token.TranslateTo(0, 160 + (i * 10), 600, Easing.BounceOut);

			await token.FadeTo(0.7, 150);
			await token.FadeTo(1, 150);
			await Task.Delay(150);  // Stagger drops
		}
        
    }

}