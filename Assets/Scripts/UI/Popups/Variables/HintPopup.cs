using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HintPopup : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _categoryText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _okayButton;
    [SerializeField] private Button _useHintButton;
    [SerializeField] private GameObject _descriptionBlur;
    
    private LevelData _currentLevel = new LevelData();
    private GameWord _unKnownWord = new GameWord();
    private void Start()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void Subscribe()
    {
        _closeButton.onClick.AddListener(Close);
        _okayButton.onClick.AddListener(Okay);
        _useHintButton.onClick.AddListener(UseHint);
    }

    private void UnSubscribe()
    {
        _closeButton.onClick.RemoveListener(Close);
        _okayButton.onClick.RemoveListener(Okay);
        _useHintButton.onClick.RemoveListener(UseHint);
    }

    public void Show(LevelData currentLevel)
    {
        _currentLevel = currentLevel;
        SetPopup();
        _view.SetActive(true);
        
    }
    public void Hide()
    {
        
        _view.SetActive(false);
    }

    private void SetPopup()
    {
        _unKnownWord = GetNonFoundedWord();
        int letterAmount = _unKnownWord.Word.Length;
        _descriptionText.text = _unKnownWord.Description;
    }

    private void ResetPopup()
    {
        _okayButton.gameObject.SetActive(false);
        _useHintButton.gameObject.SetActive(true);
        _descriptionBlur.SetActive(true);
        _currentLevel = new LevelData();
        _unKnownWord = new GameWord();
    }

    private void Close()
    {
        Hide();
    }

    private void Okay()
    {
        ResetPopup();
        Hide();
    }

    private void UseHint()
    {
        if (ResourcesManager.Instance.IsEnoughResource(ResourceTypes.Hints, 1))
        {
            ResourcesManager.Instance.ModifyResource(ResourceTypes.Hints, -1);

            _categoryText.text = _unKnownWord.Category;
            _descriptionBlur.SetActive(false);
            _okayButton.gameObject.SetActive(true);
            _useHintButton.gameObject.SetActive(false);
            
        }
    }

    private GameWord GetNonFoundedWord()
    {
        foreach (var gameWord in _currentLevel.levelWords)
        {
            if (!_currentLevel.foundWords.Contains(gameWord.Word))
            {
                return gameWord;
            }
        }
        return null;
    }
}