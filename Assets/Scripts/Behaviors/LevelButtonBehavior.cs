using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[CustomEditor(typeof(LevelButtonBehavior))]
public class MenuButtonEditor : Editor
{
	public override void OnInspectorGUI()
	{
		LevelButtonBehavior targetMenuButton = (LevelButtonBehavior)target;

		targetMenuButton.levelName = EditorGUILayout.TextField("Level Name", targetMenuButton.levelName);

		// Show default inspector property editor
		DrawDefaultInspector();
	}
}

public class LevelButtonBehavior : Button {
	public string levelName = "";

	public void Awake() {
		this.onClick.AddListener(ClickedThisButton);
	}

	private void ClickedThisButton() {
		ApplicationModelBehavior.LevelToLoad = this.levelName;
		StartCoroutine(LoadGameAsync());
	}
		
	IEnumerator LoadGameAsync()
	{
		// The Application loads the Scene in the background at the same time as the current Scene.
		//This is particularly good for creating loading screens. You could also load the Scene by build //number.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
		
	public void OnDestroy() {
		this.onClick.RemoveAllListeners();
	}
}
