using UnityEngine;
using System.Collections;

public static class ScenePreviewHandler
{

	static GameObject previewObject;

	public static void OnMove(Vector2 position)
	{
		if(previewObject == null)
			CreatePreviewObject();
			
		UpdatePreviewObject(position);
	}
	
	static void CreatePreviewObject()
	{
		
	}
	
	static void UpdatePreviewObject(Vector2 position)
	{
		
	}
	
}
