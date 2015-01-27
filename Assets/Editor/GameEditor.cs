using UnityEngine;
using System.Collections.Generic;

public static class GameEditor 
{
	public static Map currentMap;
	public static Floor currentFloor;
	
	public static Vector2 currentMousePosition;

	public static Dictionary<string, Texture2D> sprites;
	public static List<KeyValuePair<string, Texture2D>> spriteSelection;
	
}
