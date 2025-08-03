using System;
using System.Collections.Generic;
using System.Linq;

namespace Connect4Game.Settings
{
    public class GameSettings
    {
        public bool IsVsAI { get; set; }
        public AiDifficulty Difficulty { get; set; }
    }

    public enum AiDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}
