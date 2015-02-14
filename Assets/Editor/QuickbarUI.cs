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

		ClickActionUI (width);

		EditorGUILayout.Space ();

		MapFloorsUI (width);

        EditorGUILayout.Space();

        FloorTransparencyUI(width);

		EditorGUILayout.EndVertical ();
	}

	static void ClickActionUI(float width)
	{
		EditorGUILayout.BeginHorizontal ();
		
		GUILayout.FlexibleSpace ();
		
		if (SceneManager.clickAction == SceneClickAction.None)
			GUI.enabled = false;
		if(GUILayout.Button ("None", GUILayout.Width(50f)))
		{
            SceneManager.clickAction = SceneClickAction.None;
		}
		GUI.enabled = true;

        if (SceneManager.clickAction == SceneClickAction.Erase)
			GUI.enabled = false;
		if (GUILayout.Button (eraserIcon, GUILayout.Width (50f))) 
		{
            SceneManager.clickAction = SceneClickAction.Erase;
		}
		GUI.enabled = true;

        if (SceneManager.clickAction == SceneClickAction.Draw)
			GUI.enabled = false;
		if (GUILayout.Button (pencilIcon, GUILayout.Width (50f))) 
		{
            SceneManager.clickAction = SceneClickAction.Draw;
		}
		GUI.enabled = true;
		
		GUILayout.FlexibleSpace ();
		
		EditorGUILayout.EndHorizontal();
	}

	static void MapFloorsUI(float width)
	{
        EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
                FloorControlUI(width);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
                MapVisibilityUI(width);
            EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

	}

    static void FloorControlUI(float width)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (!MapManager.hasMap)
            GUI.enabled = false;
        if (GUILayout.Button(upIcon, GUILayout.Width(50f)))
        {
            MapManager.FloorUp();
        }
        GUI.enabled = true;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Floor " + ((MapManager.hasMap == true) ? MapManager.currentFloor.ToString() : "NA"));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (!MapManager.hasMap)
            GUI.enabled = false;
        if (GUILayout.Button(downIcon, GUILayout.Width(50f)))
        {
            MapManager.FloorDown();
        }
        GUI.enabled = true;
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
	
	static void MapVisibilityUI(float width)
	{
        if(GUILayout.Button("Top Floors: " + ((SceneManager.showAllFloors == true) ? "TRUE" : "FALSE")))
            SceneManager.showAllFloors = !SceneManager.showAllFloors;

        if (GUILayout.Button("Grounds: " + ((SceneManager.showGrounds == true) ? "TRUE" : "FALSE")))
            SceneManager.showGrounds = !SceneManager.showGrounds;

        if (GUILayout.Button("Corners: " + ((SceneManager.showCorners == true) ? "TRUE" : "FALSE")))
            SceneManager.showCorners = !SceneManager.showCorners;

        if (GUILayout.Button("Items: " + ((SceneManager.showItems == true) ? "TRUE" : "FALSE")))
            SceneManager.showItems = !SceneManager.showItems;

        if (GUILayout.Button("Things: " + ((SceneManager.showThings == true) ? "TRUE" : "FALSE")))
            SceneManager.showThings = !SceneManager.showThings;

        if (GUILayout.Button("Creatures: " + ((SceneManager.showCreatures == true) ? "TRUE" : "FALSE")))
            SceneManager.showCreatures = !SceneManager.showCreatures;
	}

    static void FloorTransparencyUI(float width)
    {

        EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

                SceneManager.floorTransparencyColor = EditorGUILayout.ColorField(SceneManager.floorTransparencyColor, GUILayout.Width(30f));

                SceneManager.floorTransparency = EditorGUILayout.Slider(SceneManager.floorTransparency, 0f, 1f, GUILayout.Width(width - 30f));

            EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

}
