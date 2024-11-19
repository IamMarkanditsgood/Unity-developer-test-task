using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LevelsScreen : BasicScreen
{
    [SerializeField] private CoinsTextManager _coinsTextManager;
    [SerializeField] private LevelsManager _levelsManager;
    private void OnDestroy()
    {
        _coinsTextManager.Cleanup();
    }

    public override void Init()
    {
        _coinsTextManager.Initialize();
    }

    public override void Show()
    {
        base.Show();
        SetScreen();
    }

    private void SetScreen()
    {
        _coinsTextManager.SetCoinsText();
        _levelsManager.SetLevels();
    }
}

[Serializable]
public class LevelsManager
{
    private List<Button> _levelButtons;
    public void SetLevels()
    {
        CleanLevels();

    }

    private void CleanLevels()
    {
        foreach (var button in _levelButtons)
        {
            UnityEngine.Object.Destroy(button.gameObject);
        }

        _levelButtons.Clear();
    }
}