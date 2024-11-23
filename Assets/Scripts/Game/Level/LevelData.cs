using System;
using System.Collections.Generic;

[Serializable]
public class LevelData 
{
    public string levelWord;
    public int progress;
    public float levelTime;
    public List<GameWord> levelWords;
    public List<string> foundWords;
}