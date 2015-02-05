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
		if(map.instances.Count <= 0)
			return;
			
		var mapInstances = new List<Instance>();
		foreach(var kvp in map.instances)
			mapInstances.Add(kvp.Value);
		
		var mapObjects = new List<MapObject>();
		foreach(var instance in mapInstances)
		{
			mapObjects.Add(new MapObject(instance));
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
			GameEditor.prefabs.TryGetValue(mapObject.prefabId, out prefab);
			
			if(prefab == null)
			{
				Debug.Log ("Failed to load prefab with id: " + mapObject.prefabId);
				return null;
			}
			map.Instantiate(prefab, new Vector2(mapObject.x, mapObject.y), mapObject.floor);
		}
		
		return map;
	}
	
	
	static List<Instance> Sort(List<Instance> instances)
	{
		return instances.OrderByDescending(i => i.transform.position.z)	
						.ThenBy(i => TypeToInt(i.prefab.prefabType))
						.ThenByDescending(i => i.transform.position.y)			
						.ThenBy(i => i.transform.position.x)				
						.ThenBy(i => i.stack.id).ToList();
	}
	
	static int TypeToInt(PrefabType prefabType)
	{
		switch(prefabType)
		{
		case PrefabType.Ground:
			return 0;
			break;
		case PrefabType.Corner:
			return 1;
			break;
		default:
			return 2;
			break;
		}
		return 2;
	}
}
