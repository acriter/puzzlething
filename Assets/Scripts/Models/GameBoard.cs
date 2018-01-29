using System.Collections;
using System.Collections.Generic;

public class GameBoard {
	private Dictionary<Coordinate, GameBoardSquare> boardMap;
	private Dictionary<Tile, Coordinate> tileMap;

	public Dictionary<Coordinate, GameBoardSquare> BoardMap {
		get {
			return this.boardMap;
		}
	}

	public GameBoard() {
		this.boardMap = new Dictionary<Coordinate, GameBoardSquare>();
		this.tileMap = new Dictionary<Tile, Coordinate>();

		for (int i = 0; i < 5; ++i) {
			for (int j = 0; j < 7; ++j) {
				if (i == 3 || j == 3) {
					continue;
				}
				GameBoardSquare sq = new GameBoardSquare();
				sq.attachedToGrid = true;
				Coordinate coord = new Coordinate(i, j);
				boardMap.Add(coord, sq);
			}
		}
	}

	public GameBoard(Dictionary<Coordinate, GameBoardSquare> board) {
		this.boardMap = board;
		this.tileMap = new Dictionary<Tile, Coordinate>();
	}

	public bool ContainsCoordinate(Coordinate coord) {
		return boardMap.ContainsKey(coord);
	}

	public void MoveTileToSquare(Tile t, Coordinate coord) {
		if (this.tileMap.ContainsKey(t)) {
			int tileRow = this.tileMap[t].row;
			int tileCol = this.tileMap[t].column;
			foreach (Coordinate co in t.squareInfo.Keys) {
				Coordinate c = new Coordinate(co.row + tileRow, co.column + tileCol);
				GameBoardSquare square = this.boardMap[c];
				square.RemoveGameCell(t.squareInfo[co]);
			}
		}

		foreach (Coordinate co in t.squareInfo.Keys) {
			Coordinate c = new Coordinate(coord.row + co.row, coord.column + co.column);
			GameBoardSquare square = this.boardMap[c];
			square.AddGameCell(t.squareInfo[co]);
		}

		this.tileMap[t] = coord;
	}
}
