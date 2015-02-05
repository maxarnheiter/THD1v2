using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class GameEditor 
{
	public static bool hasMap;
	public static Map currentMap;
	public static int currentFloor;

	public static bool hasSprites;
	public static Dictionary<string, Texture2D> spriteTextures;
	public static Dictionary<string, Sprite> spriteObjects;

	public static bool hasPrefabs;
	static PrefabCollection prefabCollection;
	public static Dictionary<int, Prefab> prefabs
	{
		get 
		{ 
			if(prefabCollection == null)
				return null;
			else
				return prefabCollection.prefabs; 
		}
	}
	
	
	public static List<KeyValuePair<string, Texture2D>> spriteSelection;
	
	public static Prefab currentPrefab;
	
	public static EditorClickAction clickAction;
	
	public static bool showAllFloors = false;
	public static bool showGrounds = true;
	public static bool showCorners = true;
	public static bool showThings = true;
	public static bool showItems = true;
	public static bool showCreatures = true;
	
	public static int currentRealFloor
	{
		get { return currentFloor * -1; }
	}

	public static void LoadPrefabs()
	{
		hasPrefabs = false;

		prefabCollection = PrefabLoader.GetPrefabCollection();

		if (prefabCollection != null)
			hasPrefabs = true;
	}
	
	public static void LoadSprites()
	{
		hasSprites = false;
		
		SpriteLoader.LoadSprites(out spriteTextures, out spriteObjects);

		if (spriteTextures != null && spriteObjects != null)
			hasSprites = true;
	}
	
	public static void CreateNewMap()
	{
		var wizard = new MapWizard();
		wizard.Show();
	}
	
	public static void LoadFromScene()
	{
		GameEditor.hasMap = false;
		Map map = GameObject.FindObjectOfType<Map>();
		
		if(map != null)
		{
			hasMap = true;
			currentMap = map;
			currentFloor = 0;
		}
	}
	
	public static void SaveMap()
	{
		string path = EditorUtility.SaveFilePanel("", "", "", "xml");
		
		if(path != null || path != "")
			MapSerializer.Save(GameEditor.currentMap, path);
	}
	
	public static void LoadMap()
	{
		string path = EditorUtility.OpenFilePanel("", "", "xml");
		if(path != null || path != "")
		{
			GameEditor.hasMap = false;
			Map map = MapSerializer.Load(path);
			
			if(map != null)
			{
				GameEditor.hasMap = true;
				GameEditor.currentMap = map;
				GameEditor.currentFloor = 0;
			}
		}
	}

	public static void FloorUp()
	{
		if (currentMap.highestFloor == currentFloor)
			return;

		currentFloor++;
		
		SceneView.RepaintAll();
	}

	public static void FloorDown()
	{
		if (currentMap.lowestFloor == currentFloor)
			return;
		
		currentFloor--;
		
		SceneView.RepaintAll();
	}
	
	
}
