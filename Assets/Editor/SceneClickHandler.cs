using UnityEngine;
using System.Collections;

public static class SceneClickHandler
{

	public static void OnClick(Vector2 position)
	{
		if(!GameEditor.hasMap || !GameEditor.hasPrefabs)
			return;
			
		switch(GameEditor.clickAction)
		{
			case EditorClickAction.None:
			break;
			case EditorClickAction.Draw:
				Draw (position, GameEditor.currentFloor);
			break;
			case EditorClickAction.Erase:
				Erase (position, GameEditor.currentFloor);
			break;
		}
	}
	
	static void Draw(Vector2 position, int floor)
	{
		GameEditor.currentMap.Instantiate(GameEditor.currentPrefab, position, floor);
	}
	
	static void Erase(Vector2 position, int floor)
	{
		
	}
	
}
