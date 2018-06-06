using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileHolderBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public const int TILE_HOLDER_WIDTH = 300;
	public const int TILE_HOLDER_HEIGHT = 100;

	public Image backgroundImage;
	private int currentBackground = 1;
	private int MAX_BACKGROUND = 9;

	void Start() {
		this.SetBackground(1);
	}

	public void OnPointerEnter(PointerEventData data) {
		this.StartCoroutine("ChangeBackground");
	}

	public void OnPointerExit(PointerEventData data) {
		this.StopCoroutine("ChangeBackground");
		this.SetBackground(1);
	}

	private void SetBackground(int backgroundNum) {
		this.backgroundImage.sprite = Resources.Load<Sprite>("cloud" + backgroundNum);
	}

	private IEnumerator ChangeBackground() {
		while (true) {
			yield return new WaitForSeconds(0.5f);
			this.currentBackground = (this.currentBackground % 9) + 1;
			this.SetBackground(this.currentBackground);
		}
	}
}
