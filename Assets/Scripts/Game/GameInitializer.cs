using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameConfig _gameData = new GameConfig();
    private LevelsManager _levelsManager = new LevelsManager();

    private void Start()
    {
        _levelsManager.Subscribe();
    }
    private void OnDestroy()
    {
        _levelsManager.Unsubscribe();
    }

    public void Init(GameConfig gameData)
    {
        _gameData = gameData;
        _levelsManager.InitLevels(_gameData);
    }
}