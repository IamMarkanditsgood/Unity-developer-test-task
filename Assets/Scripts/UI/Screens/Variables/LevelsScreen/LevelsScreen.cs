using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    }

    public override void Show()
    {
        base.Show();
        _levelData = SaveManager.LoadLevelList();
        SetScreen();
        _levelScreenButtonsManager.Init(_levelsManager.Drawers);
    }

    private void SetScreen()
    {
        _coinsTextManager.SetCoinsText();
        _levelsManager.SetLevels(_levelData);
    }
}
