using System.Collections;
using System.Collections.Generic;

public class GameBoard {
	private Dictionary<Coordinate, GameSquare> boardMap;

	public Dictionary<Coordinate, GameSquare> BoardMap {
		get {
			return this.boardMap;
		}
	}

	public GameBoard() {
		this.boardMap = new Dictionary<Coordinate, GameSquare>();

		for (int i = 0; i < 5; ++i) {
			for (int j = 0; j < 7; ++j) {
				if (i == 3 || j == 3) {
					continue;
				}
				GameSquare sq = new GameSquare();
				sq.attachedToGrid = true;
				Coordinate coord = new Coordinate(i, j);
				boardMap.Add(coord, sq);
			}
		}
	}

	public GameBoard(Dictionary<Coordinate, GameSquare> board) {
		this.boardMap = board;
	}

	public bool ContainsCoordinate(Coordinate coord) {
		return boardMap.ContainsKey(coord);
	}

	public void UpdateBoardAtSquare(Coordinate coord, GameSquare square) {
		if (this.ContainsCoordinate(coord)) {
			boardMap[coord] = square;
		}
	}
}
