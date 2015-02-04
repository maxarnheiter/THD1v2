using UnityEngine;
using UnityEditor;
using System.Collections;

public static class SceneInputHandler 
{

	public static void OnSceneGUI(SceneView sceneView)
	{
		if(Tools.current != Tool.View)
			return;
		
		var current = Event.current;
		
		if(current.button != 0)
			return;
		
		switch(current.type) 
		{	
			case EventType.MouseUp: 
			{
				SceneClickHandler.OnClick(GetMousePosition(sceneView, current));
				break;
			}
			case EventType.MouseDrag:
			{
				SceneClickHandler.OnClick(GetMousePosition(sceneView, current));
				break;
			}
			case EventType.MouseMove: 
			{
				ScenePreviewHandler.OnMove(GetMousePosition(sceneView, current));
				break;
			}
			case EventType.Repaint: 
			{
				SceneRenderer.Render(sceneView);
				break;
			}
		}
	}
	
	static Vector2 GetMousePosition(SceneView view, Event current)
	{
		var adjustedMousePosition = new Vector2(current.mousePosition.x, view.camera.pixelHeight - current.mousePosition.y);
		var rawMousePosition = view.camera.ScreenToWorldPoint(adjustedMousePosition);
		
		return new Vector2(Mathf.Floor(rawMousePosition.x) + 1f, Mathf.Floor(rawMousePosition.y));
	}

	
}
