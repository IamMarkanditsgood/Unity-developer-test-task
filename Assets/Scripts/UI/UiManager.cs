using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private BasicScreen[] _screens;

    [Header("Initializable Screens")]
    [SerializeField] private LevelsScreen _levelsScreen;

    private List<string> _levelWords = new List<string>();  
    private List<LevelsData> _levelsData = new List<LevelsData>();

    public void InitUI(List<string> levels, List<LevelsData> levelsData)
    {
        _levelWords = levels;
        _levelsData = levelsData;

        InitializeScreens();
    }

    private void InitializeScreens()
    {
        _levelsScreen.Init(_levelWords);

        _levelsScreen.Show();
        //TO DO
    }
}