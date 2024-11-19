using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveManager
{
    public static ResourceSaveManager Resources { get; } = new ResourceSaveManager();

    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static void SaveIntList(string key, List<int> list)
    {
        string serializedList = string.Join(",", list);
        PlayerPrefs.SetString(key, serializedList);
        PlayerPrefs.Save();
    }

    public static List<int> LoadIntList(string key)
    {
        string serializedList = PlayerPrefs.GetString(key, string.Empty);
        if (string.IsNullOrEmpty(serializedList))
        {
            return new List<int>();
        }

        return serializedList.Split(',').Select(int.Parse).ToList();
    }

    public static bool IsSaved(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return true;
        }
        return false;
    }

    public static void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void ResetKey(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
        else
        {
            Debug.Log($"Key {key} was not saved");
        }
    }
}