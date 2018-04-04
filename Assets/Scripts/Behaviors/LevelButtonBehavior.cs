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
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
		
	public void OnDestroy() {
		this.onClick.RemoveAllListeners();
	}
}
