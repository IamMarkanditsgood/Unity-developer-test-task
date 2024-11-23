using System;

public static class UIEvents 
{
    public static event Action<LevelData> OnHintPressed;
    public static event Action<int> OnLevelButtonPressed;
    public static event Action OnLevelsScreenOpenPressed;
    
    public static void PlayLevel(int levelIndex)
    {
        OnLevelButtonPressed?.Invoke(levelIndex);
    }
    public static void OlenLevelsScreen()
    {
        OnLevelsScreenOpenPressed?.Invoke();
    }
    public static void OpenHint(LevelData levelData)
    {
        OnHintPressed?.Invoke(levelData);
    }
}