using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Stack : MonoBehaviour 
{
	public int id;
	
	Vector3 lastPosition;
	SpriteRenderer spriteRenderer;

	void Start () 
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		CheckForChange();
	}
	
	void Update () 
	{
		CheckForChange();
	}
	
	void CheckForChange()
	{
		if(lastPosition != this.transform.position)
		{
			this.id = StackDistributor.GetNextId();
			spriteRenderer.sortingLayerName = "Floor " + (this.transform.position.z * -1).ToString();
		}
		lastPosition = this.transform.position;
	}
}
