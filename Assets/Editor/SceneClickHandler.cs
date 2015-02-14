using UnityEngine;
using System.Collections;

public static class SceneClickHandler
{

	public static void OnClick(Vector2 position)
	{
        if (!MapManager.hasMap || !PrefabManager.hasPrefabs)
			return;

        switch (SceneManager.clickAction)
		{
			case EditorClickAction.None:
			break;
			case EditorClickAction.Draw:
              Draw(position, MapManager.currentFloor);
			break;
			case EditorClickAction.Erase:
                Erase(position, MapManager.currentFloor);
			break;
		}
	}
	
	static void Draw(Vector2 position, int floor)
	{
        MapManager.currentMap.Instantiate(PrefabManager.currentPrefab, position, floor);
	}
	
	static void Erase(Vector2 position, int floor)
	{
		
	}
	
}
