using UnityEngine;
using UnityEditor;
using System.Collections;

public static class QuickbarUI 
{

	public static void Display(float width)
	{
		EditorGUILayout.BeginVertical ();

		GUILayout.Label ("Quickbar Tools");

		ClickActionUI (width);

		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		MapFloorsUI (width);

		EditorGUILayout.EndVertical ();
	}

	static void ClickActionUI(float width)
	{
		EditorGUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		
		if (GameEditor.clickAction == EditorClickAction.None)
			GUI.enabled = false;
		if(GUILayout.Button ("None", GUILayout.Width(50f)))
		{
			GameEditor.clickAction = EditorClickAction.None;
		}
		GUI.enabled = true;
		
		if (GameEditor.clickAction == EditorClickAction.Erase)
			GUI.enabled = false;
		if (GUILayout.Button (EditorIcons.eraserIcon, GUILayout.Width (50f))) 
		{
			GameEditor.clickAction = EditorClickAction.Erase;
		}
		GUI.enabled = true;
		
		if (GameEditor.clickAction == EditorClickAction.Draw)
			GUI.enabled = false;
		if (GUILayout.Button (EditorIcons.pencilIcon, GUILayout.Width (50f))) 
		{
			GameEditor.clickAction = EditorClickAction.Draw;
		}
		GUI.enabled = true;
		
		GUILayout.FlexibleSpace ();
		
		EditorGUILayout.EndHorizontal();
	}

	static void MapFloorsUI(float width)
	{
		EditorGUILayout.BeginHorizontal ();

			GUILayout.FlexibleSpace ();

			if (!GameEditor.hasMap)
					GUI.enabled = false;
			if (GUILayout.Button (EditorIcons.upIcon, GUILayout.Width (50f))) 
			{
				GameEditor.FloorUp();
			}
			GUI.enabled = true;

			GUILayout.FlexibleSpace ();

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
			
			GUILayout.FlexibleSpace ();
			
			GUILayout.Label ("Floor " + ((GameEditor.hasMap == true) ? GameEditor.currentFloor.ToString() : "NA"));
			
			GUILayout.FlexibleSpace ();
		
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		
			GUILayout.FlexibleSpace ();
			
			if (!GameEditor.hasMap)
				GUI.enabled = false;
			if (GUILayout.Button (EditorIcons.downIcon, GUILayout.Width (50f))) 
			{
				GameEditor.FloorDown();
			}
			GUI.enabled = true;
			
			GUILayout.FlexibleSpace ();
		
		EditorGUILayout.EndHorizontal ();

	}

}
