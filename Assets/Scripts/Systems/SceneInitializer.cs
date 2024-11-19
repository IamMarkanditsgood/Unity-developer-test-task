using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private UiManager _uiManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameJsonManager _gameJsonManager;

    private void Start()
    {
        InitializeScene();
    }

    private void InitializeScene()
    {
        InitGameManager();
        InitUI();
    }

    private void InitUI()
    {
        List<string> levels = _gameManager.GetLevels();
        List<LevelsData> levelsData = _gameManager.GetLevelsData();
        
        _uiManager.InitUI(levels, levelsData);
    }

    private void InitGameManager()
    {
        GameData gameData = _gameJsonManager.GetGameData();
        _gameManager.Init(gameData);
    }
}
