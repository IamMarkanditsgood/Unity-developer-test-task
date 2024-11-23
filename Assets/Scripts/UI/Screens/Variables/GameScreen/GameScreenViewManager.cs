using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class GameScreenViewManager
{
    [SerializeField] private TMP_Text _hintsText;
    [SerializeField] private TMP_Text _levelWordText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _typedWordText;
    [SerializeField] private TMP_Text _foundWordsAmountText;
    [SerializeField] private Transform _wordPoolContent;
    [SerializeField] private GameObject _wordPrefab;

    private List<GameObject> _words = new List<GameObject>();
    public void CleanScene()
    {
        foreach (var word in _words)
        {
            Object.Destroy(word);
        }
        _words.Clear();
    }

    public void SetTypedWords(LevelData currentLevel)
    {
        CleanScene();
        List<string> foundWords = currentLevel.foundWords;
       
        for (int i = 0; i < currentLevel.levelWords.Count; i++)
        {
            GameObject newWord = Object.Instantiate(_wordPrefab, _wordPoolContent);
            _words.Add(newWord);

            int letterAmount = currentLevel.levelWords[i].Word.Length;
            newWord.GetComponent<TMP_Text>().text = new string('-', letterAmount);


            for (int j =0; j < foundWords.Count; j++)
            {
                if (currentLevel.levelWords[i].Word == foundWords[j])
                {
                    newWord.GetComponent<TMP_Text>().text = foundWords[j].ToString();
                }
            }
        }
    }

    public void SetText(int hints, LevelData currentLevel)
    {       
        _hintsText.text = hints.ToString();
        _levelWordText.text = currentLevel.levelWord;
        _typedWordText.text = "";
        _foundWordsAmountText.text = currentLevel.foundWords.Count + "/" + currentLevel.levelWords.Count;
    }
    public void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        _timerText.text = $"{minutes:00}:{seconds:00}";

    }

    public void EraseTypedWord()
    {
        _typedWordText.text = "";
    }

    public void EraseLastLetter()
    {
        List<char> typedLetters = _typedWordText.text.ToList();

        if (typedLetters.Count > 0) 
        {
            typedLetters.RemoveAt(typedLetters.Count - 1);
            _typedWordText.text = new string(typedLetters.ToArray());
        }    
    }

    public void AddTypedLetter(char letter)
    {
        _typedWordText.text += letter;
    }

    public void ModifyHintsText(ResourceTypes resource, int newAmount)
    {
        switch (resource)
        {
            case ResourceTypes.Hints:
                _hintsText.text = newAmount.ToString();
                break;
        }
    }
}