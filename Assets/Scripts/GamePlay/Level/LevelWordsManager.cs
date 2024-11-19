using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelWordsManager
{
    [SerializeField] private List<LevelsData> _levelsData;

    public List<LevelsData> LevelsData => _levelsData;

    public void InitLevels(GameData gameData)
    {
        for (int i = 0; i < gameData.Levels.Count; i++)
        {
            LevelsData levelData = new LevelsData();

            levelData.levelWord = gameData.Levels[i];
            levelData.levelWords = GetLevelWords(i, gameData);

            _levelsData.Add(levelData);
        }
    }

    private List<GameWord> GetLevelWords(int levelIndex, GameData gameData)
    {
        WordFilter wordFilter = new WordFilter();

        List<GameWord> levelWords = wordFilter.FilterWords(gameData.Levels[levelIndex], gameData.GameWords);

        return levelWords;
    }
}
