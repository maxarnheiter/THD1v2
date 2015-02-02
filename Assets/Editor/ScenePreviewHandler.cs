using UnityEngine;
using UnityEditor;
using System.Collections;

public static class ScenePreviewHandler
{

	static GameObject previewObject;
	static SpriteRenderer previewRenderer;

	static Sprite eraserSprite;

	public static void OnMove(Vector2 position)
	{
		if(previewObject == null)
			CreatePreviewObject();

		if (eraserSprite == null)
			CreateEraserSprite ();
			
		UpdatePreviewObject(position);

		SceneView.RepaintAll ();
	}
	
	static void CreatePreviewObject()
	{
		while(GameObject.Find("Preview Object") != null)
			Object.DestroyImmediate(GameObject.Find("Preview Object"));

		previewObject = new GameObject ();
		previewObject.name = "Preview Object";
		previewRenderer = previewObject.AddComponent<SpriteRenderer> ();
		previewRenderer.sortingOrder = 32000;
		previewRenderer.color = new Color (1f, 1f, 1f, 0.5f);
		previewObject.hideFlags = HideFlags.HideAndDontSave;
	}

	static void CreateEraserSprite()
	{
		eraserSprite = Sprite.Create (EditorIcons.eraseTexture, new Rect (0,0,32,32), new Vector2 (1, 0), 32f);
	}
	
	static void UpdatePreviewObject(Vector2 position)
	{
		var prefab = GameEditor.currentPrefab;


		switch (GameEditor.clickAction) 
		{
			case EditorClickAction.None:
			{
				//Set texture
				previewRenderer.sprite = null;

				break;
			}
			case EditorClickAction.Draw:
			{
				if(prefab == null || GameEditor.hasMap == false || GameEditor.hasSprites == false)
					break;
				
				//Set position
				previewObject.transform.position = new Vector3(position.x, position.y, (float)GameEditor.currentRealFloor);
				
				//Set texture
				if(previewRenderer.sprite == null || previewRenderer.sprite.name != prefab.spriteName)
				{
					Sprite sprite;
					GameEditor.spriteObjects.TryGetValue(prefab.spriteName, out sprite);
					previewRenderer.sprite = sprite;	
				}

				//Set sorting layer
				previewRenderer.sortingLayerName = "Floor " + GameEditor.currentFloor;

				break;
			}
			case EditorClickAction.Erase:
			{
				if(GameEditor.hasMap == false)
					break;
				
				//Set position
				previewObject.transform.position = new Vector3(position.x, position.y, GameEditor.currentRealFloor);
				
				//Set texture
				if(previewRenderer.sprite != eraserSprite)
					previewRenderer.sprite = eraserSprite;
				
				//Set sorting layer
				previewRenderer.sortingLayerName = "Floor " + GameEditor.currentFloor;

				break;
			}
		}
	}


	
}
