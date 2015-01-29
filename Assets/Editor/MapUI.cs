using UnityEngine;
using UnityEditor;
using System.Collections;

public static class MapUI 
{

	public static void Display(float width)
	{
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		
			EditorGUILayout.BeginVertical();
				MapControlsUI(300);
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginVertical();
				MapStatisticsUI(300);
			EditorGUILayout.EndVertical();
		
		EditorGUILayout.EndHorizontal();
		
	}
	
	static void MapControlsUI(float width)
	{
		EditorGUILayout.Space ();
	
		EditorGUILayout.BeginHorizontal();
		
			if(GameEditor.currentMap != null)
				GUI.enabled = false;
			if(GUILayout.Button ("Create New Map", GUILayout.Width(width / 2f)))
			{
				var wizard = new MapWizard();
				wizard.Show();
			}
			GUI.enabled = true;
			
			if(GameEditor.currentMap == null)
				GUI.enabled = false;
			if(GUILayout.Button ("Save Map", GUILayout.Width(width / 2f)))
			{
				string path = EditorUtility.SaveFilePanel("", "", "", "xml");
				if(path != null || path != "")
					MapSerializer.Save(GameEditor.currentMap, path);
			}
			GUI.enabled = true;
			
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		
			if(GameEditor.currentMap != null)
				GUI.enabled = false;
			if(GUILayout.Button("Load From Scene", GUILayout.Width(width / 2f)))
			{
				Map map = GameObject.FindObjectOfType<Map>();
				
				if(map != null)
				{
					GameEditor.currentMap = map;
					GameEditor.currentFloor = map.GetZeroFloor();
				}
			}
			GUI.enabled = true;
			
			if(GameEditor.currentMap != null)
				GUI.enabled = false;
			if(GUILayout.Button("Load From File", GUILayout.Width(width / 2f)))
			{
				string path = EditorUtility.OpenFilePanel("", "", "xml");
				if(path != null || path != "")
					GameEditor.currentMap = MapSerializer.Load(path);
			}
			GUI.enabled = true;
			
		EditorGUILayout.EndHorizontal();
	}
	
	static void MapStatisticsUI(float width)
	{
		GUILayout.Label ("Current Map: " + ((GameEditor.currentMap == null) ? "NA" : GameEditor.currentMap.name), GUILayout.Width (width));
		GUILayout.Label ("Current Floor: " + ((GameEditor.currentFloor == null) ? "NA" : GameEditor.currentFloor.height.ToString()), GUILayout.Width (width));
		
		if(GameEditor.currentMap != null)
		{			
			GUILayout.Label ("Number of floors: " + GameEditor.currentMap.floors.Count, GUILayout.Width (width));
			GUILayout.Label ("Highest floor: " + ((GameEditor.currentMap.GetHighestFloor() == null) ? "NA" : GameEditor.currentMap.GetHighestFloor().height.ToString()), GUILayout.Width (width));
			GUILayout.Label ("Lowest floor: " + ((GameEditor.currentMap.GetLowestFloor() == null) ? "NA" : GameEditor.currentMap.GetLowestFloor().height.ToString()), GUILayout.Width (width));
		}
	}
}
