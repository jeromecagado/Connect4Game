using System;
using System.Collections.Generic;
using System.Linq;


namespace Connect4Game.AI
{
    public interface IStrategy
    {
        int GetMove(int[,] boardState);
    }
}
