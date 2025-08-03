using System;
using System.Collections.Generic;
using System.Linq;


namespace Connect4Game.Logic
{
    public class GameLogic
    {
        // Tracks the current game state: 0 = empty, 1 = player 1, 2 = player 2
        private readonly int[,] board = new int[6, 7];
        // Keeps track of who's turn it is: 1 or 2
        private int currentPlayer = 1;
        public int CurrentPlayer => currentPlayer;
        public int[,] Board => board;

        public void ResetGame()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = 0;
                }
            }
            currentPlayer = 1;
        }
        public bool IsBoardFull()
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[0, col] == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckForWin(int row, int column)
        {
            int player = board[row, column];
            return CheckHorizontal(row, column, player) ||
                   CheckVertical(row, column, player) ||
                   CheckDiagonalDownRight(row, column, player) ||
                   CheckDiagonalUpRight(row, column, player);
        }
        private bool CheckHorizontal(int row, int col, int player)
        {
            int count = 1;

            // Check left
            int c = col - 1;
            while (c >= 0 && board[row, c] == player)
            {
                count++;
                c--;
            }

            // Check right
            c = col + 1;
            while (c < board.GetLength(1) && board[row, c] == player)
            {
                count++;
                c++;
            }

            return count >= 4;
        }

        private bool CheckVertical(int row, int col, int player)
        {
            int count = 1;

            // Check down (most common since discs fall from the top)
            int r = row + 1;
            while (r < board.GetLength(0) && board[r, col] == player)
            {
                count++;
                r++;
            }

            // Check up (rare, but just in case)
            r = row - 1;
            while (r >= 0 && board[r, col] == player)
            {
                count++;
                r--;
            }

            return count >= 4;
        }
        private bool CheckDiagonalDownRight(int row, int col, int player)
        {
            int count = 1;

            // Down-right
            int r = row + 1, c = col + 1;
            while (r < board.GetLength(0) && c < board.GetLength(1) && board[r, c] == player)
            {
                count++;
                r++; c++;
            }

            // Up-left
            r = row - 1; c = col - 1;
            while (r >= 0 && c >= 0 && board[r, c] == player)
            {
                count++;
                r--; c--;
            }

            return count >= 4;
        }
        private bool CheckDiagonalUpRight(int row, int col, int player)
        {
            int count = 1;

            // Down-left
            int r = row + 1, c = col - 1;
            while (r < board.GetLength(0) && c >= 0 && board[r, c] == player)
            {
                count++;
                r++; c--;
            }

            // Up-right
            r = row - 1; c = col + 1;
            while (r >= 0 && c < board.GetLength(1) && board[r, c] == player)
            {
                count++;
                r--; c++;
            }

            return count >= 4;
        }

        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;
        }
    }
}
