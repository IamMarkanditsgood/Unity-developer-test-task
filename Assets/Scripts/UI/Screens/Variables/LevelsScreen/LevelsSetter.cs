using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelsSetter
{
    [SerializeField] private GameObject _levelDrawerPref;
    [SerializeField] private GameObject _defaultDrawerPref;
    [SerializeField] private Transform _content;

    [SerializeField] private int _minDrawers;

    private List<GameObject> _drawers = new List<GameObject>();

    public void SetLevels(List<string> levelWords)
    {
        CleanLevels();
        SetLevelDrawers(levelWords);
    }

    private void CleanLevels()
    {
        foreach (var button in _drawers)
        {
            UnityEngine.Object.Destroy(button.gameObject);
        }

        _drawers.Clear();
    }

    private void SetLevelDrawers(List<string> levelWords)
    {
        for (int i = 0; i < levelWords.Count; i++)
        {
            GameObject newDrawer = UnityEngine.Object.Instantiate(_levelDrawerPref, _content);
            _drawers.Add(newDrawer);

            List<int> levelsProgress = GetLevelProgress(levelWords);
            newDrawer.GetComponent<LevelDrawer>().Init(levelWords[i], levelsProgress[i]);
        }

        if(_drawers.Count < _minDrawers)
        {
            int defaultDrawers = _minDrawers - _drawers.Count;

            for (int i = 0; i < defaultDrawers; i++)
            {
                GameObject newDrawer = UnityEngine.Object.Instantiate(_defaultDrawerPref, _content);
                _drawers.Add(newDrawer);
            }
        }
    }

    private List<int> GetLevelProgress(List<string> levelWords)
    {
        List<int> levelsProgress = new List<int>();

        foreach (var level in levelWords)
        {
            int percent = 0;
            if (SaveManager.IsSaved("Progress" + level))
            {
                 percent = SaveManager.LoadInt("Progress" + level);
            }
            levelsProgress.Add(percent);
        }
        return levelsProgress;
    }
}