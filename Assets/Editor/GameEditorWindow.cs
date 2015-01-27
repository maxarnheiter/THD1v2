using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameEditorWindow : EditorWindow 
{
	[MenuItem ("THD/Game Editor")]
	static void Init() 
	{
		GameEditorWindow mapEditorWindow = (GameEditorWindow)EditorWindow.GetWindow (typeof(GameEditorWindow));
	}
	
	void OnEnable() 
	{
		this.title = "Game Editor";
		SceneView.onSceneGUIDelegate -= SceneInputHandler.OnSceneGUI;
		SceneView.onSceneGUIDelegate += SceneInputHandler.OnSceneGUI;
	}
	
	void OnGUI()
	{
		GameEditorUI.Display(this.position.width);
	}
}
