using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class SpritesUI 
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
		if (GUILayout.Button ("Load Sprites"))
			GameEditor.LoadSprites();

		GUILayout.Label ("Sprite objects loaded: " + ((GameEditor.spriteObjects == null) ? "0" : GameEditor.spriteObjects.Count().ToString()));

		GUILayout.Label ("Sprite textures loaded: " + ((GameEditor.spriteTextures == null) ? "0" : GameEditor.spriteTextures.Count().ToString()));
		
		GUILayout.Label ("Sprites selected: " + ((GameEditor.spriteSelection == null) ? "0" : GameEditor.spriteSelection.Count().ToString()));
		
		EditorGUILayout.BeginHorizontal();
		
		if(GameEditor.spriteSelection == null || GameEditor.spriteSelection.Count <= 0)
			GUI.enabled = false;
		if(GUILayout.Button ("Clear Selection"))
			GameEditor.spriteSelection = new List<KeyValuePair<string, Texture2D>>();
		GUI.enabled = true;
		
		if(GameEditor.spriteSelection == null || GameEditor.spriteSelection.Count <= 0)
			GUI.enabled = false;
		if(GUILayout.Button ("Open Prefab Wizard"))
		{
		 	var wizard =  new PrefabWizard(GameEditor.spriteSelection);
		 	wizard.Show();
		 	GameEditor.spriteSelection = null;
		 }
		GUI.enabled = true;
		
		EditorGUILayout.EndHorizontal();
	}

	static void ScrollviewUI(float width)
	{
		GUILayout.Label ("", GUILayout.Width (width));

		if(GameEditor.spriteTextures == null)
			return;
		if(GameEditor.spriteTextures.Count <= 0)
			return;
	
		size = EditorGUILayout.BeginScrollView (size);
		int currentWidth = 0;
		
		EditorGUILayout.BeginHorizontal();
		foreach(var spriteKVP in GameEditor.spriteTextures)
		{
			if(currentWidth >= width)
			{
				currentWidth = 0;
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
			}
			
			currentWidth = currentWidth + spriteKVP.Value.width + (2 * buttonPadding);
			
			DisplaySpriteButton(spriteKVP);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView ();
	}
	
	static void DisplaySpriteButton(KeyValuePair<string, Texture2D> spriteKVP)
	{
		if(GameEditor.spriteSelection == null)
			GameEditor.spriteSelection = new List<KeyValuePair<string, Texture2D>>();
	
		if(GameEditor.spriteSelection.Contains(spriteKVP))
			GUI.enabled = false;
	
		if(GUILayout.Button (spriteKVP.Value, GUILayout.Width (spriteKVP.Value.width + buttonPadding), GUILayout.Height (spriteKVP.Value.height + buttonPadding)))
		{
			GameEditor.spriteSelection.Add(spriteKVP);
		}
		
		GUI.enabled = true;
	}
}
