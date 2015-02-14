using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameEditorWindow : EditorWindow 
{
    enum UISelection
    {
        Sprites,
        Prefabs,
        Map
    }

    UISelection selection;

	[MenuItem ("THD/Game Editor")]
	static void Init() 
	{
		GameEditorWindow mapEditorWindow = (GameEditorWindow)EditorWindow.GetWindow (typeof(GameEditorWindow));
	}
	
	void OnEnable() 
	{
		this.title = "Game Editor";
		SceneManager.SubscribeInputHandler();
	}
	
	void OnGUI()
	{
		Display(this.position.width);
	}

    void Display(float width)
    {
        float quickbarUIWidth = 170f;
        float selectionUIWidth = width - quickbarUIWidth;

        EditorGUILayout.BeginHorizontal();

        SelectionUI(selectionUIWidth);

        GUILayout.FlexibleSpace();

        QuickbarUI.Display(quickbarUIWidth);

        EditorGUILayout.EndHorizontal();
    }

    void SelectionUI(float width)
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (selection == UISelection.Sprites)
            GUI.enabled = false;
        if (GUILayout.Button("Sprites UI", GUILayout.Width(100f)))
            selection = UISelection.Sprites;
        GUI.enabled = true;

        if (selection == UISelection.Prefabs)
            GUI.enabled = false;
        if (GUILayout.Button("Prefabs UI", GUILayout.Width(100f)))
            selection = UISelection.Prefabs;
        GUI.enabled = true;

        if (selection == UISelection.Map)
            GUI.enabled = false;
        if (GUILayout.Button("Map UI", GUILayout.Width(100f)))
            selection = UISelection.Map;
        GUI.enabled = true;

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        switch (selection)
        {
            case UISelection.Sprites:
                SpriteManager.DisplayUI(width);
                break;
            case UISelection.Prefabs:
                PrefabManager.DisplayUI(width);
                break;
            case UISelection.Map:
                MapManager.DisplayUI(width);
                break;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }
}
