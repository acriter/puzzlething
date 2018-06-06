using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class GameBoard {
	private Dictionary<Coordinate, GameBoardSquare> boardMap;
	private Dictionary<Tile, Coordinate> tileMap;

	public Dictionary<Coordinate, GameBoardSquare> BoardMap {
		get {
			return this.boardMap;
		}
	}

	public GameBoard(string levelName) {
		this.boardMap = new Dictionary<Coordinate, GameBoardSquare>();
		this.tileMap = new Dictionary<Tile, Coordinate>();

		if (levelName == "") {
			for (int i = 0; i < 5; ++i) {
				for (int j = 0; j < 7; ++j) {
					if (i == 3 || j == 3) {
						continue;
					}
					GameBoardSquare sq = new GameBoardSquare();
					//GameCell cell = new GameCell();
					//sq.AddGameCell(cell);
					Coordinate coord = new Coordinate(i, j);
					sq.coordinate = coord;
					boardMap.Add(coord, sq);
				}
			}
		} else {
			string path = "Assets/Resources/" + levelName + ".json";

			//Read the text from directly from the test.txt file
			StreamReader reader = new StreamReader(path); 
			string jsonText = reader.ReadToEnd();
			reader.Close();
			JSONNode node = JSON.Parse(jsonText);
			foreach (JSONNode square in node["squares"]) {
				GameBoardSquare sq = GameBoardSquare.GameBoardSquareFromNode(square);
				boardMap.Add(sq.coordinate, sq);
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

	public JSONNode ToJson() {
		JSONArray parentNode = new JSONArray();
		foreach (KeyValuePair<Coordinate, GameBoardSquare> kvp in this.boardMap) {
			if (kvp.Value.TopCell == null) {
				continue;
			}
			JSONNode newNode = new JSONObject();
			newNode["row"] = kvp.Key.row;
			newNode["column"] = kvp.Key.column;
			newNode["number"] = kvp.Value.TopCell.displayedNumber;
			newNode["blockedBottom"] = kvp.Value.TopCell.blockedBottom;
			newNode["blockedTop"] = kvp.Value.TopCell.blockedTop;
			newNode["blockedLeft"] = kvp.Value.TopCell.blockedLeft;
			newNode["blockedRight"] = kvp.Value.TopCell.blockedRight;
			newNode["solid"] = kvp.Value.TopCell.solid;
			parentNode[-1] = newNode;
		}
		return parentNode;
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

	public bool PlayWasVictorious() {
		foreach (Coordinate coordinate in boardMap.Keys) {
			if (boardMap[coordinate].TopCell == null || !boardMap[coordinate].TopCell.solid) {
				return false;
			}

			//TODO: really slow - counts each coordinate in each area instead of just checking each area
			if (!this.AreaForSquare(coordinate).MeetsCriteria()) {
				Debug.Log("coordinate (" + coordinate.row + ", " + coordinate.column + ") does not meet criteria!");
				return false;
			}
		}

		return true;
	}

	public Area AreaForSquare(Coordinate coord) {
		Area area = new Area();
		//keep track of explored cells - don't go in circles
		List<Coordinate> exploredCoordinates = new List<Coordinate>();

		//keep track of cells we haven't fully explored
		List<Coordinate> coordinateStack = new List<Coordinate>();
		Coordinate nextCoordinate = coord;
		int x = coord.row;
		int y = coord.column;

		bool foundNextCell = false;

		int i = 0;
		bool keepGoing = true;
		while (keepGoing) {
			foundNextCell = false;
			//first look left, then down, then right, then up
			GameBoardSquare nextBoardSquare = this.boardMap[nextCoordinate];
			GameCell nextCell = nextBoardSquare.TopCell;
			x = nextCoordinate.row;
			y = nextCoordinate.column;
			//Debug.Log("now at (" + x + ", " + y + ")");
			if (nextCell != null) {
				area.AddCell(nextCell);
				if (!exploredCoordinates.Contains(nextCoordinate)) {
					exploredCoordinates.Add(nextCoordinate);
					coordinateStack.Add(nextCoordinate);
				}
				if (!nextCell.blockedLeft) {
					Coordinate left = new Coordinate(x - 1, y);
					if (this.ContainsCoordinate(left)) {
						if (this.BoardMap[left].TopCell != null && !this.BoardMap[left].TopCell.blockedRight && !exploredCoordinates.Contains(left)) {
							x = x - 1;
							nextCoordinate = left;
							foundNextCell = true;
						}
					}
				}
				if (!nextCell.blockedBottom && !foundNextCell) {
					Coordinate bottom = new Coordinate(x, y - 1);
					if (this.ContainsCoordinate(bottom)) {
						if (this.BoardMap[bottom].TopCell != null && !this.BoardMap[bottom].TopCell.blockedTop && !exploredCoordinates.Contains(bottom)) {
							y = y - 1;
							nextCoordinate = bottom;
							foundNextCell = true;
						}
					}
				}
				if (!nextCell.blockedRight && !foundNextCell) {
					Coordinate right = new Coordinate(x + 1, y);
					if (this.ContainsCoordinate(right)) {
						if (this.BoardMap[right].TopCell != null && !this.BoardMap[right].TopCell.blockedLeft && !exploredCoordinates.Contains(right)) {
							x = x + 1;
							nextCoordinate = right;
							foundNextCell = true;
						}
					}
				}
				if (!nextCell.blockedTop && !foundNextCell) {
					Coordinate top = new Coordinate(x, y + 1);
					if (this.ContainsCoordinate(top)) {
						if (this.BoardMap[top].TopCell != null && !this.BoardMap[top].TopCell.blockedBottom && !exploredCoordinates.Contains(top)) {
							y = y + 1;
							nextCoordinate = top;
							foundNextCell = true;
						}
					}
				}

				if (!foundNextCell) {
					coordinateStack.RemoveAt(coordinateStack.Count - 1);
					if (coordinateStack.Count == 0) {
						keepGoing = false;
					} else {
						nextCoordinate = coordinateStack[coordinateStack.Count - 1];
					}
				}				

				++i;
				if (i == 10) {
					keepGoing = false;
				}
			} else {
				//Debug.Log("ran into a null cell at (" + x + ", " + y + ")");
				return area;
			}
		}

		return area;
	}
}
