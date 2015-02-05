using UnityEngine;
using UnityEditor;
using System.Collections;

public static class GameEditorUI 
{

	static UISelection selection;

	public static void Display(float width)
	{
		float quickbarUIWidth = 170f;
		float selectionUIWidth = width - quickbarUIWidth;
	
		EditorGUILayout.BeginHorizontal ();
	
			SelectionUI (selectionUIWidth);
		
			GUILayout.FlexibleSpace ();
			
			QuickbarUI.Display (quickbarUIWidth);
		
		EditorGUILayout.EndHorizontal();
	}

	static void SelectionUI(float width)
	{
		EditorGUILayout.BeginVertical();
		
			EditorGUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace();
	
				if (selection == UISelection.Sprites)
					GUI.enabled = false;
				if (GUILayout.Button ("Sprites UI", GUILayout.Width (100f)))
					selection = UISelection.Sprites;
				GUI.enabled = true;
		
				if (selection == UISelection.Prefabs)
					GUI.enabled = false;
				if (GUILayout.Button ("Prefabs UI", GUILayout.Width (100f)))
					selection = UISelection.Prefabs;
				GUI.enabled = true;
				
				if (selection == UISelection.Map)
					GUI.enabled = false;
				if (GUILayout.Button ("Map UI", GUILayout.Width (100f)))
					selection = UISelection.Map;
				GUI.enabled = true;
			
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginHorizontal ();
			
				switch (selection) 
				{
				case UISelection.Sprites:
					SpritesUI.Display(width);
					break;
				case UISelection.Prefabs:
					PrefabsUI.Display(width);
					break;
				case UISelection.Map:
					MapUI.Display(width);
					break;
				}
			
			EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.EndVertical();
	}
}
