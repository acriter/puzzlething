using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This class represents a coordinate (square) on the game board, and all the tiles/information on that coordinate */
public class GridBoardSquareBehavior : MonoBehaviour {
	public GameBoardSquare boardSquare;
	List<GameCellBehavior> stackedGameCells;

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
}
