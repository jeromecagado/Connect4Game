namespace Connect4Game.Settings;

using Connect4Game.SoundManag;
using Connect4Game.AI;
using Connect4Game.Logic;

public partial class GameMode : ContentPage
{
	private readonly GameSettings _gameSettings;
	private readonly SoundManager _soundManager;
    private readonly AiPlayerFactory _aiPlayerFactory;
    private bool _isTransitioning = false;
    public GameMode(GameSettings gameSettings, SoundManager soundManager, AiPlayerFactory aiPlayerFactory)
    {
        InitializeComponent();
        _gameSettings = gameSettings;
        _soundManager = soundManager;
        _aiPlayerFactory = aiPlayerFactory;
    }

    private async void OnPlayerVsPlayerClicked(object sender, EventArgs e)
	{
        await TransitionToGameAsync(AiDifficulty.Easy, false);

    }

	private void OnPlayerVsAiClicked(object sender, EventArgs e)
	{
		_gameSettings.IsVsAI = true;
		DifficultyOptions.IsVisible = true;
	}

    private async Task TransitionToGameAsync(AiDifficulty difficulty, bool isVsAI)
    {
        if (_isTransitioning) return;
        _isTransitioning = true;

        _gameSettings.IsVsAI = isVsAI;
        _gameSettings.Difficulty = difficulty;

        await _soundManager.PlayIntroVoiceAsync();
        await Task.Delay(3000);

        AiPlayer? aiPlayer = isVsAI ? _aiPlayerFactory.Create(difficulty) : null;
        var gameLogic = new GameLogic();
        var mainPage = new MainPage(_gameSettings, _soundManager, gameLogic, aiPlayer);
        var appShell = new AppShell(_soundManager, mainPage);

        Application.Current.MainPage = appShell;
    }
    private async void OnEasyAiClicked(object sender, EventArgs e)
	{
        await TransitionToGameAsync(AiDifficulty.Easy, true);

    }

    private async void OnMediumAiClicked(object sender, EventArgs e)
    {
        await TransitionToGameAsync(AiDifficulty.Medium, true);
    }

    private async void OnHardAiClicked(object sender, EventArgs e)
    {
        await TransitionToGameAsync(AiDifficulty.Hard, true);
    }
}