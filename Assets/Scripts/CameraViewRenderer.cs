using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class CameraViewRenderer
{
	static float searchBuffer = 2f;

	public static void UpdateObjects(Camera camera, List<Instance> instances)
	{
		var visible = new List<Instance>();
		
		var cameraRect = GetVisibleRect(camera);
		
		//Filter out those that arn't visible to camera
		foreach(var instance in instances)
		{
			if(IsWithinView(cameraRect, instance))
				visible.Add(instance);
		}
		
		//Sort those that are visible
		var sorted = Sort (visible);
		
		SetSortingOrder(sorted);
		
	}
	
	static void SetSortingOrder(List<Instance> instances)
	{
		var cornerCount = 0;
		var thingCount = 0;
		
		var cornerBase = 1;
		var thingBase = 5000;
		
		foreach(var instance in instances)
		{
			switch(instance.prefab.prefabType)
			{
				case PrefabType.Ground:
					instance.spriteRenderer.sortingOrder = 0;
					break;
				case PrefabType.Corner:
					instance.spriteRenderer.sortingOrder = cornerBase + cornerCount;
					cornerCount++;
					break;
				default:
					instance.spriteRenderer.sortingOrder = thingBase + thingCount;
					thingCount++;
					break;
			}
		}
		
	}
	
	static List<Instance> Sort(List<Instance> instances)
	{
		return instances.OrderByDescending(i => i.transform.position.z)	
				.ThenBy(i => i.TypeToInt())
				.ThenByDescending(i => i.transform.position.y)			
				.ThenBy(i => i.transform.position.x)				
				.ThenBy(i => i.stack.id)
                .ToList();
	}
	
	static Rect GetVisibleRect(Camera camera)
	{
		float camRatio = camera.pixelWidth / camera.pixelHeight;
		float halfCamHeight = camera.orthographicSize;
		float halfCamWidth = halfCamHeight * camRatio;
		
		return new Rect( (camera.transform.position.x - halfCamWidth - searchBuffer),
		                (camera.transform.position.y - halfCamHeight - searchBuffer),
		                ((halfCamWidth + searchBuffer) * 2f),
		                ((halfCamHeight + searchBuffer) * 2f));
	}
	
	static bool IsWithinView(Rect rect, Instance instance)
	{
		if(	instance.transform.position.x >= rect.xMin &&
			instance.transform.position.x <= rect.xMax &&
			instance.transform.position.y >= rect.yMin &&
			instance.transform.position.y <= rect.yMax)
			return true;
			
		return false;
	}

}
