using UnityEngine;
using UnityEditor;
using System.Collections;

public static class GameEditorUI 
{

	static UISelection selection;

	public static void Display(float width)
	{
		SelectionUI ();
		
		EditorGUILayout.Space();
		
		SelectionSpecifcUI (width);
	}

	static void SelectionUI()
	{
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
	}

	static void SelectionSpecifcUI(float width)
	{
		EditorGUILayout.BeginHorizontal ();

			var quickbarWidth = 200f;
			var selectionWidth = width - quickbarWidth;

			switch (selection) 
			{
				case UISelection.Sprites:
					SpritesUI.Display(selectionWidth);
				break;
				case UISelection.Prefabs:
					PrefabsUI.Display(selectionWidth);
				break;
				case UISelection.Map:
					MapUI.Display(selectionWidth);
				break;
			}

			GUILayout.FlexibleSpace ();

			QuickbarUI.Display (quickbarWidth);

		EditorGUILayout.EndHorizontal ();
	}
}
