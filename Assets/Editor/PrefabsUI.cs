using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PrefabsUI 
{

	static Vector2 size = Vector2.zero;
	static int buttonPadding = 12;
	
	static PrefabCategory categoryFilter = PrefabCategory.Any;
	static PrefabColor colorFilter = PrefabColor.Any;
	
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
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		
		GUILayout.Label("Category:", GUILayout.Width (65f));
		
		categoryFilter = (PrefabCategory)EditorGUILayout.EnumPopup(categoryFilter, GUILayout.Width (110f));
		
		if(GUILayout.Button ("Reset",GUILayout.Width (50f)))
			categoryFilter = PrefabCategory.Any;
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		
		GUILayout.Label("Color:", GUILayout.Width (65f));
		
		colorFilter = (PrefabColor)EditorGUILayout.EnumPopup(colorFilter, GUILayout.Width (110f));
		
		if(GUILayout.Button ("Reset",GUILayout.Width (50f)))
			colorFilter = PrefabColor.Any;
		
		EditorGUILayout.EndHorizontal ();
	}
	
	static void ScrollviewUI(float width)
	{
		GUILayout.Label ("", GUILayout.Width (width));

		if(GameEditor.prefabs == null)
			return;
		if(GameEditor.prefabs.Count <= 0)
			return;
		
		size = EditorGUILayout.BeginScrollView (size);
		int currentWidth = 0;
		
		EditorGUILayout.BeginHorizontal();
		foreach(var prefabKVP in GameEditor.prefabs)
		{
			if(currentWidth >= width)
			{
				currentWidth = 0;
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
			}
			
			currentWidth = currentWidth + prefabKVP.Value.spriteWidth + (2 * buttonPadding);
			
			if(!IsFiltered(prefabKVP.Value))
				DisplayPrefabButton(prefabKVP);
		}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.EndScrollView ();
	}
	
	static bool IsFiltered(Prefab prefab)
	{
		if(categoryFilter != PrefabCategory.Any && prefab.prefabCategory != categoryFilter)
			return true;
			
		if(colorFilter != PrefabColor.Any && prefab.prefabColor != colorFilter)
			return true;
			
		return false;
	}
	
	static void DisplayPrefabButton(KeyValuePair<int, Prefab> prefabKVP)
	{
		
		if(GameEditor.currentPrefab == prefabKVP.Value)
			GUI.enabled = false;
			
		Texture2D prefabTexture;
		GameEditor.spriteTextures.TryGetValue(prefabKVP.Value.spriteName, out prefabTexture);
		if(prefabTexture == null)
		{
			//TODO display an error texture
		}
		
		if(GUILayout.Button (prefabTexture, GUILayout.Width (prefabTexture.width + buttonPadding), GUILayout.Height (prefabTexture.height + buttonPadding)))
		{
			GameEditor.currentPrefab = prefabKVP.Value;
		}
		
		GUI.enabled = true;
	}
}
