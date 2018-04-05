using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface INumberInputHandler {
	void DidFinishTypingNumber(string number);
}

public class GridEditorBehavior : MonoBehaviour, IToolbarModeInterface, INumberInputHandler {
	public NumberInputBehavior numberInputBehavior;
	private GameBoard gameBoard;
	private Dictionary<Coordinate, GridBoardSquareBehavior> gameBoardDictionary;
	private ToolbarMode toolbarMode;

	private Coordinate currentlyEditedCoordinate = Coordinate.NullCoordinate();

	private GameObject currentlyDraggedObj;

	public void DidSwitchToMode(ToolbarMode mode) {
		this.currentlyEditedCoordinate = Coordinate.NullCoordinate();
		toolbarMode = mode;
		Debug.Log("switched to mode " + mode);
	}

	public void OnPointerClick(PointerEventData eventData) {
		Vector2 clickPos = eventData.pressPosition;
		Vector2 releaseLocalPos = transform.InverseTransformPoint(clickPos);
		//base it on where the center of the tile is, not the bottom left
		int x = (int)Mathf.Floor(releaseLocalPos.x / GameCellBehavior.TILE_SIZE);
		int y = (int)Mathf.Floor(releaseLocalPos.y / GameCellBehavior.TILE_SIZE);

		Coordinate clickedCoord = new Coordinate(x, y);
		if (gameBoard.ContainsCoordinate(clickedCoord)) {
			GameBoardSquare square = gameBoard.BoardMap[clickedCoord];
			GridBoardSquareBehavior squareBehavior = gameBoardDictionary[clickedCoord];

			switch (toolbarMode) {
				case ToolbarMode.Border:
					break;
				case ToolbarMode.Number:
					if (square.TopCell != null) {
						this.currentlyEditedCoordinate = clickedCoord;
						this.numberInputBehavior.Show();
					}
					break;
				case ToolbarMode.Tile:
					if (!squareBehavior.isActive) {
						squareBehavior.Activate();
						if (square.TopCell == null) {
							GameCell cell = new GameCell();
							square.AddGameCell(cell);
							squareBehavior.Initialize(square);
						}
					} else if (square.TopCell != null) {
						square.RemoveGameCell(square.TopCell);
						squareBehavior.UpdateSquare();
					} else {
						squareBehavior.Deactivate();
					}
					break;
			}
		}
	}

	public void DidFinishTypingNumber(string number) {
		if (!this.currentlyEditedCoordinate.Equals(Coordinate.NullCoordinate())) {
			GameBoardSquare square = gameBoard.BoardMap[this.currentlyEditedCoordinate];
			GridBoardSquareBehavior squareBehavior = gameBoardDictionary[this.currentlyEditedCoordinate];
			int numValue;
			if (number == "") {
				numValue = 0;
			} else {
				numValue = int.Parse(number);
			}
			if (square.TopCell != null) {
				square.TopCell.displayedNumber = numValue;
				squareBehavior.UpdateSquare();
			} else {
				Debug.Log("there was no top cell...");
			}
		} else {
			Debug.Log("currently edited coordinate is null");
		}

		this.currentlyEditedCoordinate = Coordinate.NullCoordinate();
	}

	public void Start() {
		int START_SIZE = 6;
		this.gameBoardDictionary = new Dictionary<Coordinate, GridBoardSquareBehavior>();

		Dictionary<Coordinate, GameBoardSquare> gameBoardDict = new Dictionary<Coordinate, GameBoardSquare>();

		for (int i = 0; i < START_SIZE; ++i) {
			for (int j = 0; j < START_SIZE; ++j) {
				Coordinate coord = new Coordinate(i, j);
				GameBoardSquare square = new GameBoardSquare(coord);
				gameBoardDict.Add(coord, square);
			}
		}
			
		this.gameBoard = new GameBoard(gameBoardDict);
		this.SetUpBoardSquares();
		this.numberInputBehavior.inputDelegate = this;
	}

	private void SetUpBoardSquares() {
		float size = GameCellBehavior.TILE_SIZE;
		foreach (Coordinate coord in this.gameBoard.BoardMap.Keys) {
			GameObject obj = Resources.Load("Prefabs/GridBoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(coord.row * size, coord.column * size);

			GridBoardSquareBehavior sqBehavior = instantiatedObj.GetComponent<GridBoardSquareBehavior>();

			this.gameBoardDictionary.Add(coord, sqBehavior);
			GameBoardSquare sq = this.gameBoard.BoardMap[coord];
			sqBehavior.Initialize(sq);
			sqBehavior.Deactivate();
		}
	}
}
