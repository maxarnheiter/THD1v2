using UnityEngine;
using UnityEditor;
using System.Collections;

public static class MapUI 
{

	public static void Display(float width)
	{
		width = 400;
	
		GUILayout.Label ("Current Map: " + ((GameEditor.currentMap == null) ? "NA" : GameEditor.currentMap.name), GUILayout.Width (width));
		GUILayout.Label ("Current Floor: " + ((GameEditor.currentFloor == null) ? "NA" : GameEditor.currentFloor.height.ToString()), GUILayout.Width (width));
		
		if(GameEditor.currentMap == null)
		{
			EditorGUILayout.BeginHorizontal();
				if(GUILayout.Button ("Create New Map", GUILayout.Width(width / 3f)))
				{
					var wizard = new MapWizard();
					wizard.Show();
				}
				if(GUILayout.Button("Load From File", GUILayout.Width(width / 3f)))
				{
				}
				if(GUILayout.Button("Load From Scene", GUILayout.Width(width / 3f)))
				{
					Map map = GameObject.FindObjectOfType<Map>();
					
					if(map != null)
					{
						GameEditor.currentMap = map;
						GameEditor.currentFloor = map.GetZeroFloor();
					}
				}
			EditorGUILayout.EndHorizontal();
		}
		
		if(GameEditor.currentMap != null)
		{
			EditorGUILayout.Space();
			
			GUILayout.Label ("Number of floors: " + GameEditor.currentMap.floors.Count, GUILayout.Width (width));
			GUILayout.Label ("Highest floor: " + ((GameEditor.currentMap.GetHighestFloor() == null) ? "NA" : GameEditor.currentMap.GetHighestFloor().height.ToString()), GUILayout.Width (width));
			GUILayout.Label ("Lowest floor: " + ((GameEditor.currentMap.GetLowestFloor() == null) ? "NA" : GameEditor.currentMap.GetLowestFloor().height.ToString()), GUILayout.Width (width));
		}
		
		
		
	}
}
