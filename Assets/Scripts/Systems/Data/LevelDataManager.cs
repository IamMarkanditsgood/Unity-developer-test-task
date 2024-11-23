using System.Collections.Generic;
using UnityEngine;

public static class LevelDataManager
{
    private static List<LevelData> _levels = new List<LevelData>(); 

    public static List<LevelData> Levels => _levels;

    public static LevelData GetCurrentLevelData(string levelWord)
    {
        int levelIndex = GetCurrentLevelIndex(levelWord);

        return _levels[levelIndex];
    }

    public static void UpdateFoundedWords(LevelData newLevel, string foundWord)
    {
        int levelIndex = GetCurrentLevelIndex(newLevel.levelWord);

        if (!newLevel.foundWords.Contains(foundWord))
        {
            newLevel.foundWords.Add(foundWord);
            UpdateLevel(newLevel);
        }   
    }

    public static void UpdateTime(LevelData newLevel, float time)
    {
        int levelIndex = GetCurrentLevelIndex(newLevel.levelWord);

        newLevel.levelTime = time;

        UpdateLevel(newLevel);
    }

    private static void UpdateLevel(LevelData newLevel)
    {
        int levelIndex = GetCurrentLevelIndex(newLevel.levelWord);

        _levels[levelIndex] = newLevel;

        SaveManager.UpdateLevelListSaves(_levels);
    }

    private static int GetCurrentLevelIndex(string currentLevelWord)
    {
         _levels = SaveManager.LoadLevelList();

        for (int i = 0; i < _levels.Count; i++)
        {
            if (_levels[i].levelWord == currentLevelWord)
            {
                return i;
            }
        }
        Debug.LogError("Unknown level"); 
        return -1;
    }
}