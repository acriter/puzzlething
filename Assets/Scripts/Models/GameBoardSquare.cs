using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

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

	public static GameBoardSquare GameBoardSquareFromNode(JSONNode square) {
		int row = square["row"];
		int column = square["column"];
		GameBoardSquare sq = new GameBoardSquare();
		Coordinate coord = new Coordinate(row, column);
		sq.coordinate = coord;
		//TODO: only add GameCell if square is not "greyed out" (add something in the json for that)
		GameCell gameCell = GameCell.GameCellFromJSONNode(square);
		sq.AddGameCell(gameCell);
		return sq;
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
