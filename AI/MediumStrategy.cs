using Connect4Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Connect4Game.AI
{
    public class MediumStrategy : IStrategy
    {
        private readonly Random _random = new();
        private readonly AnalyzeBoard _analyzer;

        public MediumStrategy(AnalyzeBoard analyzer)
        {
            _analyzer = analyzer;
        }

        public int GetMove(int[,] boardState)
        {
            var validMoves = _analyzer.GetValidMoves(boardState);
            var aiPlayer = PlayerType.AI;
            var humanPlayer = PlayerType.Human;

            // AI will try to win.
            foreach (var col in validMoves)
            {
                if (_analyzer.IsWinningMove(boardState, col, aiPlayer))
                {
                    return col;
                }
            }

            // Block player from winning
            foreach (var col in validMoves)
            {
                if (_analyzer.IsWinningMove(boardState, col, humanPlayer))
                {
                    return col;
                }
            }

            // Focus on center columns
            int center = boardState.GetLength(1) / 2;
            if (validMoves.Contains(center))
            {
                return center;
            }

            // Prefer columns near center
            var sortedByCenterProximity = validMoves
                .OrderBy(col => Math.Abs(col - center))
                .ToList();

            if (sortedByCenterProximity.Count > 0)
                return sortedByCenterProximity[0];

            // If nothing else use random drops

            return validMoves[_random.Next(validMoves.Count)];
        }
    }    
}
