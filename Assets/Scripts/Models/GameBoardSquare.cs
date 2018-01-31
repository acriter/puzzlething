using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSquare {
	public Coordinate coordinate;
	List<GameCell> squares;

	public GameCell TopCell {
		get {
			if (this.squares.Count == 0) {
				return null;
			}

			return this.squares[this.squares.Count - 1];
		}
	}

	public GameBoardSquare(Coordinate coord) : this() {
		this.coordinate = coord;
	}

	public GameBoardSquare() {
		this.squares = new List<GameCell>();
	}

	public void AddGameCell(GameCell cell) {
		if (!this.squares.Contains(cell)) {
			this.squares.Add(cell);
		}
	}

	public void RemoveGameCell(GameCell cell) {
		if (this.squares.Contains(cell)) {
			this.squares.Remove(cell);
		}
	}
}
