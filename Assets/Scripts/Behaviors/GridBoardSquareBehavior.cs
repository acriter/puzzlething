using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridBoardSquareBehavior : BoardSquareBehavior {
	List<BoardSquareBehavior> stackedBoardSquares;

	public void Start() {
		this.stackedBoardSquares = new List<BoardSquareBehavior>();
	}

	public void AddGameSquare(BoardSquareBehavior square) {
		this.stackedBoardSquares.Add(square);
	}

	public void RemoveGameSquare(BoardSquareBehavior square) {
		this.stackedBoardSquares.Remove(square);
	}

	public void Initialize(GameBoardSquare square) {
		GameCell topCell = square.TopCell;
		if (topCell == null) {
			if (!square.isActive) {
				Debug.Log("not active");
			}
			//it's a slot to put a square but not an actual square
		} else {
			this.UpdateWithGameCell(topCell);
		}
	}
}
