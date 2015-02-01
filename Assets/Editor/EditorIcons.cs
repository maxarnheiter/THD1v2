using UnityEngine;
using UnityEditor;
using System.Collections;

public static class EditorIcons 
{
	
	static Texture2D _pencilIcon;
	public static Texture2D pencilIcon 
	{
		get { return _pencilIcon ?? (_pencilIcon = Resources.Load("EditorSprites/pencil") as Texture2D); }
	}
	
	static Texture2D _eraserIcon;
	public static Texture2D eraserIcon 
	{
		get { return _eraserIcon ?? (_eraserIcon = Resources.Load("EditorSprites/eraser") as Texture2D); }
	}
	
	static Texture2D _upIcon;
	public static Texture2D upIcon 
	{
		get { return _upIcon ?? (_upIcon = Resources.Load("EditorSprites/up") as Texture2D); }
	}
	
	static Texture2D _downIcon;
	public static Texture2D downIcon 
	{
		get { return _downIcon ?? (_downIcon = Resources.Load("EditorSprites/down") as Texture2D); }
	}


	static Texture2D _eraseTexture;
	public static Texture2D eraseTexture
	{
		get { return _eraseTexture ?? (_eraseTexture = Resources.Load("EditorSprites/eraseTexture") as Texture2D); }
	}
	
}
