using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public static class SpriteManager
{
    public static bool hasSprites;
    public static Dictionary<string, Texture2D> spriteTextures;
    public static Dictionary<string, Sprite> spriteObjects;


    public static void LoadSprites()
    {
        hasSprites = false;

        SpriteLoader.LoadSprites(out spriteTextures, out spriteObjects);

        if (spriteTextures != null && spriteObjects != null)
            hasSprites = true;
    }

    public static void DisplayUI(float width)
    {
        SpritesUI.Display(width);
    }

    public static void OpenPrefabWizard(List<KeyValuePair<string, Texture2D>> spriteSelection)
    {
        var wizard = new SpriteWizard(spriteSelection);
        wizard.Show();
    }


}

