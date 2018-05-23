using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridEditorBehavior : MonoBehaviour, IToolbarModeInterface {
	public NumberInputBehavior numberInputBehavior;
	private GameBoard gameBoard;
	private Dictionary<Coordinate, EditorGridBoardSquareBehavior> gameBoardDictionary;

	private GameObject currentlyDraggedObj;

	private bool miniSize = false;
	private int gridCount = 6;

	public void DidSwitchToMode(ToolbarMode mode) {
		Debug.Log("switched to mode " + mode);
	}

	public void ConfigureWithSize(int size, bool mini) {
		this.gridCount = size;
		this.miniSize = mini;
	}

	public void Start() {
		this.gameBoardDictionary = new Dictionary<Coordinate, EditorGridBoardSquareBehavior>();

		Dictionary<Coordinate, GameBoardSquare> gameBoardDict = new Dictionary<Coordinate, GameBoardSquare>();

		for (int i = 0; i < this.gridCount; ++i) {
			for (int j = 0; j < this.gridCount; ++j) {
				Coordinate coord = new Coordinate(i, j);
				GameBoardSquare square = new GameBoardSquare(coord);
				gameBoardDict.Add(coord, square);
			}
		}
			
		this.gameBoard = new GameBoard(gameBoardDict);
		this.SetUpBoardSquares();
	}

	private void SetUpBoardSquares() {
		float size = this.miniSize ? GameCellBehavior.MINI_TILE_SIZE : GameCellBehavior.TILE_SIZE;
		foreach (Coordinate coord in this.gameBoard.BoardMap.Keys) {
			GameObject obj = Resources.Load("Prefabs/EditorGridBoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			
			instantiatedObj.transform.localScale = new Vector2(size / GameCellBehavior.TILE_SIZE, size / GameCellBehavior.TILE_SIZE);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(coord.row * size, coord.column * size);

			EditorGridBoardSquareBehavior sqBehavior = instantiatedObj.GetComponent<EditorGridBoardSquareBehavior>();
			sqBehavior.ConfigureWithSize(this.miniSize);

			this.gameBoardDictionary.Add(coord, sqBehavior);
			GameBoardSquare sq = this.gameBoard.BoardMap[coord];
			sqBehavior.Initialize(sq);
			sqBehavior.Deactivate();
		}
	}
}
