using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class NumberInputBehavior : MonoBehaviour {
	public InputField inputField;
	public INumberInputHandler inputDelegate;
	private CanvasGroup canvasGroup;
	private bool isShowing;

	public void Start() {
		this.inputField.onEndEdit.AddListener(InputEditEnded);
		this.inputField.onValidateInput += delegate(string input, int charIndex, char addedChar) { return Validate(charIndex, addedChar); };
		this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
	}

	private char Validate(int charIndex, char addedChar) {
		if (charIndex > 1) {
			return '\0';
		}

		Regex regex = new Regex("^[0-9]$");
		if (regex.IsMatch("" + addedChar)) {
			return addedChar;
		} else {
			return '\0';
		};
	}

	public void Show() {
		this.canvasGroup.blocksRaycasts = true;
		this.StartCoroutine(FadeInOut(fadeIn:true));
	}

	private IEnumerator FadeInOut(bool fadeIn) {
		float totalTime = 0.3f;
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime / totalTime;
			if (fadeIn) {
				this.canvasGroup.alpha = Mathf.Lerp(0, 1, t * t);
			} else {
				this.canvasGroup.alpha = Mathf.Lerp(1, 0, t * t);
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

