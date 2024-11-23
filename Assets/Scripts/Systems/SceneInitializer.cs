using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private UiManager _uiManager;
    [SerializeField] private GameInitializer _gameManager;
    [SerializeField] private GameDataManager _gameDataManager;
    [SerializeField] private ResourcesManager _resourcesManager;

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

        if (!SaveManager.PlayerPrefs.IsSaved(GameKeys.IsFirstRun))
        {
            FirstRun();
        }
    }

    private void FirstRun()
    {
        SaveManager.PlayerPrefs.SaveInt(GameKeys.IsFirstRun, 1);
        ResourcesManager.Instance.ModifyResource(ResourceTypes.Hints, 10);
    }

    private void InitGameManager()
    {
        GameConfig gameData = _gameDataManager.GetGameData();
        _gameManager.Init(gameData);
    }

    private void InitUI()
    {
        _uiManager.InitUI();
    }    
}
