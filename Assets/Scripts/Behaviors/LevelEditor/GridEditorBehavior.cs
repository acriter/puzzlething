using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface INumberInputHandler {
	void DidFinishTypingNumber(string number);
}

public class GridEditorBehavior : MonoBehaviour, IToolbarModeInterface {
	public NumberInputBehavior numberInputBehavior;
	private GameBoard gameBoard;
	private Dictionary<Coordinate, EditorGridBoardSquareBehavior> gameBoardDictionary;
	private ToolbarMode toolbarMode;

	private Coordinate currentlyEditedCoordinate = Coordinate.NullCoordinate();

	private GameObject currentlyDraggedObj;

	public void DidSwitchToMode(ToolbarMode mode) {
		this.currentlyEditedCoordinate = Coordinate.NullCoordinate();
		toolbarMode = mode;
		Debug.Log("switched to mode " + mode);
	}

	public void Start() {
		int START_SIZE = 6;
		this.gameBoardDictionary = new Dictionary<Coordinate, EditorGridBoardSquareBehavior>();

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
		if (this.numberInputBehavior != null) {
			this.numberInputBehavior.inputDelegate = this;
		}
	}

	private void SetUpBoardSquares() {
		float size = GameCellBehavior.TILE_SIZE;
		foreach (Coordinate coord in this.gameBoard.BoardMap.Keys) {
			GameObject obj = Resources.Load("Prefabs/EditorGridBoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(coord.row * size, coord.column * size);

			EditorGridBoardSquareBehavior sqBehavior = instantiatedObj.GetComponent<EditorGridBoardSquareBehavior>();

			this.gameBoardDictionary.Add(coord, sqBehavior);
			GameBoardSquare sq = this.gameBoard.BoardMap[coord];
			sqBehavior.Initialize(sq);
			sqBehavior.Deactivate();
		}
	}
}
