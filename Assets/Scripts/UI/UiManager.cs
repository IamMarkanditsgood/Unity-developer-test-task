using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private BasicScreen[] _screens;

    private List<string> _levelWords = new List<string>();  
    private List<LevelsData> _levelsData = new List<LevelsData>();

    public void InitUI(List<string> levels, List<LevelsData> levelsData)
    {
        _levelWords = levels;
        _levelsData = levelsData;

        foreach (var screen in _screens)
        {
            screen.Init();
        }
    }
}