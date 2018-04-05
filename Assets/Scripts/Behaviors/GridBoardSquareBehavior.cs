using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* This class represents a coordinate (square) on the game board, and all the tiles/information on that coordinate */
public class GridBoardSquareBehavior : BoardSquareBehavior {
	List<BoardSquareBehavior> stackedBoardSquares;

	public void Start() {
		//This is a list of the tiles that are on this square
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
				this.Deactivate();
			}
			//it's a slot to put a square but not an actual square
		} else {
			this.InitializeWithGameCell(topCell);
		}
	}

	public void Activate() {
		this.backgroundImage.color = Color.white;
	}

	public void Deactivate() {
		this.backgroundImage.color = new Color(0.1f, 0.1f, 0.1f);
	}
}
