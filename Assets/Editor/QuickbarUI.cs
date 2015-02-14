using UnityEngine;
using UnityEditor;
using System.Collections;

public static class QuickbarUI 
{

    static Texture2D _pencilIcon;
    static Texture2D pencilIcon
    { get { return _pencilIcon ?? (_pencilIcon = Resources.Load("EditorSprites/pencil") as Texture2D); } }


    static Texture2D _eraserIcon;
    static Texture2D eraserIcon
    { get { return _eraserIcon ?? (_eraserIcon = Resources.Load("EditorSprites/eraser") as Texture2D); } }


    static Texture2D _upIcon;
    static Texture2D upIcon
    { get { return _upIcon ?? (_upIcon = Resources.Load("EditorSprites/up") as Texture2D); } }


    static Texture2D _downIcon;
    static Texture2D downIcon
    { get { return _downIcon ?? (_downIcon = Resources.Load("EditorSprites/down") as Texture2D); } }

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
		
		if (SceneManager.clickAction == EditorClickAction.None)
			GUI.enabled = false;
		if(GUILayout.Button ("None", GUILayout.Width(50f)))
		{
            SceneManager.clickAction = EditorClickAction.None;
		}
		GUI.enabled = true;

        if (SceneManager.clickAction == EditorClickAction.Erase)
			GUI.enabled = false;
		if (GUILayout.Button (eraserIcon, GUILayout.Width (50f))) 
		{
            SceneManager.clickAction = EditorClickAction.Erase;
		}
		GUI.enabled = true;

        if (SceneManager.clickAction == EditorClickAction.Draw)
			GUI.enabled = false;
		if (GUILayout.Button (pencilIcon, GUILayout.Width (50f))) 
		{
            SceneManager.clickAction = EditorClickAction.Draw;
		}
		GUI.enabled = true;
		
		GUILayout.FlexibleSpace ();
		
		EditorGUILayout.EndHorizontal();
	}

	static void MapFloorsUI(float width)
	{
		EditorGUILayout.BeginHorizontal ();

			GUILayout.FlexibleSpace ();

            if (!MapManager.hasMap)
					GUI.enabled = false;
			if (GUILayout.Button (upIcon, GUILayout.Width (50f))) 
			{
                MapManager.FloorUp();
			}
			GUI.enabled = true;

			GUILayout.FlexibleSpace ();

		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.BeginHorizontal ();
			
			GUILayout.FlexibleSpace ();

            GUILayout.Label("Floor " + ((MapManager.hasMap == true) ? MapManager.currentFloor.ToString() : "NA"));
			
			GUILayout.FlexibleSpace ();
		
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.BeginHorizontal ();
		
			GUILayout.FlexibleSpace ();

            if (!MapManager.hasMap)
				GUI.enabled = false;
			if (GUILayout.Button (downIcon, GUILayout.Width (50f))) 
			{
                MapManager.FloorDown();
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
		
			if(!SceneManager.showAllFloors)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Top Floors", GUILayout.Width (width / 2)))
			{
                SceneManager.showAllFloors = !SceneManager.showAllFloors;
				SceneView.RepaintAll();
			}

            if (SceneManager.showAllFloors)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();

        if (!SceneManager.showGrounds)
				GUILayout.FlexibleSpace();
				
			if(GUILayout.Button ("Grounds", GUILayout.Width (width / 2)))
			{
                SceneManager.showGrounds = !SceneManager.showGrounds;
				SceneView.RepaintAll();
			}

            if (SceneManager.showGrounds)
				GUILayout.FlexibleSpace();
			
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();

        if (!SceneManager.showCorners)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Corners", GUILayout.Width (width / 2)))
			{
                SceneManager.showCorners = !SceneManager.showCorners;
				SceneView.RepaintAll();
			}

            if (SceneManager.showCorners)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();

            if (!SceneManager.showThings)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Things", GUILayout.Width (width / 2)))
			{
                SceneManager.showThings = !SceneManager.showThings;
				SceneView.RepaintAll();
			}

            if (SceneManager.showThings)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();

            if (!SceneManager.showItems)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Items", GUILayout.Width (width / 2)))
			{
                SceneManager.showItems = !SceneManager.showItems;
				SceneView.RepaintAll();
			}

            if (SceneManager.showItems)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();

            if (!SceneManager.showCreatures)
				GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Creatures", GUILayout.Width (width / 2)))
			{
                SceneManager.showCreatures = !SceneManager.showCreatures;
				SceneView.RepaintAll();
			}

            if (SceneManager.showCreatures)
				GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal ();
	}

}
