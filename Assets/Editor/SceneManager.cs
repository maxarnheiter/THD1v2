using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public enum SceneClickAction
{
    None,
    Draw,
    Erase
}

public static class SceneManager
{
    static bool _showAllFloors = false;
    public static bool showAllFloors
    {
        get { return _showAllFloors; }
        set
        {
            _showAllFloors = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }

    static bool _showGrounds = true;
    public static bool showGrounds
    {
        get { return _showGrounds; }
        set
        {
            _showGrounds = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }


    static bool _showCorners = true;
    public static bool showCorners
    {
        get { return _showCorners; }
        set
        {
            _showCorners = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }


    static bool _showThings = true;
    public static bool showThings
    {
        get { return _showThings; }
        set
        {
            _showThings = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }

    static bool _showItems = true;
    public static bool showItems
    {
        get { return _showItems; }
        set
        {
            _showItems = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }

    static bool _showCreatures = true;
    public static bool showCreatures
    {
        get { return _showCreatures; }
        set
        {
            _showCreatures = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }


    public static SceneClickAction clickAction;

    static float _floorTransparency = 0.5f;
    public static float floorTransparency
    {
        get { return _floorTransparency; }
        set
        {
            _floorTransparency = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }

    static Color _floorTransparencyColor = Color.blue;
    public static Color floorTransparencyColor
    {
        get { return _floorTransparencyColor; }
        set
        {
            _floorTransparencyColor = value;
            UpdateScene(SceneView.currentDrawingSceneView);
        }
    }

    public static void SubscribeInputHandler()
    {
        SceneView.onSceneGUIDelegate -= SceneInputHandler.OnSceneGUI;
        SceneView.onSceneGUIDelegate += SceneInputHandler.OnSceneGUI;
    }

    public static void UpdateScene(SceneView sceneView)
    {
        SceneRenderer.Render(sceneView);
    }
}

