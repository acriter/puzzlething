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
}
