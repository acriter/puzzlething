using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightableButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private bool isSelected = false;
	private bool isSelectable = true;
	public Image highlightedImage;
	private CanvasGroup canvasGroup;
	private Color finishedColor = new Color(199f/255f, 151f/255f, 181f/255f, 0.5f);

	void Start() {
		this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
		this.highlightedImage = this.gameObject.GetComponent<Image>();
	}

	public void SetEditable(bool editable) {
		this.canvasGroup.alpha = editable ? 1f : 0f;
		this.isSelectable = editable;
	}


	public void OnPointerEnter(PointerEventData eventData) {
		if (!this.isSelectable) {
			return;
		}
			
		this.StopAllCoroutines();
		this.StartCoroutine(TweenToColor(a: this.finishedColor));
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (!this.isSelectable) {
			return;
		}
		Debug.Log("up!");
	}

	public void OnPointerDown(PointerEventData eventData) {
		if (!this.isSelectable) {
			return;
		}
		Debug.Log("down!");
	}

	public void OnPointerExit(PointerEventData eventData) {
		if (!this.isSelectable) {
			return;
		}

		this.StopAllCoroutines();
		this.StartCoroutine(TweenToColor(a: Color.clear));
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (!this.isSelectable) {
			return;
		}
		Debug.Log("click!");
	}

	private IEnumerator TweenToColor(Color a) {
		float elapsedTime = 0f;
		float totalTime = 0.15f;
		Color startingColor = this.highlightedImage.color;
		while (elapsedTime < totalTime) {
			float t = elapsedTime / totalTime;
			this.highlightedImage.color = Color.Lerp(startingColor, a, 2 * t * t * (3f - 2.5f * t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
