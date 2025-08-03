using System;
using System.Collections.Generic;
using System.Linq;


namespace Connect4Game.AI
{
    public class AiPlayer
    {
        private readonly IStrategy _strategy;

        public AiPlayer(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public int DecideMove(int[,] boardState)
        {
            return _strategy.GetMove(boardState);
        }

    }
}
