using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public static class MapSerializer
{

	public static void Save(Map map, string path)
	{
		if(map.transform.childCount <= 0)
			return;
		
		var mapObjects = new List<MapObject>();
		foreach(var child in map.transform)
		{
            Transform transform = child as Transform;
            Prefab prefab = child as Prefab;
			mapObjects.Add(new MapObject(prefab.id, transform.position));
		}
		
		var mapFile = new MapFile(map.name, mapObjects.ToArray());
		
		XmlSerializer serializer = new XmlSerializer(typeof(MapFile));
		using(FileStream stream = new FileStream(path, FileMode.Create))
			serializer.Serialize(stream, mapFile);
	}
	
	public static Map Load(string path)
	{
		MapFile mapFile;
		
		XmlSerializer serializer =  new XmlSerializer(typeof(MapFile));
		using(FileStream stream = new FileStream(path, FileMode.Open))
			mapFile = serializer.Deserialize(stream) as MapFile;
			
		if(mapFile == null)
			return null;
			
		var newObject = new GameObject();
		Map map = newObject.AddComponent<Map>();
		map.name = mapFile.mapName;
		newObject.name = map.name;
		
		foreach(var mapObject in mapFile.mapObjects)
		{
			Prefab prefab;
            PrefabManager.prefabs.TryGetValue(mapObject.prefabId, out prefab);
			
			if(prefab == null)
			{
				Debug.Log ("Failed to load prefab with id: " + mapObject.prefabId);
				return null;
			}
			InstanceManager.Instantiate(prefab, new Vector2(mapObject.x, mapObject.y), mapObject.floor);
		}
		
		return map;
	}
	
	
	static List<Instance> Sort(List<Instance> instances)
	{
		return instances.OrderByDescending(i => i.transform.position.z)	
						.ThenBy(i => i.TypeToInt())
						.ThenByDescending(i => i.transform.position.y)			
						.ThenBy(i => i.transform.position.x)				
						.ThenBy(i => i.stack.id).ToList();
	}
}
