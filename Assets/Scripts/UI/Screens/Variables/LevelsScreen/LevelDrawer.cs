using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDrawer : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelWordText;
    [SerializeField] private TMP_Text _levelProgressText;
    [SerializeField] private Image _progressMedal;

    [SerializeField] private Sprite[] _medals;
    [SerializeField] private int[] _medalPercents;
    [SerializeField] private int bronzeMedalIndex = 0;

    [SerializeField] private GameObject _levelProgress;
    [SerializeField] private GameObject _doneSign;

    public void Init(string levelWord, int levelProgress)
    { 
        SetText(levelWord, levelProgress);
        SetProgress(levelProgress);
    }

    private void SetText(string levelWord, int levelProgress)
    {
        _levelWordText.text = levelWord;
        _levelProgressText.text = levelProgress + "%";
    }

    private void SetProgress(int levelProgress)
    {
        for (int i = 0; i < _medalPercents.Length; i++)
        {
            if (levelProgress > _medalPercents[i])
            {
                _doneSign.SetActive(false);
                _levelProgress.SetActive(true);

                _progressMedal.sprite = _medals[i];
            } 
        }
        if (levelProgress < _medalPercents[bronzeMedalIndex])
        {
            _doneSign.SetActive(false);
            _levelProgress.SetActive(true);
            _progressMedal.sprite = _medals[bronzeMedalIndex];
        }

        if (levelProgress == 100)
        {
            _doneSign.SetActive(true);
            _levelProgress.SetActive(false);
        }
        
    }
}
