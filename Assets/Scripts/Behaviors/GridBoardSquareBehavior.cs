using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* This class represents a coordinate (square) on the game board, and all the tiles/information on that coordinate */
public class GridBoardSquareBehavior : MonoBehaviour {
	public GameBoardSquare boardSquare;
	List<GameCellBehavior> stackedGameCells;

	//Only used in edit mode - is this square part of the puzzle?
	public bool isActive;

	public void Awake() {
		//This is a list of the tiles that are on this square
		this.stackedGameCells = new List<GameCellBehavior>();
	}

	public void AddGameCell(GameCellBehavior cell) {
		this.stackedGameCells.Add(cell);
	}

	public void RemoveGameCell(GameCellBehavior cell) {
		this.stackedGameCells.Remove(cell);
	}

	public void Initialize(GameBoardSquare square) {
		this.boardSquare = square;
		GameCell topCell = square.TopCell;
		if (topCell != null) {
			//TODO: code is copied from TileBehavior
			GameObject obj = Resources.Load("Prefabs/GameCell") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = Vector2.zero;

			GameCellBehavior cellBehavior = instantiatedObj.GetComponent<GameCellBehavior>();
			cellBehavior.InitializeWithGameCell(topCell);
			this.AddGameCell(cellBehavior);
		}
	}

	//Update based on information stored in the model (boardSquare)
	public void UpdateSquare() {
		List<GameCellBehavior> cellsToRemove = new List<GameCellBehavior>();
		foreach (GameCellBehavior cellBehavior in this.stackedGameCells) {
			if (!this.boardSquare.cells.Contains(cellBehavior.cell)) {
				cellsToRemove.Add(cellBehavior);
			} else {
				cellBehavior.UpdateCell();
			}
		}

		foreach (GameCellBehavior cellBehaviorToDestroy in cellsToRemove) {
			this.RemoveGameCell(cellBehaviorToDestroy);
			Destroy(cellBehaviorToDestroy.gameObject);
		}
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
