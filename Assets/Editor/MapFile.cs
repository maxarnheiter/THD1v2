using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class MapFile 
{

	public string mapName;
	public MapObject[] mapObjects;
	
	public MapFile()
	{}
	
	public MapFile(string mapName, MapObject[] mapObjects)
	{
		this.mapName = mapName;
		this.mapObjects = mapObjects;
	}
	
}
