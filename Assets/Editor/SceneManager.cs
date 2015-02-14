using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public enum EditorClickAction
{
    None,
    Draw,
    Erase
}

public static class SceneManager
{

    public static bool showAllFloors = false;
    public static bool showGrounds = true;
    public static bool showCorners = true;
    public static bool showThings = true;
    public static bool showItems = true;
    public static bool showCreatures = true;

    public static EditorClickAction clickAction;


    public static void SubscribeInputHandler()
    {
        SceneView.onSceneGUIDelegate -= SceneInputHandler.OnSceneGUI;
        SceneView.onSceneGUIDelegate += SceneInputHandler.OnSceneGUI;
    }
}

