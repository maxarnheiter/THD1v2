using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public static class MapManager
{

    public static bool hasMap;
    public static Map currentMap;
    public static int currentFloor;

    public static int currentRealFloor
    {
        get { return currentFloor * -1; }
    }

    static MapWizard wizard;

    public static void DisplayUI(float width)
    {
        MapUI.Display(width);
    }

    public static void CreateNewMap()
    {
        if (wizard == null)
        {
            wizard = new MapWizard();
            wizard.Show();
        }
    }

    public static void LoadFromScene()
    {
        hasMap = false;
        Map map = GameObject.FindObjectOfType<Map>();

        if (map != null)
        {
            hasMap = true;
            currentMap = map;
            currentFloor = 0;
        }
    }

    public static void SaveMap()
    {
        string path = EditorUtility.SaveFilePanel("", "", "", "xml");

        if (path != null || path != "")
            MapSerializer.Save(currentMap, path);
    }

    public static void LoadMap()
    {
        string path = EditorUtility.OpenFilePanel("", "", "xml");
        if (path != null || path != "")
        {
            hasMap = false;
            Map map = MapSerializer.Load(path);

            if (map != null)
            {
                hasMap = true;
                currentMap = map;
                currentFloor = 0;
            }
        }
    }

    public static void FloorUp()
    {
        if (currentMap.highestFloor == currentFloor)
            return;

        currentFloor++;

        SceneView.RepaintAll();
    }

    public static void FloorDown()
    {
        if (currentMap.lowestFloor == currentFloor)
            return;

        currentFloor--;

        SceneView.RepaintAll();
    }
}

