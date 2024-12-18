﻿using System.Collections.Generic;

public class WordFilter
{
    public List<GameWord> FilterWords(string levelWord, List<GameWord> words)
    {
        var levelWordCounts = CountLetters(levelWord);

        var validWords = new List<GameWord>();
        var uniqueWords = new HashSet<string>();

        foreach (var word in words)
        {
            var wordCounts = CountLetters(word.Word);

            if (CanFormWord(wordCounts, levelWordCounts) && !uniqueWords.Contains(word.Word))
            {
                validWords.Add(word);
                uniqueWords.Add(word.Word);
            }
        }
        validWords = SortByLetters(validWords);
        return validWords;
    }

    private List<GameWord> SortByLetters(List<GameWord> words)
    {
        words.Sort((gw1, gw2) => gw1.Word.Length.CompareTo(gw2.Word.Length));
        return words;
    }

    private Dictionary<char, int> CountLetters(string word)
    {
        var letterCounts = new Dictionary<char, int>();

        foreach (var letter in word)
        {
            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter]++;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        return letterCounts;
    }

    private bool CanFormWord(Dictionary<char, int> wordCounts, Dictionary<char, int> levelWordCounts)
    {
        foreach (var kvp in wordCounts)
        {
            if (!levelWordCounts.ContainsKey(kvp.Key) || levelWordCounts[kvp.Key] < kvp.Value)
            {
                return false;
            }
        }

        return true;
    }
}