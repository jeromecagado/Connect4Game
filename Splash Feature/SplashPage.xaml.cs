namespace Connect4Game.Splash_Feature;
using Connect4Game.SoundManag;

public partial class SplashPage : ContentPage
{
	private readonly SoundManager _soundManager;
	private readonly AppShell _appShell;
	private bool _hasStarted = false;
	public SplashPage(SoundManager soundManager, AppShell appShell)
	{
		InitializeComponent();
		_soundManager = soundManager;
		_appShell = appShell;
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
        await _soundManager.PlayIntroVoiceAsync();

        await Task.Delay(3000);
		Application.Current.MainPage = _appShell;
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

			await token.TranslateTo(0, 160 + (i * 10), 600, Easing.BounceOut);

			await token.FadeTo(0.7, 150);
			await token.FadeTo(1, 150);

			await _soundManager.PlayDropSound();

			await Task.Delay(200);  // Stagger drops
		}
        
    }

}