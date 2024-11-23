using System.Collections;
using System.Collections.Generic;

public static class SaveManager
{
    public static ResourceSaveManager Resources { get; } = new ResourceSaveManager();
    public static PlayerPrefStorage PlayerPrefs { get; } = new PlayerPrefStorage();
    public static JsonStorage JsonStorage { get; } = new JsonStorage();

    public static void UpdateLevelListSaves(List<LevelData> newData)
    {
        var wrapper = new LevelDataListWrapper { levels = newData };
        JsonStorage.SaveToJson(GameKeys.LevelsData, wrapper);
    }

    public static List<LevelData> LoadLevelList()
    {
        var loadedWrapper = JsonStorage.LoadFromJson<LevelDataListWrapper>("levelsData");
        List<LevelData> levelsData = loadedWrapper?.levels ?? new List<LevelData>();
        return levelsData;
    }
}