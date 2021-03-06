﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class PrefabLoader 
{

	public static PrefabCollection GetPrefabCollection()
	{
		while(GameObject.Find("Prefab Collection") != null)
			GameObject.DestroyImmediate(GameObject.Find("Prefab Collection"));
			
		var newObject = new GameObject();
		newObject.name = "Prefab Collection";
		var prefabCollection = newObject.AddComponent<PrefabCollection>();
		
		prefabCollection.prefabs = LoadPrefabs();
		
		return prefabCollection;
	}


	static Dictionary<int, Prefab> LoadPrefabs()
	{
		double startTime = EditorApplication.timeSinceStartup;
		
		var prefabs = new Dictionary<int, Prefab>();
		
		var rawPrefabObjects = Resources.LoadAll("Prefabs/");
		
		if(rawPrefabObjects.Count() <= 0)
		{
			Debug.Log ("No prefab objects could be loaded.");
			return null;
		}
		
		foreach(var obj in rawPrefabObjects)
		{
			Prefab prefab = (obj as GameObject).GetComponent<Prefab>();
			
			if(!prefabs.ContainsKey(prefab.id))
			{
				prefabs.Add(prefab.id, prefab);
			}
			else
			{
				Debug.Log ("Duplicate id found. Prefab with id " + prefab.id);
				return null;
			}
		}
		
		Debug.Log ("Prefab load time: " + (EditorApplication.timeSinceStartup - startTime).ToString("#.###") + " seconds. Quantity loaded: " + prefabs.Count);
		
		return prefabs;
	}
	
}
