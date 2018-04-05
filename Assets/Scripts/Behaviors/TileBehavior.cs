using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileBehavior : DragHandler {

	public Tile piece;

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

			GameCellBehavior cellBehavior = instantiatedObj.GetComponent<GameCellBehavior>();
			cellBehavior.InitializeWithGameCell(piece.squareInfo[pair]);
		}
	}
}
