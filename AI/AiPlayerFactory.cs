using Connect4Game.Settings;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Connect4Game.AI
{
    public class AiPlayerFactory
    {
        private readonly EasyStrategy _easy;
        private readonly MediumStrategy _medium;
        private readonly HardStrategy _hard;

        public AiPlayerFactory(EasyStrategy easy, MediumStrategy medium, HardStrategy hard)
        {
            _easy = easy;
            _medium = medium;
            _hard = hard;
        }

        public AiPlayer Create(AiDifficulty difficulty)
        {
            IStrategy strategy = difficulty switch
            {
                AiDifficulty.Easy => _easy,
                AiDifficulty.Medium => _medium,
                AiDifficulty.Hard => _hard,
                _ => _easy
            };

            return new AiPlayer(strategy);
        }
    }
}
