using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private LevelWordsManager _levelWordsManager;

    public void Init(GameData gameData)
    {
        _gameData = gameData;
        _levelWordsManager.InitLevels(_gameData);
    }
    
    public List<string> GetLevels()
    {
        return _gameData.Levels;
    }

    public List<LevelsData> GetLevelsData()
    {
        return _levelWordsManager.LevelsData;
    }
}