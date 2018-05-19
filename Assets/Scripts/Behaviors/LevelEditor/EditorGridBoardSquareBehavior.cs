using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditorGridBoardSquareBehavior : GridBoardSquareBehavior, IPointerClickHandler {

	//Only used in edit mode - is this square part of the puzzle?
	public bool isActive;

	public void OnPointerClick(PointerEventData eventData) {
		if (!this.isActive) {
			this.Activate();
			if (this.boardSquare.TopCell == null) {
				GameCell cell = new GameCell();
				this.boardSquare.AddGameCell(cell);
				this.Initialize(this.boardSquare);
			}
		} else if (this.boardSquare.TopCell != null) {
			this.boardSquare.RemoveGameCell(this.boardSquare.TopCell);
			this.UpdateSquare();
		} else {
			this.Deactivate();
		}
		Vector2 clickPos = eventData.pressPosition;
		Vector2 releaseLocalPos = transform.InverseTransformPoint(clickPos);
		Debug.Log(name + " Game Object Clicked!");
	}

	public override void AddGameCell(GameCellBehavior cell) {
		this.stackedGameCells.Add(cell);
		cell.EnableCellEditing();
	}

	public override void RemoveGameCell(GameCellBehavior cell) {
		this.stackedGameCells.Remove(cell);
		cell.EnableCellEditing();
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
}
