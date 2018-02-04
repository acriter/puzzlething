using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ToolbarMode {
	Tile,
	Number,
	Border
}

public interface IToolbarModeInterface {
	void DidSwitchToMode(ToolbarMode mode);
}

public class ToolbarEditorBehavior : MonoBehaviour {
	public Toggle numberToggle, tileToggle, borderToggle;
	public ToggleGroup toggleGroup;
	public IToolbarModeInterface gridDelegate;
	private Toggle selectedToggle;

	
	// Use this for initialization
	void Start () {
		GridEditorBehavior del = GameObject.FindObjectOfType<GridEditorBehavior>();
		if (del == null) {
			Debug.LogWarning("couldn't find grid editor behavior");
		}
		gridDelegate = del;
		numberToggle.onValueChanged.AddListener((bool selected) => TaskOnClick(numberToggle, selected));
		tileToggle.onValueChanged.AddListener((bool selected) => TaskOnClick(tileToggle, selected));
		borderToggle.onValueChanged.AddListener((bool selected) => TaskOnClick(borderToggle, selected));
	}

	void TaskOnClick(Toggle Toggle, bool selected) {
		Debug.Log((selected ? "clicked" : "unclicked") + " a button");
		selectedToggle = Toggle;
		if (Toggle == numberToggle) {
			
		}
	}

	void Destroy() {
		numberToggle.onValueChanged.RemoveAllListeners();
		tileToggle.onValueChanged.RemoveAllListeners();
		borderToggle.onValueChanged.RemoveAllListeners();
	}

	
}
