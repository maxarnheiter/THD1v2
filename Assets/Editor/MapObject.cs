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
	
	public MapObject(Instance instance)
	{
		this.prefabId = instance.prefab.id;
		this.x = instance.transform.position.x;
		this.y = instance.transform.position.y;
		this.floor = (int)instance.transform.position.z * -1;
	}
	
}
