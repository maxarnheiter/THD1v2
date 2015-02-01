using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{

	public int height;
	
	public int realHeight
	{
		get { return (height * -1); }
	}

	public string sortingLayerName
	{
		get { return ("Floor " + this.height.ToString ()); }
	}
	
	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
