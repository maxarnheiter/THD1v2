using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PrefabsUI 
{

	static Vector2 size = Vector2.zero;
	static int buttonPadding = 12;
	
	static PrefabType typeFilter = PrefabType.Any;
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
            PrefabManager.LoadPrefabs();

        GUILayout.Label("Prefab objects loaded: " + ((PrefabManager.prefabs == null) ? "0" : PrefabManager.prefabs.Count().ToString()));
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		
		GUILayout.Label("Type:", GUILayout.Width (65f));
		
		typeFilter = (PrefabType)EditorGUILayout.EnumPopup(typeFilter, GUILayout.Width (110f));
		
		if(GUILayout.Button ("Reset",GUILayout.Width (50f)))
			categoryFilter = PrefabCategory.Any;
		
		EditorGUILayout.EndHorizontal ();
		
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

        if (PrefabManager.prefabs == null)
			return;
        if (PrefabManager.prefabs.Count <= 0)
			return;
		
		size = EditorGUILayout.BeginScrollView (size);

        foreach (var set in PrefabManager.prefabs.GroupBy(p => p.Value.setId))
		{
			DisplaySet (set.OrderBy(p => p.Value.spriteWidth), width);
		}
	
		
		
		EditorGUILayout.EndScrollView ();
	}
	
	static void DisplaySet(IOrderedEnumerable<KeyValuePair<int, Prefab>> set, float width)
	{
		GUILayout.Label("Set ID: " + set.First().Value.setId);
	
		int currentWidth = 0;
	
		EditorGUILayout.BeginHorizontal();
		foreach(var prefabKVP in set)
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
	}
	
	static bool IsFiltered(Prefab prefab)
	{
	
		if(typeFilter != PrefabType.Any && prefab.prefabType != typeFilter)
			return true;
		
		if(categoryFilter != PrefabCategory.Any && prefab.prefabCategory != categoryFilter)
			return true;
			
		if(colorFilter != PrefabColor.Any && prefab.prefabColor != colorFilter)
			return true;
			
		return false;
	}
	
	static void DisplayPrefabButton(KeyValuePair<int, Prefab> prefabKVP)
	{

        if (PrefabManager.currentPrefab == prefabKVP.Value)
			GUI.enabled = false;
			
		Texture2D prefabTexture;
		SpriteManager.spriteTextures.TryGetValue(prefabKVP.Value.spriteName, out prefabTexture);
		if(prefabTexture == null)
		{
			//TODO display an error texture
		}
		
		if(GUILayout.Button (prefabTexture, GUILayout.Width (prefabTexture.width + buttonPadding), GUILayout.Height (prefabTexture.height + buttonPadding)))
		{
            PrefabManager.currentPrefab = prefabKVP.Value;
			
			if(SceneManager.clickAction == EditorClickAction.None)
                SceneManager.clickAction = EditorClickAction.Draw;
		}
		
		GUI.enabled = true;
	}
}
