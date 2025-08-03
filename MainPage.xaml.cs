using Connect4Game.AI;
using Connect4Game.Logic;
using Connect4Game.Settings;
using Connect4Game.SoundManag;
using Plugin.Maui.Audio;
using System.Runtime.CompilerServices;

namespace Connect4Game
{
    public partial class MainPage : ContentPage
    {
        private readonly AiPlayer _aiPlayer;
        private readonly SoundManager _soundManager;
        private readonly BoxView[] indicatorTokens = new BoxView[7];
        private readonly TapGestureRecognizer[] _tapRecognizers = new TapGestureRecognizer[7];
        private readonly GameLogic _game;
        // Stores references to the visual tokens on the grid
        private readonly BoxView[,] tokens = new BoxView[6, 7];
        // Prevents duplicate drops
        private bool isDropping = false;
        private readonly GameSettings _gameSettings;
        // Checks for AI flag.
        private bool isVsAI = false;

        public MainPage(GameSettings gameSettings, SoundManager soundManager, GameLogic gameLogic, AiPlayer? aiPlayer)
        {
            InitializeComponent();
            _gameSettings = gameSettings;
            _soundManager = soundManager;
            _game = gameLogic;
            _aiPlayer = aiPlayer;

            isVsAI = _gameSettings.IsVsAI;
            System.Diagnostics.Debug.WriteLine($"Game Mode: {(isVsAI ? "Player vs AI" : "Player vs Player")}");


            indicatorTokens[0] = Indicator0;
            indicatorTokens[1] = Indicator1;
            indicatorTokens[2] = Indicator2;
            indicatorTokens[3] = Indicator3;
            indicatorTokens[4] = Indicator4;
            indicatorTokens[5] = Indicator5;
            indicatorTokens[6] = Indicator6;


            for (int i = 0; i < indicatorTokens.Length; i++)
            {
                var indicator = indicatorTokens[i];

                var pointerEnter = new PointerGestureRecognizer();
                pointerEnter.PointerEntered += (s, e) => ApplyHoverEffect(indicator);

                var pointerExit = new PointerGestureRecognizer();
                pointerExit.PointerExited += (s, e) => ClearHoverEffect(indicator);

                indicator.GestureRecognizers.Add(pointerEnter);
                indicator.GestureRecognizers.Add(pointerExit);
            }


            // Create 6 rows
            for (int row = 0; row < 6; row++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            // Create 7 columns
            for (int col = 0; col < 7; col++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Add circular tokens (BoxViews)
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    var token = new BoxView
                    {
                        BackgroundColor = Colors.LightGray,
                        CornerRadius = 100,
                        Margin = 8
                    };

                    tokens[row, col] = token; // Save reference

                    Grid.SetRow(token, row);
                    Grid.SetColumn(token, col);
                    GameGrid.Children.Add(token);
                }
            }

            GameGrid.SizeChanged += (s, e) =>
            {
                double cellWidth = (GameGrid.Width - 20) / 7;
                double cellHeight = (GameGrid.Height - 20) / 6;
                double size = Math.Min(cellWidth, cellHeight) - 8;

                foreach (var token in tokens)
                {
                    token.WidthRequest = size;
                    token.HeightRequest = size;
                    token.CornerRadius = size / 2;
                }
            };

            UpdateIndicatorColors();
        }

        private async void PlayDropSound()
        {
            await _soundManager.PlayDropSound();
        }

        private async void PlayResetSound()
        {
            await _soundManager.PlayResetSound();
        }
        private async void OnArrowsTapped(object sender, EventArgs e)
        {
            if (isVsAI && _game.CurrentPlayer == 2)
                return;

            if (sender is BoxView box &&
                box.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tap &&
                tap.CommandParameter is string param &&
                int.TryParse(param, out int column))
            {

                // Handle the logic for dropping a disc in the selected column
                await DropDiscInColumn(column);
            }
        }
        private async Task DropDiscInColumn(int column)
        {
            if (isDropping) return;
            isDropping = true;

            for (int row = 5; row >= 0; row--)
            {
                if (_game.Board[row, column] == 0)
                {
                    _game.Board[row, column] = _game.CurrentPlayer;
                    tokens[row, column].BackgroundColor = _game.CurrentPlayer == 1 ? Colors.Red : Colors.Yellow;

                    // Gravity animation
                    tokens[row, column].TranslationY = -400;
                    tokens[row, column].Scale = 0.6;

                    await Task.WhenAll(
                        tokens[row, column].TranslateTo(0, 0, 400, Easing.BounceOut),
                        tokens[row, column].ScaleTo(1.0, 400, Easing.CubicOut)
                    );
                    PlayDropSound();
                    

                    tokens[row, column].Scale = 0.5;
                    await tokens[row, column].ScaleTo(1, 300, Easing.BounceOut);

                    // Check for win
                    if (_game.CheckForWin(row, column))
                    {
                        if (isVsAI && _game.CurrentPlayer == 2)
                        {
                            await DisplayAlert("Connect Four!", "AI wins!", "Play Again");
                            _game.ResetGame();
                    //        RemoveAllTapGestures();
                            PlayResetSound();
                            ClearBoardVisuals();
                            UpdateIndicatorColors();
                            isDropping = false;
                            return;
                        }
                        await DisplayAlert("Connect Four!", $"Player {_game.CurrentPlayer} wins!", "Play Again");
                        _game.ResetGame();
                    //    RemoveAllTapGestures();
                        PlayResetSound();
                        ClearBoardVisuals();
                        UpdateIndicatorColors();
                        isDropping = false;
                        return;
                    }
                    else if (_game.IsBoardFull())
                    {
                        await DisplayAlert("Draw!", "No more moves - it's a tie!", "Play Again");
                        _game.ResetGame();
                  //      RemoveAllTapGestures();
                        PlayResetSound();
                        ClearBoardVisuals();
                        UpdateIndicatorColors();
                        isDropping = false;
                        return;
                    }
                    else
                    {
                        // Switch turns
                        _game.SwitchPlayer();
                        UpdateIndicatorColors();

                        // If AI, AI will make a move. 
                        if (isVsAI && _game.CurrentPlayer == 2)
                        {
                            await Task.Delay(500);
                            int aiColumn = _aiPlayer.DecideMove(_game.Board);
                            Console.WriteLine($"AI chose column: {aiColumn}");
                            isDropping = false;
                            await DropDiscInColumn(aiColumn);
                        }
                    }
                    // Now unlock
                    isDropping = false;
                    return;
                }
            }
            // If no empty slot found
            await DisplayAlert("Column Full", "Try a different column!", "OK");
            isDropping = false;
        }

        private void UpdateIndicatorColors()
        {

            var color = _game.CurrentPlayer == 1 ? Colors.Red : Colors.Yellow;
            TurnToken.BackgroundColor = color;

            if (isVsAI && _game.CurrentPlayer == 2)
            {
                TurnText.Text = "AI's Turn";
            }
            else
            {
                TurnText.Text = $"Player {_game.CurrentPlayer}'s Turn";
            }
                
            for (int col = 0; col < indicatorTokens.Length; col++)
            {
                indicatorTokens[col].BackgroundColor = color;
                indicatorTokens[col].Opacity = (isVsAI && _game.CurrentPlayer == 2) ? 0.5 : 1.0;
            }

          //  SetArrowInputEnabled(!(isVsAI && _game.CurrentPlayer == 2));
        }

        private void ApplyHoverEffect(BoxView box)
        {
            var color = _game.CurrentPlayer == 1 ? Color.FromArgb("#FF6666") : Color.FromArgb("#FFFF66");
            box.BackgroundColor = color;
            box.Scale = 1.2;
        }

        private void ClearHoverEffect(BoxView box)
        {
            var color = _game.CurrentPlayer == 1 ? Colors.Red : Colors.Yellow;
            box.BackgroundColor = color;
            box.Scale = 1.0;
        }
        private void ClearBoardVisuals()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    tokens[row, col].BackgroundColor = Colors.LightGray;
                }
            }
        }
    }
}
