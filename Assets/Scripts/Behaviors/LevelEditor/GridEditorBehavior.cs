using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEditorBehavior : MonoBehaviour, IToolbarModeInterface {
	public GameObject myObj;

	private GameBoard gameBoard;
	private Dictionary<Coordinate, GridBoardSquareBehavior> gameBoardDictionary;
	private ToolbarMode toolbarMode;

	private GameObject currentlyDraggedObj;

	public void DidSwitchToMode(ToolbarMode mode) {
		toolbarMode = mode;
	}

	public void Start() {
		this.gameBoard = new GameBoard();
		this.SetUpBoardSquares();
	}

	private void SetUpBoardSquares() {
		float size = BoardSquareBehavior.TILE_SIZE;
		foreach (Coordinate coord in this.gameBoard.BoardMap.Keys) {
			GameObject obj = Resources.Load("Prefabs/GridBoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(coord.row * size, coord.column * size);

			GridBoardSquareBehavior sqBehavior = obj.GetComponent<GridBoardSquareBehavior>();
			GameBoardSquare sq = this.gameBoard.BoardMap[coord];
			sqBehavior.UpdateWithGameSquare(sq.TopCell);
		}
	}
}
