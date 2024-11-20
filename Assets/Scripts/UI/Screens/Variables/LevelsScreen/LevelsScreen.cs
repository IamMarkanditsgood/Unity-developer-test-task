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

    private List<string> _levelWords;

    private void OnDestroy()
    {
        _coinsTextManager.Cleanup();
    }

    public void Init(List<string> levelWords) 
    {
        _levelWords = levelWords;
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
        _levelsManager.SetLevels(_levelWords);
    }
}