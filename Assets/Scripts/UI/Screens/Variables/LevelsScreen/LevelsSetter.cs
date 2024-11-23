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
    private List<LevelData> _levelData = new List<LevelData>();

    public List<GameObject> Drawers => _drawers;

    public void SetLevels(List<LevelData> levelData)
    {
        _levelData = levelData;
        CleanLevels();
        SetLevelDrawers();
    }

    private void CleanLevels()
    {
        foreach (var button in _drawers)
        {
            UnityEngine.Object.Destroy(button.gameObject);
        }

        _drawers.Clear();
    }

    private void SetLevelDrawers()
    {
        for (int i = 0; i < _levelData.Count; i++)
        {
            GameObject newDrawer = UnityEngine.Object.Instantiate(_levelDrawerPref, _content);
            _drawers.Add(newDrawer);

            List<int> levelsProgress = GetLevelProgress();
            newDrawer.GetComponent<LevelDrawer>().Init(_levelData[i].levelWord, levelsProgress[i]);
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

    private List<int> GetLevelProgress()
    {
        List<int> levelsProgress = new List<int>();

        foreach (var levelData in _levelData)
        {
            int percent = levelData.progress;       
            levelsProgress.Add(percent);
        }
        return levelsProgress;
    }
}