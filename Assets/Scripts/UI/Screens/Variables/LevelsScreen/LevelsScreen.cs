using System.Collections.Generic;
using UnityEngine;

public class LevelsScreen : BasicScreen
{
    [SerializeField] private CoinsTextManager _coinsTextManager;
    [SerializeField] private LevelsSetter _levelsManager;

    private LevelScreenButtonsManager _levelScreenButtonsManager = new LevelScreenButtonsManager();
    private List<LevelData> _levelData;

    private void Start()
    {
        _coinsTextManager.Initialize();
    }

    private void OnDestroy()
    {
        _coinsTextManager.Cleanup();
        _levelScreenButtonsManager.Destruct();
    }

    public override void Show()
    {     
        _levelData = SaveManager.LoadLevelList();
        SetScreen();
        _levelScreenButtonsManager.Init(_levelsManager.Drawers);

        base.Show();
    }

    private void SetScreen()
    {
        _coinsTextManager.SetCoinsText();
        _levelsManager.SetLevels(_levelData);
    }
}