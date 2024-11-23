using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameScreenButtonsManager
{
    [SerializeField] private Button _hintButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _fullEraseButton;
    [SerializeField] private Button _letterEraseButton;
    [SerializeField] private Button[] _letterButtons;
    [SerializeField] private TMP_Text[] _letters;

    private List<char> _levelWord = new List<char>();
    private List<int> _typedLetters = new List<int>();

    public event Action OnHintPressed;
    public event Action OnFullErase;
    public event Action OnLetterErase;
    public event Action<int> OnLetterPressed;

    public void Subscribe()
    {
        _hintButton.onClick.AddListener(HintPressed);
        _closeButton.onClick.AddListener(Exit);
        _fullEraseButton.onClick.AddListener(FullErase);
        _letterEraseButton.onClick.AddListener(EraseLastLater);
        for(int i = 0; i < _letterButtons.Length; i++)
        {
            int index = i;
            _letterButtons[index].onClick.AddListener(() => TypeLetter(index));
        }
    }

    public void Unsubscribe()
    {
        _hintButton.onClick.RemoveListener(HintPressed);
        _closeButton.onClick.RemoveListener(Exit);
        _fullEraseButton.onClick.RemoveListener(FullErase);
        _letterEraseButton.onClick.RemoveListener(EraseLastLater);
        for (int i = 0; i < _letterButtons.Length; i++)
        {
            int index = i;
            _letterButtons[index].onClick.RemoveListener(() => TypeLetter(index));
        }
    }

    public void SetButtons(List<char> levelWord)
    {
        _levelWord = levelWord;

        for (int i = 0; i < _levelWord.Count; i++)
        {
            _letterButtons[i].interactable = true;

            _letters[i].enabled = true;
            _letters[i].text = _levelWord[i].ToString();
        }
    }

    public void ResetButtons()
    {
        
        for (int i = 0; i < _levelWord.Count; i++)
        {
            _letterButtons[i].interactable = true;
            _letters[i].enabled = true;
        }
    }

    public void CleanScene()
    {
        _levelWord = new List<char>();
        _typedLetters = new List<int>();
        foreach (var letter in _letters)
        {
            letter.enabled = false;
        }
        foreach (var button in _letterButtons)
        {
            button.interactable = false;
        }
    }

    private void Exit()
    {
        UIEvents.OlenLevelsScreen();
    }

    private void FullErase()
    {
        
        ResetButtons();
        _typedLetters = new List<int>();
        OnFullErase?.Invoke();
    }

    private void EraseLastLater()
    {
        _letterButtons[_typedLetters[^1]].interactable = true;
        _letters[_typedLetters[^1]].enabled = true;

        _typedLetters.RemoveAt(_typedLetters.Count - 1);

        OnLetterErase?.Invoke();
    }

    private void TypeLetter(int letterIndex)
    {
        _letterButtons[letterIndex].interactable = false;
        _letters[letterIndex].enabled = false;

        _typedLetters.Add(letterIndex);

        OnLetterPressed?.Invoke(letterIndex);
    }

    private void HintPressed()
    {
        OnHintPressed?.Invoke();
    }
}
