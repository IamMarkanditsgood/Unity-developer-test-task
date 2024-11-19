using System.Collections.Generic;
using UnityEngine;

public class ResourceSaveManager
{
    private readonly Dictionary<ResourceTypes, string> ResourceKeys = new Dictionary<ResourceTypes, string>
        {
            { ResourceTypes.Coins, ResourcesKeys.Coins },
        };

    public void SaveResource(ResourceTypes resource, int amount)
    {
        if (ResourceKeys.TryGetValue(resource, out string key))
        {
            SaveManager.SaveInt(key, amount);
        }
        else
        {
            Debug.LogWarning($"Invalid resource type: {resource}");
        }
    }

    public int LoadResource(ResourceTypes resource)
    {
        if (ResourceKeys.TryGetValue(resource, out string key) && SaveManager.IsSaved(key))
        {
            return SaveManager.LoadInt(key, 0);
        }

        Debug.LogWarning($"Resource not found or not saved: {resource}");
        return 0;
    }
}