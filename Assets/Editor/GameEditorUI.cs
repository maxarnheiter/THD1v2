﻿using UnityEngine;
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
		
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal ();
	}

	static void SelectionSpecifcUI(float width)
	{
		switch (selection) 
		{
			case UISelection.Sprites:
				SpritesUI.Display(width);
			break;
			case UISelection.Prefabs:
				PrefabsUI.Display(width);
			break;
		}
	}
}
