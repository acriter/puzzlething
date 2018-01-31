using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public Area AreaForSquare(Coordinate coord) {
		Area area = new Area();

		//keep track of explored cells - don't go in circles
		List<GameCell> exploredCells = new List<GameCell>();

		//keep track of cells we haven't fully explored
		List<GameCell> cellStack = new List<GameCell>();
		GameCell cell = this.boardMap[coord].TopCell;
		int x = coord.row;
		int y = coord.column;

		bool keepGoing = true;
		while (keepGoing) {
			//first look left, then down, then right, then up
			if (cell != null) {
				area.AddCell(cell);
				exploredCells.Add(cell);
				cellStack.Add(cell);
				if (!cell.blockedLeft) {
					Coordinate left = new Coordinate(x - 1, y);
					if (this.ContainsCoordinate(left)) {
						if (!this.BoardMap[left].blockedRight && !exploredCells.Contains(this.BoardMap[left])) {
							x -= 1;
							cell = this.BoardMap[left].TopCell;
						}
					}
				} else if (!cell.blockedBottom) {
					Coordinate bottom = new Coordinate(x, y - 1);
					if (this.ContainsCoordinate(bottom)) {
						if (!this.BoardMap[bottom].blockedTop && !exploredCells.Contains(this.BoardMap[bottom])) {
							y -= 1;
							cell = this.BoardMap[bottom].TopCell;
						}
					}
				} else if (!cell.blockedRight) {
					Coordinate right = new Coordinate(x + 1, y);
					if (this.ContainsCoordinate(right)) {
						if (!this.BoardMap[right].blockedLeft && !exploredCells.Contains(this.BoardMap[right])) {
							x += 1;
							cell = this.BoardMap[right].TopCell;
						}
					}
				} else if (!cell.blockedTop) {
					Coordinate top = new Coordinate(x, y + 1);
					if (this.ContainsCoordinate(top)) {
						if (!this.BoardMap[top].blockedBottom && !exploredCells.Contains(this.BoardMap[top])) {
							y += 1;
							cell = this.BoardMap[top].TopCell;
						}
					}
				} else {
					cellStack.RemoveAt(cellStack.Count - 1);
					if (cellStack.Count == 0) {
						keepGoing = false;
					} else {
						cell = cellStack[cellStack.Count - 1];
					}
				}
			} else {
				Debug.Log("ran into a null cell... not sure what to do");
			}
		}

		return area;
	}

	//public Area AreaForGameCell(GameCell cell) {
	//	List<GameCell> exploredCells = new List<GameCell>();
	//	return null;
	//}
}
