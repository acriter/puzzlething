using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardSquare {
	public Coordinate coordinate;

	public List<GameCell> cells;

	public GameCell TopCell {
		get {
			if (this.cells.Count == 0) {
				return null;
			}

			return this.cells[this.cells.Count - 1];
		}
	}

	public GameBoardSquare(Coordinate coord) : this() {
		this.coordinate = coord;
	}

	public GameBoardSquare() {
		this.cells = new List<GameCell>();
	}

	public void AddGameCell(GameCell cell) {
		if (!this.cells.Contains(cell)) {
			this.cells.Add(cell);
		}
	}

	public void RemoveGameCell(GameCell cell) {
		if (this.cells.Contains(cell)) {
			this.cells.Remove(cell);
		}
	}
}
