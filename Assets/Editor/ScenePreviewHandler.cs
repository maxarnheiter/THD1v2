using UnityEngine;
using UnityEditor;
using System.Collections;

public static class ScenePreviewHandler
{

	static GameObject previewObject;
	static SpriteRenderer previewRenderer;

	static Sprite eraserSprite;

    static Texture2D _eraseTexture;
    static Texture2D eraseTexture
    { get { return _eraseTexture ?? (_eraseTexture = Resources.Load("EditorSprites/eraseTexture") as Texture2D); } }

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
		eraserSprite = Sprite.Create (eraseTexture, new Rect (0,0,32,32), new Vector2 (1, 0), 32f);
	}
	
	static void UpdatePreviewObject(Vector2 position)
	{
        var prefab = PrefabManager.currentPrefab;


        switch (SceneManager.clickAction) 
		{
			case EditorClickAction.None:
			{
				//Set texture
				previewRenderer.sprite = null;

				break;
			}
			case EditorClickAction.Draw:
			{
                if (prefab == null || MapManager.hasMap == false || SpriteManager.hasSprites == false)
					break;
				
				//Set position
                previewObject.transform.position = new Vector3(position.x, position.y, (float)MapManager.currentRealFloor);
				
				//Set texture
				if(previewRenderer.sprite == null || previewRenderer.sprite.name != prefab.spriteName)
				{
					Sprite sprite;
					SpriteManager.spriteObjects.TryGetValue(prefab.spriteName, out sprite);
					previewRenderer.sprite = sprite;	
				}

				//Set sorting layer
                previewRenderer.sortingLayerName = "Floor " + MapManager.currentFloor;

				break;
			}
			case EditorClickAction.Erase:
			{
                if (MapManager.hasMap == false)
					break;
				
				//Set position
                previewObject.transform.position = new Vector3(position.x, position.y, MapManager.currentRealFloor);
				
				//Set texture
				if(previewRenderer.sprite != eraserSprite)
					previewRenderer.sprite = eraserSprite;
				
				//Set sorting layer
                previewRenderer.sortingLayerName = "Floor " + MapManager.currentFloor;

				break;
			}
		}
	}


	
}
