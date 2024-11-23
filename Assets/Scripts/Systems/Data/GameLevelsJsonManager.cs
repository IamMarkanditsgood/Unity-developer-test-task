using System;
using System.IO;
using UnityEngine;

[Serializable]
public class GameLevelsJsonManager
{
    [SerializeField] private TextAsset jsonFile;

    public void CleanGameSaves()
    {
        SaveManager.PlayerPrefs.ResetSaves();
        DeleteAllJsonFiles();
    }

    public GameLevelsData GetGameData()
    {
       
        if (jsonFile != null)
        {
            GameLevelsData gameData = LoadDataFromTextAsset(jsonFile, new GameLevelsData());

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

    private static void DeleteAllJsonFiles()
    {
        string SaveFolder = Application.persistentDataPath;
        if (Directory.Exists(SaveFolder))
        {
            string[] jsonFiles = Directory.GetFiles(SaveFolder, "*.json");

            foreach (var file in jsonFiles)
            {
                File.Delete(file);
            }

            Debug.Log("Усі JSON-файли видалено.");
        }
        else
        {
            Debug.LogWarning("Папка для збереження JSON-файлів не знайдена.");
        }
    }
}