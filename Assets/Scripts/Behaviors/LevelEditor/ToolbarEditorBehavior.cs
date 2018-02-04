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
	private ToolbarMode toolbarMode;

	
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

		this.TaskOnClick(tileToggle, true);
	}

	void TaskOnClick(Toggle toggle, bool selected) {
		selectedToggle = toggle;
		if (selected) {
			if (toggle == numberToggle) {
				toolbarMode = ToolbarMode.Number;
			} else if (toggle == borderToggle) {
				toolbarMode = ToolbarMode.Border;
			} else if (toggle == tileToggle) {
				toolbarMode = ToolbarMode.Tile;
			} else {
				Debug.Log("something really weird happened");
			}

			gridDelegate.DidSwitchToMode(toolbarMode);
		}
	}

	void Destroy() {
		numberToggle.onValueChanged.RemoveAllListeners();
		tileToggle.onValueChanged.RemoveAllListeners();
		borderToggle.onValueChanged.RemoveAllListeners();
	}

	
}
