﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Map : MonoBehaviour 
{

	public string name;
	
	public Dictionary<int, Instance> instances;
	
	public int highestFloor;
	public int lowestFloor;
	
	public Map()
	{
		this.highestFloor = 10;
		this.lowestFloor = -10;
		this.instances =  new Dictionary<int, Instance>();
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
		ContinuityCheck();
	}
	
	void ContinuityCheck()
	{
		if(this.transform.childCount != this.instances.Count)
			foreach(Transform child in transform)
				instances.Add(child.gameObject.GetInstanceID(), new Instance(child.gameObject));
	}
	
	public void Instantiate(Prefab prefab, Vector2 position, int floor)
	{
		var newPrefab = Object.Instantiate(prefab, new Vector3(position.x, position.y, (float)(floor * -1)), Quaternion.identity) as Prefab;
		
		var newObject = newPrefab.gameObject;
		
		newObject.transform.parent = this.transform;
		
		instances.Add(newObject.GetInstanceID(), new Instance(newObject));
	}

    public void Destroy()
    {
        //TODO
    }

}