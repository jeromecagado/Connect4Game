namespace Connect4Game.Settings;

using Connect4Game.SoundManag;

public partial class GameMode : ContentPage
{
	private readonly GameSettings _gameSettings;
	private readonly SoundManager _soundManager;
    private readonly AppShell _appShell;
    private bool _isTransitioning = false;
    public GameMode(GameSettings gameSettings, AppShell appShell, SoundManager soundManager)
    {
        InitializeComponent();
        _gameSettings = gameSettings;
        _appShell = appShell;
        _soundManager = soundManager;
    }

    private async void OnPlayerVsPlayerClicked(object sender, EventArgs e)
	{
        if (_isTransitioning)
        {
            return;
        }
        _isTransitioning = true;

		_gameSettings.IsVsAI = false;
		_gameSettings.Difficulty = AiDifficulty.Easy; // Default AI.

        await _soundManager.PlayIntroVoiceAsync();
        await Task.Delay(3000);
        Application.Current.MainPage = _appShell;
        
    }

	private void OnPlayervsAiClicked(object sender, EventArgs e)
	{
		_gameSettings.IsVsAI = true;
		DifficultyOptions.IsVisible = true;
	}

	private async void OnEasyAiClicked(object sender, EventArgs e)
	{
        if (_isTransitioning)
        {
            return;
        }
        _isTransitioning = true;

        _gameSettings.IsVsAI = true;
		_gameSettings.Difficulty = AiDifficulty.Easy;

        await _soundManager.PlayIntroVoiceAsync();
        await Task.Delay(3000);
        Application.Current.MainPage = _appShell;
    }

    private async void OnMediumAiClicked(object sender, EventArgs e)
    {
        if (_isTransitioning)
        {
            return;
        }
        _isTransitioning = true;

        _gameSettings.IsVsAI = true;
        _gameSettings.Difficulty = AiDifficulty.Medium;

        await _soundManager.PlayIntroVoiceAsync();
        await Task.Delay(3000);
        Application.Current.MainPage = _appShell;
    }

    private async void OnHardAiClicked(object sender, EventArgs e)
    {
        if (_isTransitioning)
        {
            return;
        }
        _isTransitioning = true;

        _gameSettings.IsVsAI = true;
        _gameSettings.Difficulty = AiDifficulty.Hard;

        await _soundManager.PlayIntroVoiceAsync();
        await Task.Delay(3000);
        Application.Current.MainPage = _appShell;
    }
}