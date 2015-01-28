using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PrefabsUI 
{
	static Vector2 size = Vector2.zero;
	static int buttonPadding = 12;
	
	public static void Display(float width)
	{
		var sidebarWidth = 250;
		var scrollviewWidth = width - sidebarWidth - 10;
		
		EditorGUILayout.BeginHorizontal ();
		
		EditorGUILayout.BeginVertical (GUILayout.Width (sidebarWidth));
		SidebarUI (sidebarWidth);
		EditorGUILayout.EndVertical ();
		
		EditorGUILayout.BeginVertical (GUILayout.Width (scrollviewWidth));
		ScrollviewUI (scrollviewWidth);
		EditorGUILayout.EndVertical ();
		
		EditorGUILayout.EndHorizontal ();
	}
	
	static void SidebarUI(float width)
	{
		if (GUILayout.Button ("Load Prefabs"))
			GameEditor.LoadPrefabs();
		
		GUILayout.Label ("Prefab objects loaded: " + ((GameEditor.prefabs == null) ? "0" : GameEditor.prefabs.Count().ToString()));
		
	}
	
	static void ScrollviewUI(float width)
	{
	}
}
