using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class SpriteLoader
{

	public static Dictionary<string, Texture2D> LoadSprites()
	{
		
		var rawSprites = Resources.LoadAll ("Sprites/");
		
		if(rawSprites.Count () <= 0)
		{
			Debug.Log ("No sprites or spritesheets could be loaded.");
			return null;
		}
		
		Dictionary<string, Texture2D> sprites =  new Dictionary<string, Texture2D>();
		
		foreach(var obj in rawSprites)
		{
			if(obj.GetType() == typeof(Sprite))
			{
				Sprite sprite = obj as Sprite;
				if(!sprites.ContainsKey(sprite.name))
				{
					sprites.Add(sprite.name, sprite.GetSpriteTexture());
				}
				else
				{
					Debug.Log ("Duplicate sprite name found. Sprite with name " + sprite.name);
					return null;
				}
			}
		}
		
		return sprites;
	}
}
