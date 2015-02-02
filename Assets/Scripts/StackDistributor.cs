using UnityEngine;
using System.Collections;

public static class StackDistributor 
{

	static int nextId;
	
	public static int GetNextId()
	{
		nextId++;
		return nextId;
	}
	
}
