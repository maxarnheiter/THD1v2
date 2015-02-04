using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class SceneRenderer
{
	public static void Render(SceneView sceneView)
	{
		if(!GameEditor.hasMap)
			return;
			
		if(GameEditor.currentMap.instances.Count <= 0)
			return;
			
		var nonFiltered = new List<Instance>();
		var filtered = new List<Instance>();
		
		foreach(var instanceKVP in GameEditor.currentMap.instances)
		{
			if(!IsFiltered(instanceKVP.Value))
				nonFiltered.Add(instanceKVP.Value);
			else
				filtered.Add(instanceKVP.Value);
		}
		
		if(filtered.Count > 0)
			foreach(var instance in filtered)
				instance.spriteRenderer.enabled = false;
		
		if(nonFiltered.Count > 0)
		{
			foreach(var instance in nonFiltered)
				instance.spriteRenderer.enabled = true;
				
			CameraViewRenderer.UpdateObjects(sceneView.camera, nonFiltered);
		}
			
	}
	
	static bool IsFiltered(Instance instance)
	{
		//Filter by Prefab Type
		switch(instance.prefab.prefabType)
		{
			case PrefabType.Ground:
				if(!GameEditor.showGrounds)
					return true;
				break;
			case PrefabType.Corner:
				if(!GameEditor.showCorners)
					return true;
				break;
			case PrefabType.Thing:
				if(!GameEditor.showThings)
					return true;
				break;
			case PrefabType.Item:
				if(!GameEditor.showItems)
					return true;
				break;
			case PrefabType.Player:
				if(!GameEditor.showCreatures)
					return true;
				break;
			case PrefabType.Monster:
				if(!GameEditor.showCreatures)
					return true;
				break;
			case PrefabType.NPC:
				if(!GameEditor.showCreatures)
					return true;
				break;
			default:
				return false;
				break;
		}
		
		//Filter by Floor
		if(GameEditor.showAllFloors)
			return true;
		
		if(instance.transform.position.z < (GameEditor.currentFloor * -1))
			return true;
		
		return false;
	}
	
}
