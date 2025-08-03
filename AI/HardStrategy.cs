using System;
using System.Collections.Generic;
using System.Linq;


namespace Connect4Game.AI
{
    public class HardStrategy : IStrategy
    {
        private readonly Random _random = new();

        public int GetMove(int[,] boardState)
        {
            var validColumns = new List<int>();

            for (int col = 0; col < boardState.GetLength(1); col++)
            {
                if (boardState[0, col] == 0)
                {
                    validColumns.Add(col);
                }
            }

            if (validColumns.Count == 0)
            {
                throw new InvalidOperationException("No valid moves available");
            }
            return validColumns[_random.Next(validColumns.Count)];
        }
    }
}
