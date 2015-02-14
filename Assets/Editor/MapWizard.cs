using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MapWizard : EditorWindow
{

	string newMapName;
	
	bool hasError;
	List<string> errorText;
	
	bool hasCompleted;
	string completionText;
	
	static void Init() 
	{
		SpriteWizard prefabWizardWindow = (SpriteWizard)EditorWindow.GetWindow (typeof(SpriteWizard));
	}
	
	public MapWizard()
	{
		this.newMapName = "New Map";
		this.errorText = new List<string>();
	}
	
	void OnGUI()
	{
		EditorGUILayout.Space();
	
		EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Map Name:", GUILayout.Width (100));
			newMapName = EditorGUILayout.TextField(newMapName, GUILayout.Width (150f));
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		if(hasError)
		{
			EditorGUILayout.BeginVertical();
			
			GUIStyle style = new GUIStyle(EditorStyles.label);
			style.normal.textColor = Color.red;
			
			foreach(var error in errorText)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
					GUILayout.Label(error, style);
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.EndVertical();
		}
		
		if(hasCompleted)
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
			GUIStyle style = new GUIStyle(EditorStyles.label);
			style.normal.textColor = Color.blue;
			GUILayout.Label(completionText, style);
			
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}
		
		
		EditorGUILayout.Space ();
		
		EditorGUILayout.BeginHorizontal();
		
			GUILayout.FlexibleSpace();
			
			if(GUILayout.Button ("Make", GUILayout.Width (50f)))
				Make ();
			
			if(GUILayout.Button("Close", GUILayout.Width(50f)))
				this.Close();
		
		EditorGUILayout.EndHorizontal();
	}
	
	void Make()
	{
		if(CheckForErrors())
			return;
		
		var newObject = new GameObject();
		Map map = newObject.AddComponent<Map>();
		
		map.name = newMapName;
		newObject.name = newMapName;
		
		hasCompleted = true;
        completionText = "Sucessfuly finished creating new map.";

        if (MapManager.currentMap == null)
            MapManager.LoadFromScene();
	}
	
	bool CheckForErrors()
	{
		hasError = false;	
		errorText = new List<string>();
		
		if(newMapName == "" || newMapName == null)
		{
			hasError = true;
			errorText.Add("Invalid map name.");
		}
		
		return hasError;
	}
}
