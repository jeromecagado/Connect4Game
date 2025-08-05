using System;
using System.Collections.Generic;
using System.Linq;
using Connect4Game.Logic;

namespace Connect4Game.AI
{
    public class AnalyzeBoard
    {
        public AnalyzeBoard() { }

        public bool IsWinningMove(int[,] board, int column, PlayerType player)
        {
            // Shallow copy of board structure
            int[,] clone = (int[,])board.Clone();
            int row = GetLowestEmptyRow(column, clone);

            if (row == -1) return false; // Column is full

            clone[row, column] = (int)player;

            return CheckForWin(clone, row, column, (int)player);
        }

        public List<int> GetValidMoves(int[,] board)
        {
            List<int> validMoves = new();
            int columns = board.GetLength(1);

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
            return -1;
        }

        private bool CheckForWin(int[,] board, int row, int col, int player)
        {
            return CheckDirection(board, row, col, player, 0, 1) ||  // Horizontal
                   CheckDirection(board, row, col, player, 1, 0) ||  // Vertical
                   CheckDirection(board, row, col, player, 1, 1) ||  // Diagonal /
                   CheckDirection(board, row, col, player, -1, 1);   // Diagonal \
        }

        private bool CheckDirection(int[,] board, int row, int col, int player, int dRow, int dCol)
        {
            int count = 1;

            int r = row + dRow, c = col + dCol;
            while (IsInBounds(board, r, c) && board[r, c] == player)
            {
                count++;
                r += dRow;
                c += dCol;
            }

            r = row - dRow; c = col - dCol;
            while (IsInBounds(board, r, c) && board[r, c] == player)
            {
                count++;
                r -= dRow;
                c -= dCol;
            }

            return count >= 4;
        }

        private bool IsInBounds(int[,] board, int row, int col)
        {
            return row >= 0 && row < board.GetLength(0) &&
                   col >= 0 && col < board.GetLength(1);
        }
    }
}