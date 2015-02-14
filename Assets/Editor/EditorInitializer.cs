using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public static class EditorInitializer
{

	static bool loadSprites;
	static bool loadPrefabs;
	static bool loadMapFromScene;

	static EditorInitializer()
	{
		EditorApplication.update += Update;
	}
	
	static void Update()
	{
		if(EditorApplication.timeSinceStartup > 2)
		if(!loadSprites)
		{
			loadSprites = true;
			SpriteManager.LoadSprites();
		}
		
		if(EditorApplication.timeSinceStartup > 5)
		if(!loadPrefabs)
		{
			loadPrefabs = true;
			PrefabManager.LoadPrefabs();
		}
		
		if(EditorApplication.timeSinceStartup > 5)
		if(!loadMapFromScene)
		{
			loadMapFromScene = true;
			MapManager.LoadFromScene();
		}
	}
	
}
