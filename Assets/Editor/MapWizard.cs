using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MapWizard : EditorWindow
{

	string newMapName;
	
	int highestFloor = 0;
	int lowestFloor = 0;
	
	bool hasError;
	List<string> errorText;
	
	bool hasCompleted;
	string completionText;
	
	static void Init() 
	{
		PrefabWizard prefabWizardWindow = (PrefabWizard)EditorWindow.GetWindow (typeof(PrefabWizard));
	}
	
	public MapWizard()
	{
		this.newMapName = "New Map";
		this.highestFloor = 3;
		this.lowestFloor = -3;
		this.errorText = new List<string>();
	}
	
	void OnGUI()
	{
		EditorGUILayout.Space();
	
		EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Map Name:", GUILayout.Width (100));
			newMapName = EditorGUILayout.TextField(newMapName, GUILayout.Width (150f));
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Highest Floor:", GUILayout.Width (100));
			highestFloor = EditorGUILayout.IntField(highestFloor, GUILayout.Width (150f));
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Lowest Floor:", GUILayout.Width (100));
			lowestFloor = EditorGUILayout.IntField(lowestFloor, GUILayout.Width (150f));
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
		
		for(int i = highestFloor; i >= lowestFloor; i--)
		{
			map.AddNewFloor(i);
		}
		
		hasCompleted = true;
		completionText = "Sucessfuly finished creating new map.";
	}
	
	bool CheckForErrors()
	{
		hasError = false;	
		errorText = new List<string>();
		
		for(int i = highestFloor; i >= lowestFloor; i--)
		{
			string layerName = "Floor " + i.ToString();
			if(LayerMask.NameToLayer(layerName) == -1)
			{
				hasError = true;
				errorText.Add ("Missing Layer for " + layerName);
			}
		}
		
		if(highestFloor <= lowestFloor || lowestFloor >= highestFloor)
		{
			hasError = true;
			errorText.Add("Highest floor must be greater than lowest floor.");
		}
		
		if(newMapName == "" || newMapName == null)
		{
			hasError = true;
			errorText.Add("Invalid map name.");
		}
		
		return hasError;
	}
}
