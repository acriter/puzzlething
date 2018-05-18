using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorGridBoardSquareBehavior : GridBoardSquareBehavior, IPointerClickHandler {
	
	//Only used in edit mode - is this square part of the puzzle?
	public bool isActive;

	public void OnPointerClick(PointerEventData pointerEventData) {
		//Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
		Debug.Log(name + " Game Object Clicked!");
	}

	//These are only used in edit mode (to show that this square will not be part of the puzzle)
	public void Activate() {
		CanvasGroup canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1f;
		this.isActive = true;
	}

	public void Deactivate() {
		CanvasGroup canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0.1f;
		this.isActive = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
