using UnityEngine;
using System.Collections.Generic;

public static class GameEditor 
{
	public static bool hasMap;
	public static Map currentMap;
	public static Floor currentFloor;

	public static bool hasSprites;
	public static Dictionary<string, Texture2D> spriteTextures;
	public static Dictionary<string, Sprite> spriteObjects;

	public static bool hasPrefabs;
	public static Dictionary<int, Prefab> prefabs;
	
	public static List<KeyValuePair<string, Texture2D>> spriteSelection;
	
	public static Prefab currentPrefab;
	
	public static EditorClickAction clickAction;


	public static void LoadPrefabs()
	{
		hasPrefabs = false;

		prefabs = PrefabLoader.LoadPrefabs();

		if (prefabs != null)
			hasPrefabs = true;
	}
	
	public static void LoadSprites()
	{
		hasSprites = false;

		SpriteLoader.LoadSprites(out spriteTextures, out spriteObjects);

		if (spriteTextures != null && spriteObjects != null)
			hasSprites = true;
	}

	public static void FloorUp()
	{
		if (currentMap.GetHighestFloor () == currentFloor)
			return;

		currentFloor = currentMap.GetFloor (currentFloor.height + 1);
	}

	public static void FloorDown()
	{
		if (currentMap.GetLowestFloor () == currentFloor)
			return;
		
		currentFloor = currentMap.GetFloor (currentFloor.height - 1);
	}
	
	
}
