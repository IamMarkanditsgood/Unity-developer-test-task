using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameLevelsData _gameData = new GameLevelsData();
    private LevelsManager _levelsManager = new LevelsManager();

    private void Start()
    {
        _levelsManager.Subscribe();
    }
    private void OnDestroy()
    {
        _levelsManager.Unsubscribe();
    }

    public void Init(GameLevelsData gameData)
    {
        _gameData = gameData;
        _levelsManager.InitLevels(_gameData);
    }
}