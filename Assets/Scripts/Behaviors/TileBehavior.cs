using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileBehavior : DragHandler {

	public Tile piece;
	private bool maximizedForDragging = false;

	public void StartedBeingDragged() {
		//just check the hierarchy for this idk
		if (GetComponentInParent<TileBankBehavior>() != null) {
			this.Grow();
		}
	}

	public void StoppedBeingDragged() {
		Transform parent = transform.parent;
		if (parent.GetComponent<GridBehavior>() != null) {
			//got put onto the grid
		} else {
			this.Shrink();
		}
	}

	private void Grow() {
		StopCoroutine("ChangeSize");
		StartCoroutine(ChangeSize(true));
	}

	private void Shrink() {
		StopCoroutine("ChangeSize");
		StartCoroutine(ChangeSize(false));
	}

	private IEnumerator ChangeSize(bool grow) {
		Vector3 scaleEnd = Vector3.one * (grow ? 2 : 1);
		Vector3 scaleStart = this.transform.localScale;
		float totalTime = 0.2f;
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime / totalTime;
			this.transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, t * t * t * t);
			yield return null;
		}
	}

	public void Awake() {
		//used for drag handler
		gameObject.AddComponent<CanvasGroup>();
	}

	public void SetUpWithGamePiece(Tile piece) {
		this.piece = piece;

		foreach (Coordinate pair in piece.squareInfo.Keys) {
			GameObject obj = Resources.Load("Prefabs/GameCell") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(GameCellBehavior.TILE_SIZE * pair.row, GameCellBehavior.TILE_SIZE * pair.column);
			instantiatedObj.transform.localScale = Vector3.one;

			GameCellBehavior cellBehavior = instantiatedObj.GetComponent<GameCellBehavior>();
			cellBehavior.InitializeWithGameCell(piece.squareInfo[pair]);
		}
	}
}
