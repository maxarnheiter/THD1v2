using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class SpritesUI 
{
	static Vector2 size = Vector2.zero;
	static int buttonPadding = 12;
	
	static bool showOnlyUnused;

    static List<KeyValuePair<string, Texture2D>> spriteSelection;

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
		if (GUILayout.Button ("Load Sprites"))
            SpriteManager.LoadSprites();

        GUILayout.Label("Sprite objects loaded: " + ((SpriteManager.spriteObjects == null) ? "0" : SpriteManager.spriteObjects.Count().ToString()));

        GUILayout.Label("Sprite textures loaded: " + ((SpriteManager.spriteTextures == null) ? "0" : SpriteManager.spriteTextures.Count().ToString()));
		
		GUILayout.Label ("Sprites selected: " + ((spriteSelection == null) ? "0" : spriteSelection.Count().ToString()));
		
		EditorGUILayout.BeginHorizontal();
		
		if(spriteSelection == null || spriteSelection.Count <= 0)
			GUI.enabled = false;
		if(GUILayout.Button ("Clear Selection"))
			spriteSelection = new List<KeyValuePair<string, Texture2D>>();
		GUI.enabled = true;
		
		if(spriteSelection == null || spriteSelection.Count <= 0)
			GUI.enabled = false;
		if(GUILayout.Button ("Open Prefab Wizard"))
		{
            SpriteManager.OpenPrefabWizard(spriteSelection);
            spriteSelection.Clear();
		 }
		GUI.enabled = true;
		
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		showOnlyUnused = EditorGUILayout.Toggle("Show Only Unused", showOnlyUnused);
	}

	static void ScrollviewUI(float width)
	{
		GUILayout.Label ("", GUILayout.Width (width));

        if (SpriteManager.spriteTextures == null)
			return;
        if (SpriteManager.spriteTextures.Count <= 0)
			return;
	
		size = EditorGUILayout.BeginScrollView (size);
		int currentWidth = 0;
		
		EditorGUILayout.BeginHorizontal();
        foreach (var spriteKVP in SpriteManager.spriteTextures)
		{
			if(currentWidth >= width)
			{
				currentWidth = 0;
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
			}
			
			currentWidth = currentWidth + spriteKVP.Value.width + (2 * buttonPadding);
			
			if(!IsFiltered(spriteKVP))
				DisplaySpriteButton(spriteKVP);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView ();
	}
	
	static void DisplaySpriteButton(KeyValuePair<string, Texture2D> spriteKVP)
	{
		if(spriteSelection == null)
			spriteSelection = new List<KeyValuePair<string, Texture2D>>();
	
		if(spriteSelection.Contains(spriteKVP))
			GUI.enabled = false;
	
		if(GUILayout.Button (spriteKVP.Value, GUILayout.Width (spriteKVP.Value.width + buttonPadding), GUILayout.Height (spriteKVP.Value.height + buttonPadding)))
		{
			spriteSelection.Add(spriteKVP);
		}
		
		GUI.enabled = true;
	}
	
	static bool IsFiltered(KeyValuePair<string, Texture2D> spriteKVP)
	{
		if(!showOnlyUnused)
			return false;

        if (!PrefabManager.hasPrefabs)
			return false;

        if (PrefabManager.prefabs.Any(p => p.Value.spriteName == spriteKVP.Key))
			return true;
			
		return false;
	}
}
