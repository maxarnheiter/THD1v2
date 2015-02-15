using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PrefabsUI 
{

	static Vector2 size = Vector2.zero;
	static int buttonPadding = 12;
	
	static PrefabType typeFilter = PrefabType.Any;
	static PrefabCategory categoryFilter = PrefabCategory.Any;
	static PrefabColor colorFilter = PrefabColor.Any;
	
    enum ClickAction
    {
        SetAsCurrent,
        ChangeSetId,
        ChangeType,
        ChangeCategory,
        ChangeColor
    }
    static ClickAction clickAction;

    static int nextSetId;
    static PrefabType nextPrefabType;
    static PrefabCategory nextPrefabCategory;
    static PrefabColor nextPrefabColor;

	
	public static void Display(float width)
	{
		var sidebarWidth = 250f;
        var scrollviewWidth = width - sidebarWidth - 10;
		
		EditorGUILayout.BeginHorizontal ();
		
		EditorGUILayout.BeginVertical (GUILayout.Width (sidebarWidth));
		SidebarUI (sidebarWidth);
		EditorGUILayout.EndVertical ();
		
		EditorGUILayout.BeginVertical (GUILayout.Width (scrollviewWidth));
		ScrollviewUI (scrollviewWidth);
		EditorGUILayout.EndVertical ();
		
		EditorGUILayout.EndHorizontal ();
	}
	
	static void SidebarUI(float width)
	{
        PrefabLoadingUI(width);

        PrefabViewFilterUI(width);

        PrefabSelectionUI(width);

        PrefabSelectionSpecificUI(width);
	}

    static void PrefabLoadingUI(float width)
    {
        if (GUILayout.Button("Load Prefabs"))
            PrefabManager.LoadPrefabs();

        GUILayout.Label("Prefab objects loaded: " + ((PrefabManager.prefabs == null) ? "0" : PrefabManager.prefabs.Count().ToString()));

        EditorGUILayout.Space();
    }

    static void PrefabViewFilterUI(float width)
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Type:", GUILayout.Width(65f));

        typeFilter = (PrefabType)EditorGUILayout.EnumPopup(typeFilter, GUILayout.Width(90f));

        if (GUILayout.Button("Reset", GUILayout.Width(45f)))
            categoryFilter = PrefabCategory.Any;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Category:", GUILayout.Width(65f));

        categoryFilter = (PrefabCategory)EditorGUILayout.EnumPopup(categoryFilter, GUILayout.Width(90f));

        if (GUILayout.Button("Reset", GUILayout.Width(45f)))
            categoryFilter = PrefabCategory.Any;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Color:", GUILayout.Width(65f));

        colorFilter = (PrefabColor)EditorGUILayout.EnumPopup(colorFilter, GUILayout.Width(90f));

        if (GUILayout.Button("Reset", GUILayout.Width(45f)))
            colorFilter = PrefabColor.Any;

        EditorGUILayout.EndHorizontal();
    }

    static void PrefabSelectionUI(float width)
    {
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

            if (clickAction == ClickAction.SetAsCurrent)
                GUI.enabled = false;
            if (GUILayout.Button("Select"))
                clickAction = ClickAction.SetAsCurrent;
            GUI.enabled = true;

            if (clickAction == ClickAction.ChangeSetId)
                GUI.enabled = false;
            if (GUILayout.Button("SetID"))
                clickAction = ClickAction.ChangeSetId;
            GUI.enabled = true;

            if (clickAction == ClickAction.ChangeType)
                GUI.enabled = false;
            if (GUILayout.Button("Type"))
                clickAction = ClickAction.ChangeType;
            GUI.enabled = true;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

            if (clickAction == ClickAction.ChangeCategory)
                GUI.enabled = false;
            if (GUILayout.Button("Category"))
                clickAction = ClickAction.ChangeCategory;
            GUI.enabled = true;

            if (clickAction == ClickAction.ChangeColor)
                GUI.enabled = false;
            if (GUILayout.Button("Color"))
                clickAction = ClickAction.ChangeColor;
            GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
    }

    static void PrefabSelectionSpecificUI(float width)
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

            switch(clickAction)
            {
                case ClickAction.SetAsCurrent:
                {
                    GUILayout.FlexibleSpace();

                    if(PrefabManager.currentPrefab != null)
                    {
                        Texture2D texture;
                        SpriteManager.spriteTextures.TryGetValue(PrefabManager.currentPrefab.spriteName, out texture);
                        GUILayout.Button(texture, GUILayout.Width(texture.width + buttonPadding), GUILayout.Height(texture.height + buttonPadding));
                    }

                    GUILayout.FlexibleSpace();

                    break;
                }
                case ClickAction.ChangeSetId:
                {
                    GUILayout.Label("Set ID: ");

                    nextSetId = EditorGUILayout.IntField(nextSetId);

                    if(GUILayout.Button("Get Next"))
                    {
                        nextSetId = PrefabManager.prefabs.Max(p => p.Value.setId) + 1;
                    }

                    break;
                }
                case ClickAction.ChangeType:
                {
                    nextPrefabType = (PrefabType)EditorGUILayout.EnumPopup("Prefab Type ", nextPrefabType);
                    break;
                }
                case ClickAction.ChangeCategory:
                {
                    nextPrefabCategory = (PrefabCategory)EditorGUILayout.EnumPopup("Prefab Category ", nextPrefabCategory);
                    break;
                }
                case ClickAction.ChangeColor:
                {
                    nextPrefabColor = (PrefabColor)EditorGUILayout.EnumPopup("Prefab Color ", nextPrefabColor);
                    break;
                }
            }

        EditorGUILayout.EndHorizontal();
    }
	
	static void ScrollviewUI(float width)
	{
		GUILayout.Label ("", GUILayout.Width (width));

        if (PrefabManager.prefabs == null)
			return;
        if (PrefabManager.prefabs.Count <= 0)
			return;
		
		size = EditorGUILayout.BeginScrollView (size);

        foreach (var set in PrefabManager.prefabs.GroupBy(p => p.Value.setId))
		{
			DisplaySet (set.OrderBy(p => p.Value.spriteWidth), width);
		}

		EditorGUILayout.EndScrollView ();
	}
	
	static void DisplaySet(IOrderedEnumerable<KeyValuePair<int, Prefab>> set, float width)
	{
		GUILayout.Label("Set ID: " + set.First().Value.setId);
	
		int currentWidth = 0;
	
		EditorGUILayout.BeginHorizontal();
		foreach(var prefabKVP in set)
		{
			if(currentWidth >= width)
			{
				currentWidth = 0;
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
			}
			
			currentWidth = currentWidth + prefabKVP.Value.spriteWidth + (2 * buttonPadding);
			
			if(!IsFiltered(prefabKVP.Value))
				DisplayPrefabButton(prefabKVP);
		}
		EditorGUILayout.EndHorizontal();
	}
	
	static bool IsFiltered(Prefab prefab)
	{
	
		if(typeFilter != PrefabType.Any && prefab.prefabType != typeFilter)
			return true;
		
		if(categoryFilter != PrefabCategory.Any && prefab.prefabCategory != categoryFilter)
			return true;
			
		if(colorFilter != PrefabColor.Any && prefab.prefabColor != colorFilter)
			return true;
			
		return false;
	}
	
	static void DisplayPrefabButton(KeyValuePair<int, Prefab> prefabKVP)
	{

        if (PrefabManager.currentPrefab == prefabKVP.Value)
			GUI.enabled = false;
			
		Texture2D prefabTexture;
		SpriteManager.spriteTextures.TryGetValue(prefabKVP.Value.spriteName, out prefabTexture);
		if(prefabTexture == null)
		{
			//TODO display an error texture
		}

        if (GUILayout.Button(prefabTexture, GUILayout.Width(prefabTexture.width + buttonPadding), GUILayout.Height(prefabTexture.height + buttonPadding)))
            OnPrefabButtonPressed(prefabKVP.Value);
		
		GUI.enabled = true;
	}

    static void OnPrefabButtonPressed(Prefab prefab)
    {
        switch (clickAction)
        {
            case ClickAction.SetAsCurrent:
            {
                PrefabManager.currentPrefab = prefab;
                break;
            }
            case ClickAction.ChangeSetId:
            {
                prefab.setId = nextSetId;
                EditorUtility.SetDirty(prefab);
                break;
            }
            case ClickAction.ChangeType:
            {
                prefab.prefabType = nextPrefabType;
                EditorUtility.SetDirty(prefab); 
                break;
            }
            case ClickAction.ChangeCategory:
            {
                prefab.prefabCategory = nextPrefabCategory;
                EditorUtility.SetDirty(prefab);    
                break;
            }
            case ClickAction.ChangeColor:
            {
                prefab.prefabColor = nextPrefabColor;
                EditorUtility.SetDirty(prefab);
                break;
            }
        }
    }
}
