using UnityEngine;
using UnityEditor;
using System.Collections;

public static class MapUI 
{

	public static void Display(float width)
	{
		var controlsWidth = 250f;
		var statisticsWidth = 300f;

		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();

			EditorGUILayout.BeginVertical();
				MapControlsUI(controlsWidth);
			EditorGUILayout.EndVertical();
			
			EditorGUILayout.BeginVertical();
				MapStatisticsUI(statisticsWidth);
			EditorGUILayout.EndVertical();

			GUILayout.Label ("", GUILayout.Width (width - controlsWidth - statisticsWidth - 35));
		
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
				GameEditor.CreateNewMap();
			}
			GUI.enabled = true;
			
			if(GameEditor.currentMap == null)
				GUI.enabled = false;
			if(GUILayout.Button ("Save Map", GUILayout.Width(width / 2f)))
			{
				GameEditor.SaveMap();
			}
			GUI.enabled = true;
			
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		
			if(GameEditor.currentMap != null)
				GUI.enabled = false;
			if(GUILayout.Button("Load From Scene", GUILayout.Width(width / 2f)))
			{
				GameEditor.LoadFromScene();
			}
			GUI.enabled = true;
			
			if(GameEditor.currentMap != null)
				GUI.enabled = false;
			if(GUILayout.Button("Load From File", GUILayout.Width(width / 2f)))
			{
				GameEditor.LoadMap();
			}
			GUI.enabled = true;
			
		EditorGUILayout.EndHorizontal();
	}
	
	static void MapStatisticsUI(float width)
	{
		GUILayout.Label ("Current Map: " + ((GameEditor.currentMap == null) ? "NA" : GameEditor.currentMap.name), GUILayout.Width (width));
		GUILayout.Label ("Current Floor: " + ((GameEditor.currentFloor == null) ? "NA" : GameEditor.currentFloor.ToString()), GUILayout.Width (width));
		GUILayout.Label ("Highest floor: " + ((GameEditor.currentMap == null) ? "NA" : GameEditor.currentMap.highestFloor.ToString()), GUILayout.Width (width));
		GUILayout.Label ("Lowest floor: " + ((GameEditor.currentMap == null) ? "NA" : GameEditor.currentMap.lowestFloor.ToString()), GUILayout.Width (width));
		
	}
}
