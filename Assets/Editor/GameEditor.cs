using UnityEngine;
using System.Collections.Generic;

public static class GameEditor 
{
	public static Map currentMap;
	public static Floor currentFloor;
	
	public static Vector2 currentMousePosition;

	public static Dictionary<string, Texture2D> spriteTextures;
	public static Dictionary<string, Sprite> spriteObjects;
	public static Dictionary<int, Prefab> prefabs;
	
	public static List<KeyValuePair<string, Texture2D>> spriteSelection;
	
	public static Prefab currentPrefab;
	
	public static void LoadPrefabs()
	{
		prefabs = PrefabLoader.LoadPrefabs();
	}
	
	public static void LoadSprites()
	{
		SpriteLoader.LoadSprites(out spriteTextures, out spriteObjects);
	}
	
	
}
