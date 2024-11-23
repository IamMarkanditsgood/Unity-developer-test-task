using System.Collections.Generic;

using System.Linq;
using TMPro;
using UnityEngine;
using System;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;




public class GamePlayManager
{
    private List<char> _typedWord = new List<char>();

    public void Clean()
    {
        EraseWord();
    }
    public void TypeLetter(char letter)
    {
        _typedWord.Add(letter);
    }

    public void EraseWord()
    {
        _typedWord = new List<char>();
    }
    public void EraseLastLetter()
    {
        _typedWord.RemoveAt(_typedWord.Count - 1);
    }

    public bool IsWordPresent(string levelWord)
    {
        string typedWordString = new string(_typedWord.ToArray());
        Debug.Log("TypedWord = "+ typedWordString);
        List<LevelData> levels = SaveManager.LoadLevelList();

        LevelData currentLevel = levels.FirstOrDefault(level => level.levelWord == levelWord);
        if (currentLevel == null) return false;


        bool isWordInLevelWords = currentLevel.levelWords.Any(levelWordData => levelWordData.Word == typedWordString);
        if (!isWordInLevelWords) return false;

        if (currentLevel.foundWords.Contains(typedWordString))
        {

            return false; 
        }

        currentLevel.foundWords.Add(typedWordString);
        SaveManager.UpdateLevelListSaves(levels);
        
        return true;
    }
}
