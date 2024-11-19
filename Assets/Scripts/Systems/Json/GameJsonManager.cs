using System;
using UnityEngine;

[Serializable]
public class GameJsonManager
{
    [SerializeField] private TextAsset jsonFile;

    public GameData GetGameData()
    {
       
        if (jsonFile != null)
        {
            GameData gameData = LoadDataFromTextAsset(jsonFile, new GameData());

            if (gameData != null)
            {
                Debug.Log("JSON parsed successfully!");
                return gameData;
            }
            else
            {
                Debug.LogError("Failed to parse the JSON data.");
                return null;
            }
        }
        else
        {
            Debug.LogError($"JSON file '{jsonFile}' not found in Resources.");
            return null;
        }
    }

    private T LoadDataFromTextAsset<T>(TextAsset textAsset, T defaultValue)
    {
        if (textAsset == null || string.IsNullOrWhiteSpace(textAsset.text))
        {
            return defaultValue;
        }

        try
        {
            return JsonUtility.FromJson<T>(textAsset.text);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error parsing JSON: {ex.Message}");
            return defaultValue;
        }
    }
}