using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class SceneClickHandler
{

	public static void OnClick(Vector2 position)
	{
        if (!MapManager.hasMap || !PrefabManager.hasPrefabs)
			return;

        switch (SceneManager.clickAction)
		{
			case SceneClickAction.None:
			break;
			case SceneClickAction.Draw:
                Draw(PrefabManager.currentPrefab, position, MapManager.currentFloor);
			break;
			case SceneClickAction.Erase:
                Erase(position, MapManager.currentFloor);
			break;
		}
	}
	

    //Drawing
	static void Draw(Prefab prefab, Vector2 position, int floor)
	{
        if (IsDuplicate(prefab, position, floor))
            return;
        

        InstanceManager.Instantiate(PrefabManager.currentPrefab, position, floor);
	}

    static bool IsDuplicate(Prefab prefab, Vector2 position, int floor)
    {
        var instances = InstanceManager.GetAllFromPosition(new Vector3(position.x, position.y, floor * -1));

        if (instances == null)
            return false;

        if (instances.Any(i => i.prefab.id == prefab.id))
            return true;

        return false;
    }
	

    //Erasing
	static void Erase(Vector2 position, int floor)
	{
        var instances = InstanceManager.GetAllSortedFromPosition(new Vector3(position.x, position.y, floor * -1));

        foreach(var instance in instances)
        {
            if(IsCurrentlyVisible(instance))
            {
                InstanceManager.Destroy(instance);
                break;
            }
        }
	}

    static bool IsCurrentlyVisible(Instance instance)
    {
        if (SceneManager.showGrounds && instance.prefab.prefabType == PrefabType.Ground)
            return true;

        if (SceneManager.showCorners && instance.prefab.prefabType == PrefabType.Corner)
            return true;

        if (SceneManager.showThings && instance.prefab.prefabType == PrefabType.Thing)
            return true;

        if (SceneManager.showItems && instance.prefab.prefabType == PrefabType.Item)
            return true;

        if (SceneManager.showCreatures && instance.prefab.prefabType == PrefabType.Monster)
            return true;

        if (SceneManager.showCreatures && instance.prefab.prefabType == PrefabType.Player)
            return true;

        if (SceneManager.showCreatures && instance.prefab.prefabType == PrefabType.NPC)
            return true;

        return false;
    }
	
}
