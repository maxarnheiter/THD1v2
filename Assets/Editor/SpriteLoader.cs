using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class SpriteLoader
{

	public static void LoadSprites(out Dictionary<string, Texture2D> textures, out Dictionary<string, Sprite> sprites)
	{
		var startTime = EditorApplication.timeSinceStartup;
		
		var rawSprites = Resources.LoadAll ("Sprites/");
		
		textures = new Dictionary<string, Texture2D>();
		sprites =  new Dictionary<string, Sprite>();
		
		if(rawSprites.Count () <= 0)
		{
			Debug.Log ("No sprites or spritesheets could be loaded.");
			return;
		}
		
		foreach(var obj in rawSprites)
		{
			if(obj.GetType() == typeof(Sprite))
			{
				Sprite sprite = obj as Sprite;
				if(!sprites.ContainsKey(sprite.name))
				{
					sprites.Add(sprite.name, sprite);
					textures.Add(sprite.name, sprite.GetSpriteTexture());
				}
				else
				{
					Debug.Log ("Duplicate sprite name found. Sprite with name " + sprite.name);
					return;
				}
			}
		}

		Debug.Log ("Sprite load time: " + (EditorApplication.timeSinceStartup - startTime).ToString("#.###") + " seconds. Quantity loaded: " + sprites.Count);
	}
}
