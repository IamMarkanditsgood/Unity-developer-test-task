using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents 
{
    public static event Action<List<LevelData>> OnLevelDataUpdated;
    public static event Action<List<LevelData>>  OnWordFound;

    public static void UpdateLevelData(List<LevelData> newData)
    {
        OnLevelDataUpdated?.Invoke(newData);
    }
    public static void WordFound(List<LevelData> LevelsData)
    {
        OnWordFound?.Invoke(LevelsData);
    }
}
