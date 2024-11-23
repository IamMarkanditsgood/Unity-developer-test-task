using System;
using System.Collections.Generic;

public static class GameEvents 
{
    public static event Action<List<LevelData>>  OnWordFound;

    public static void WordFound(List<LevelData> LevelsData)
    {
        OnWordFound?.Invoke(LevelsData);
    }
}