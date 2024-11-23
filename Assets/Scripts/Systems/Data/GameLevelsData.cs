using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class GameLevelsData
{
    public List<string> Levels;      
    public List<GameWord> GameWords; 
}

[Serializable]
public class GameWord
{
    public string Word;        
    public string Description; 
    public string Category;    
}
[Serializable]
public class LevelDataListWrapper
{
    public List<LevelData> levels;
}