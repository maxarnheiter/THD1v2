using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class Map : MonoBehaviour 
{

	public string name;
	
	public int highestFloor;
	public int lowestFloor;
	
	public Map()
	{
		this.highestFloor = 10;
		this.lowestFloor = -10;
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
		if(this.transform.childCount != InstanceManager.instances.Count)
			foreach(Transform child in transform)
				InstanceManager.instances.Add(child.gameObject.GetInstanceID(), new Instance(child.gameObject));
	}

}