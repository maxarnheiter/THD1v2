using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public static class PrefabManager
{
    public static Prefab currentPrefab;

    public static bool hasPrefabs;
    static PrefabCollection prefabCollection;
    public static Dictionary<int, Prefab> prefabs
    {
        get
        {
            if (prefabCollection == null)
                return null;
            else
                return prefabCollection.prefabs;
        }
    }

    public static void DisplayUI(float width)
    {
        PrefabsUI.Display(width);
    }

    public static void LoadPrefabs()
    {
        hasPrefabs = false;

        prefabCollection = PrefabLoader.GetPrefabCollection();

        if (prefabCollection != null)
            hasPrefabs = true;
    }
    
}

