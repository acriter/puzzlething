using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IHighlightableButtonOwnerDelegate {
	void DidPressHighlightableButton(HighlightableButton button);
}

public class HighlightableButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	private bool isSelected = false;
	public bool isSelectable = true;
	public Image highlightedImage;
	private CanvasGroup canvasGroup;
	public IHighlightableButtonOwnerDelegate owner;
	private Color finishedColor = new Color(199f/255f, 151f/255f, 181f/255f, 0.5f);
	private Color downColor = new Color(199f/255f, 151f/255f, 181f/255f, 0.8f);

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

		this.StopAllCoroutines();
		this.StartCoroutine(TweenToColor(a: Color.clear));
	}

	public void OnPointerDown(PointerEventData eventData) {
		if (!this.isSelectable) {
			return;
		}

		this.StopAllCoroutines();
		this.StartCoroutine(TweenToColor(a: this.downColor));
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

		if (this.owner != null) {
			this.owner.DidPressHighlightableButton(this);
		}
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
