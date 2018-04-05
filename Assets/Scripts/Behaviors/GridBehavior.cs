using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* This class manages the board grid, i.e. the thing that the player needs to put stuff on and manipulate to beat the level */
public class GridBehavior : MonoBehaviour, IDropHandler {
	private GameBoard gameBoard;
	private Dictionary<Coordinate, GridBoardSquareBehavior> gameBoardDictionary;
	public ITileDroppedDelegate dropTileDelegate;
	private GameObject currentlyDraggedObj;
	

	public void Start() {
	}

	public void InitializeWithLevel(string levelToLoad) {
		this.gameBoard = new GameBoard(levelToLoad);
		this.SetUpBoardSquares();
	}

	public void OnDrop(PointerEventData eventData) {
		GameObject draggedItem = DragHandler.draggedItem;
		if (draggedItem == null) {
			return;
		}

		currentlyDraggedObj = draggedItem;

		Vector2 releaseLocalPos = transform.InverseTransformPoint(currentlyDraggedObj.transform.position);
		releaseLocalPos += new Vector2(GameCellBehavior.TILE_SIZE / 2.0f, GameCellBehavior.TILE_SIZE / 2.0f);
		//base it on where the center of the tile is, not the bottom left
		int x = (int)Mathf.Floor(releaseLocalPos.x / GameCellBehavior.TILE_SIZE);
		int y = (int)Mathf.Floor(releaseLocalPos.y / GameCellBehavior.TILE_SIZE);

		if (!this.ValidatePositionAndUpdateGrid(x, y)) {
			return;
		}

		currentlyDraggedObj.transform.SetParent(transform);
		this.dropTileDelegate.DidDropTile(draggedItem);
		this.StartCoroutine(SnapPlacedPieceToGrid(x, y));
	}

	private bool ValidatePositionAndUpdateGrid(int x, int y) {
		TileBehavior tileBeh = currentlyDraggedObj.GetComponent<TileBehavior>();
		if (tileBeh == null) {
			return false;
		}

		//TODO: move this to the model layer
		Tile piece = tileBeh.piece;
		foreach(Coordinate t in piece.squareInfo.Keys) {
			Coordinate newCoord = new Coordinate(x + t.row, y + t.column);
			if (!this.gameBoard.ContainsCoordinate(newCoord)) {
				return false;
			}
		}

		this.gameBoard.MoveTileToSquare(piece, new Coordinate(x, y));
		return true;
	}

	public bool IsLevelSolved() {
		return this.gameBoard.PlayWasVictorious();
	}

	public bool IsTilePlaced(TileBehavior tile) {
		return tile.transform.parent.Equals(transform);
	}

	private IEnumerator SnapPlacedPieceToGrid(int x, int y) {
		float elapsedTime = 0f;
		float totalTime = 0.15f;
		Vector2 startingPos = currentlyDraggedObj.transform.localPosition;
		Vector2 endingPos = new Vector2(x * GameCellBehavior.TILE_SIZE, y * GameCellBehavior.TILE_SIZE);
		while (elapsedTime < totalTime) {
			float t = elapsedTime / totalTime;
			currentlyDraggedObj.transform.localPosition = Vector2.Lerp(startingPos, endingPos, 2 * t * t * (3f - 2.5f * t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

	private void SetUpBoardSquares() {
		float size = GameCellBehavior.TILE_SIZE;
		foreach (Coordinate coord in this.gameBoard.BoardMap.Keys) {
			GameObject obj = Resources.Load("Prefabs/GridBoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(coord.row * size, coord.column * size);

			GridBoardSquareBehavior sqBehavior = instantiatedObj.GetComponent<GridBoardSquareBehavior>();
			GameBoardSquare sq = this.gameBoard.BoardMap[coord];
			sqBehavior.Initialize(sq);
		}
	}
}
