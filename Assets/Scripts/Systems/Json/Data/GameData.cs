using UnityEngine;
using System.Collections.Generic;
using System.IO;


[System.Serializable]
public class GameData
{
    public List<string> Levels;      
    public List<GameWord> GameWords; 
}

[System.Serializable]
public class GameWord
{
    public string Word;        
    public string Description; 
    public string Category;    
}