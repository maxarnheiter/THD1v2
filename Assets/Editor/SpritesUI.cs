using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public static class SpritesUI 
{
	static Vector2 size = Vector2.zero;

	public static void Display(float width)
	{
		var sidebarWidth = 250;
		var scrollviewWidth = width - sidebarWidth;

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
			GameEditor.sprites = SpriteLoader.LoadSprites ();

		GUILayout.Label ("Sprites loaded: " + ((GameEditor.sprites == null) ? "0" : GameEditor.sprites.Count().ToString()));
	}

	static void ScrollviewUI(float width)
	{
		size = EditorGUILayout.BeginScrollView (size);

		EditorGUILayout.EndScrollView ();
	}
}
