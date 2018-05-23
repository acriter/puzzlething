using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface INumberInputHandler {
	void DidFinishTypingNumber(string number);
}

public class EditorGridBoardSquareBehavior : GridBoardSquareBehavior, IPointerClickHandler, INumberInputHandler {

	//Only used in edit mode - is this square part of the puzzle?
	public bool isActive;

	public void DidFinishTypingNumber(string number) {
		int numValue;
		if (number == "") {
			numValue = 0;
		} else {
			numValue = int.Parse(number);
		}
		if (boardSquare.TopCell != null) {
			boardSquare.TopCell.displayedNumber = numValue;
			this.UpdateSquare();
		} else {
			Debug.Log("there was no top cell...");
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (this.boardSquare.TopCell != null) {
				NumberInputBehavior input = GameObject.FindObjectOfType<NumberInputBehavior>();
				if (input != null) {
					input.inputDelegate = this;
					input.Show(boardSquare.TopCell.displayedNumber == 0 ? "" : "" + boardSquare.TopCell.displayedNumber);
				}
			}
		} else if (eventData.button == PointerEventData.InputButton.Left) {
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
		}
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
