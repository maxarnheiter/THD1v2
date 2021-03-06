﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class SceneRenderer
{

	public static void Render(SceneView sceneView)
	{
        if (!MapManager.hasMap)
			return;

        if (InstanceManager.instances.Count <= 0)
			return;
			
		var nonFiltered = new List<Instance>();
		var filtered = new List<Instance>();

        foreach (var instanceKVP in InstanceManager.instances)
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

            foreach (var instance in nonFiltered)
                SetFloorTransparency(instance);
				
			CameraViewRenderer.UpdateObjects(sceneView.camera, nonFiltered);
		}
			
	}
	
	static bool IsFiltered(Instance instance)
	{
		//Filter by Prefab Type
		switch(instance.prefab.prefabType)
		{
			case PrefabType.Ground:
				if(!SceneManager.showGrounds)
					return true;
				break;
			case PrefabType.Corner:
                if (!SceneManager.showCorners)
					return true;
				break;
			case PrefabType.Thing:
                if (!SceneManager.showThings)
					return true;
				break;
			case PrefabType.Item:
                if (!SceneManager.showItems)
					return true;
				break;
			case PrefabType.Player:
                if (!SceneManager.showCreatures)
					return true;
				break;
			case PrefabType.Monster:
                if (!SceneManager.showCreatures)
					return true;
				break;
			case PrefabType.NPC:
                if (!SceneManager.showCreatures)
					return true;
				break;
			default:
				return false;
				break;
		}
		
		//Filter by Floor
        if (SceneManager.showAllFloors)
			return false;

        if (instance.transform.position.z < MapManager.currentRealFloor)
			return true;
		
		return false;
	}

    static void SetFloorTransparency(Instance instance)
    {
        if (instance.transform.position.z > MapManager.currentRealFloor)
        {
            instance.spriteRenderer.color = new Color(SceneManager.floorTransparencyColor.r, SceneManager.floorTransparencyColor.g, SceneManager.floorTransparencyColor.b, SceneManager.floorTransparency);
        }
        else
            instance.spriteRenderer.color = Color.white;
    }
	
}
