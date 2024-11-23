using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private UiManager _uiManager;
    [SerializeField] private GameInitializer _gameManager;
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private GameLevelsJsonManager _gameDataManager;

    [SerializeField] private bool _resetGame;

    private void Start()
    {
        if (_resetGame)
        {
            _gameDataManager.CleanGameSaves();
        }

        InitializeScene();
    }

    private void InitializeScene()
    {       
        InitGameManager();
        InitUI();
        _resourcesManager.Initialize();

        IsFirstRun();
    }

    private void IsFirstRun()
    {
        if (!SaveManager.PlayerPrefs.IsSaved(GameKeys.IsFirstRun))
        {
            SaveManager.PlayerPrefs.SaveInt(GameKeys.IsFirstRun, 1);
            ResourcesManager.Instance.ModifyResource(ResourceTypes.Hints, 10);
        }    
    }

    private void InitGameManager()
    {
        GameLevelsData gameData = _gameDataManager.GetGameData();
        _gameManager.Init(gameData);
    }

    private void InitUI()
    {
        _uiManager.InitUI();
    }    
}