using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberInputBehavior : MonoBehaviour {
	public InputField inputField;
	public INumberInputHandler inputDelegate;
	private CanvasGroup canvasGroup;
	private bool isShowing;

	public void Start() {
		this.inputField.onEndEdit.AddListener(InputEditEnded);
		this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
	}

	public void Show() {
		this.canvasGroup.blocksRaycasts = true;
		this.StartCoroutine(FadeInOut(fadeIn:true));
	}

	private IEnumerator FadeInOut(bool fadeIn) {
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime;
			if (fadeIn) {
				this.canvasGroup.alpha = Mathf.Lerp(0, 1, t * t);
			} else {
				this.canvasGroup.alpha = Mathf.Lerp(1, 0, 1 - t * t);
			}
			yield return null;
		}
	}

	public void Hide() {
		this.StartCoroutine(FadeInOut(fadeIn:false));
		this.canvasGroup.blocksRaycasts = false;
	}

	private void InputEditEnded(string input) {
		this.Hide();
		this.inputDelegate.DidFinishTypingNumber(input);
	}

	public void OnDestroy() {
		this.inputField.onEndEdit.RemoveAllListeners();
	}
}

