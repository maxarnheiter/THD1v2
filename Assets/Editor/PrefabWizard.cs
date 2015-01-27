using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PrefabWizard : EditorWindow 
{
	List<KeyValuePair<string, Texture2D>> selectionList;
	KeyValuePair<string, Texture2D> selectionNode;
	int selectionIndex;

	static void Init() 
	{
		PrefabWizard prefabWizardWindow = (PrefabWizard)EditorWindow.GetWindow (typeof(PrefabWizard));
	}
	
	public PrefabWizard(List<KeyValuePair<string, Texture2D>> selection)
	{
		selectionList = selection;
		selectionNode = selection[0];
	}
	
	public void OnGUI()
	{
		if(selectionList == null || selectionList.Count <= 0)
			this.Close();
	
		GUILayout.Label ("Sprites in selection: " + selectionList.Count());
		
		EditorGUILayout.BeginHorizontal();
		
			if(GUILayout.Button ("<"))
				selectionIndex--;
			
			if(GUILayout.Button (">"))
				selectionIndex++;
		
		EditorGUILayout.EndHorizontal();
		
		selectionNode = selectionList[Mathf.Abs(selectionIndex) % selectionList.Count];
		GUILayout.Button (selectionNode.Value, GUILayout.Width (selectionNode.Value.width), GUILayout.Height (selectionNode.Value.height));
		
		GUILayout.Label (selectionNode.Key);
		
		EditorGUILayout.BeginHorizontal();
		
			if(GUILayout.Button ("Make"))
				Make ();
				
			if(GUILayout.Button ("Cancel"))
				this.Close();
		
		EditorGUILayout.EndHorizontal();
	}
	
	void Make()
	{
	}
}
