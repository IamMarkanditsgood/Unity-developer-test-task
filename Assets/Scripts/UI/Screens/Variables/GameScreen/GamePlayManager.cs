using System.Collections.Generic;
using System.Linq;

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

        LevelData currentLevel = LevelDataManager.GetCurrentLevelData(levelWord);

        bool isWordInLevelWords = currentLevel.levelWords.Any(levelWordData => levelWordData.Word == typedWordString);
        if (!isWordInLevelWords) return false;

        if (currentLevel.foundWords.Contains(typedWordString))
        {
            return false; 
        }

        LevelDataManager.UpdateFoundedWords(currentLevel, typedWordString);
        return true;
    }
}