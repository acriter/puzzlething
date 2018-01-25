using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileBehavior : DragHandler {

	private GamePiece piece;

	public void Awake() {
		//used for drag handler
		gameObject.AddComponent<CanvasGroup>();
	}

	public void SetUpWithGamePiece(GamePiece piece) {
		this.piece = piece;

		foreach (KeyValuePair<int, int> pair in piece.squareInfo.Keys) {
			GameObject obj = Resources.Load("Prefabs/BoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(BoardSquareBehavior.TILE_SIZE * pair.Key, BoardSquareBehavior.TILE_SIZE * pair.Value);

			BoardSquareBehavior sqBehavior = obj.GetComponent<BoardSquareBehavior>();
			sqBehavior.UpdateWithGameSquare(piece.squareInfo[pair]);
		}
	}
}
