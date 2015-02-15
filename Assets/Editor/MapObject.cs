using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class MapObject 
{

	public int prefabId;
	public float x;
	public float y;
	
	public int floor;
	
	public MapObject()
	{}
	
	public MapObject(int prefabId, Vector3 position)
	{
        this.prefabId = prefabId;
		this.x = position.x;
		this.y = position.y;
		this.floor = (int)position.z * -1;
	}
	
}
