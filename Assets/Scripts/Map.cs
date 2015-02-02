using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
	
	}

}