using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Map : MonoBehaviour 
{

	public string name;
	
	public List<Floor> floors;
	public List<GameObject> floorObjects;
	
	public Map()
	{
		this.floors = new List<Floor>();
		this.floorObjects =  new List<GameObject>();
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
	
	public void AddNewFloor(int newFloorHeight)
	{
		var newFloorObject = new GameObject();
		var newFloor = newFloorObject.AddComponent<Floor>();
		
		newFloor.height = newFloorHeight;
		newFloorObject.name = "Floor " + newFloorHeight.ToString();
		newFloorObject.transform.parent = this.transform;
		
		this.floors.Add(newFloor);
		this.floorObjects.Add(newFloorObject);
	}
	
	public Floor GetZeroFloor()
	{
		if(floors.Count == 0)
			return null;
			
		return this.floors.Where(f => f.height == 0).FirstOrDefault();
	}
	
	public Floor GetHighestFloor()
	{
		if(floors.Count == 0)
			return null;

		return this.floors.OrderByDescending(f => f.height).FirstOrDefault();
	}

	public Floor GetLowestFloor()
	{
		if(floors.Count == 0)
			return null;
			
		return this.floors.OrderByDescending(f => f.height).LastOrDefault();
	}
}