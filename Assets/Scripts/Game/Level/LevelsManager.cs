using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class LevelsManager
{
    private List<LevelData> _levelsData = new List<LevelData>();

    public List<LevelData> LevelsData => _levelsData;

    public void Subscribe()
    {
        GameEvents.OnWordFound += CalculateLevelsProgress;
    }
    public void Unsubscribe()
    {
        GameEvents.OnWordFound -= CalculateLevelsProgress;
    }
    public void InitLevels(GameConfig gameData)
    {
        SetLevelsData(gameData);
        CalculateLevelsProgress(_levelsData);
        SaveManager.UpdateLevelListSaves(_levelsData);
    }

    public void CalculateLevelsProgress(List<LevelData> LevelsData)
    {
        for (int i = 0; i < LevelsData.Count; i++)
        {
            string levelWord = LevelsData[i].levelWord;
            int openedWords = LevelsData[i].foundWords.Count;
            int totalWords = LevelsData[i].levelWords.Count;

            int percentage = (int)Math.Round(((double)openedWords / totalWords) * 100);

            LevelsData[i].progress = percentage;
        }
        SaveManager.UpdateLevelListSaves(LevelsData);
    }

    private void SetLevelsData(GameConfig gameData)
    {
        
        if (!SaveManager.JsonStorage.Exists(GameKeys.LevelsData))
        {
            for (int i = 0; i < gameData.Levels.Count; i++)
            {
                LevelData levelData = new LevelData();

                levelData.levelWord = gameData.Levels[i];
                levelData.levelWords = GetLevelWords(i, gameData);
                levelData.progress = 0;
                levelData.foundWords = new List<string>();
                levelData.levelTime = 0;

                _levelsData.Add(levelData);
            }
        }
        else
        {
            _levelsData = SaveManager.LoadLevelList();
        }
    }   

    private List<GameWord> GetLevelWords(int levelIndex, GameConfig gameData)
    {
        WordFilter wordFilter = new WordFilter();

        List<GameWord> levelWords = wordFilter.FilterWords(gameData.Levels[levelIndex], gameData.GameWords);

        return levelWords;
    }
}
