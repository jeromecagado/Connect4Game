using System;
using System.Collections.Generic;
using System.Linq;
using Connect4Game.Logic;

namespace Connect4Game.AI
{
    public class AnalyzeBoard
    {
        private readonly GameLogic _game;

        public AnalyzeBoard(GameLogic game)
        {
            _game = game;
        }
        public bool IsWinningMove(int column, PlayerType player)
        {
            // Shallow copy of board structure
            int[,] clone = (int[,])_game.Board.Clone();
            int row = GetLowestEmptyRow(column, clone);

            if (row == -1) return false; // Column is full, can't play here

            clone[row, column] = (int)player;

            var tempGame = new GameLogic();
            CopyBoard(clone, tempGame.Board);

            return tempGame.CheckForWin(row, column);
        }
        private void CopyBoard(int[,] source, int[,] target)
        {
            for (int row = 0; row < source.GetLength(0); row++)
            {
                for (int col = 0; col < source.GetLength(1); col++)
                {
                    target[row, col] = source[row, col];
                }
            }
        }
        public List<int> GetValidMoves(int[,] board)
        {
            List<int> validMoves = new();
            int columns = board.GetLength(1);

            // Checks the top row to see if any positions are open.
            for (int col = 0; col < columns; col++)
            {
                if (board[0, col] == 0)
                {
                    validMoves.Add(col);
                }
            }
            return validMoves;
        }
        public int GetLowestEmptyRow(int column, int[,] board)
        {
            for (int row = board.GetLength(0) - 1; row >= 0; row--)
            {
                if (board[row, column] == 0)
                {
                    return row;
                }
            }
            return -1; // Column is full
        }
    }
}
