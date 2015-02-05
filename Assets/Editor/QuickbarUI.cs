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
		
		EditorGUILayout.Space ();
		
		MapVisibilityUI(width);

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
	
	static void MapVisibilityUI(float width)
	{
		EditorGUILayout.Space();
						
		EditorGUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace();
				GUILayout.Label("Show");
			GUILayout.FlexibleSpace();
				GUILayout.Label("Hide");
			GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		
			if(!GameEditor.showAllFloors)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Top Floors", GUILayout.Width (width / 2)))
			{
				GameEditor.showAllFloors =! GameEditor.showAllFloors;
				SceneView.RepaintAll();
			}
			
			if(GameEditor.showAllFloors)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		
			if(!GameEditor.showGrounds)
				GUILayout.FlexibleSpace();
				
			if(GUILayout.Button ("Grounds", GUILayout.Width (width / 2)))
			{
				GameEditor.showGrounds =! GameEditor.showGrounds;
				SceneView.RepaintAll();
			}
				
			if(GameEditor.showGrounds)
				GUILayout.FlexibleSpace();
			
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		
			if(!GameEditor.showCorners)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Corners", GUILayout.Width (width / 2)))
			{
				GameEditor.showCorners =! GameEditor.showCorners;
				SceneView.RepaintAll();
			}
			
			if(GameEditor.showCorners)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		
			if(!GameEditor.showThings)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Things", GUILayout.Width (width / 2)))
			{
				GameEditor.showThings =! GameEditor.showThings;
				SceneView.RepaintAll();
			}
			
			if(GameEditor.showThings)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		
			if(!GameEditor.showItems)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Items", GUILayout.Width (width / 2)))
			{
				GameEditor.showItems =! GameEditor.showItems;
				SceneView.RepaintAll();
			}
			
			if(GameEditor.showItems)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		
			if(!GameEditor.showCreatures)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Creatures", GUILayout.Width (width / 2)))
			{
				GameEditor.showCreatures =! GameEditor.showCreatures;
				SceneView.RepaintAll();
			}
			
			if(GameEditor.showCreatures)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
	}

}
