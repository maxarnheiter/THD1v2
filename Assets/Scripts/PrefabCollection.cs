using UnityEngine;
using System.Collections.Generic;

public class PrefabCollection : MonoBehaviour 
{

	public Dictionary<int, Prefab> prefabs;
	
	public PrefabCollection()
	{
		this.prefabs = new Dictionary<int, Prefab>();
	}
	
}
