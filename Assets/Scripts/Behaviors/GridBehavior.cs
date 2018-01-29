using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridBehavior : MonoBehaviour, IDropHandler {
	public int gridWidth = 3;
	public int gridHeight = 3;
	public GameObject myObj;

	private GameObject currentlyDraggedObj;
	

	public void Start() {
		this.SetUpBoardSquares();
	}

	public void OnDrop(PointerEventData eventData) {
		GameObject draggedItem = DragHandler.draggedItem;
		if (draggedItem != null) {
			draggedItem.transform.SetParent(transform);
		}

		currentlyDraggedObj = draggedItem;
		
		Vector2 releaseLocalPos = transform.InverseTransformPoint(currentlyDraggedObj.transform.position);
		releaseLocalPos += new Vector2(BoardSquareBehavior.TILE_SIZE / 2.0f, BoardSquareBehavior.TILE_SIZE / 2.0f);
		//base it on where the center of the tile is, not the bottom left
		int x = (int)releaseLocalPos.x / BoardSquareBehavior.TILE_SIZE;
		int y = (int)releaseLocalPos.y / BoardSquareBehavior.TILE_SIZE;		
		this.StartCoroutine(SnapPlacedPieceToGrid(x, y));
	}

	private IEnumerator SnapPlacedPieceToGrid(int x, int y) {
		float elapsedTime = 0f;
		float totalTime = 0.15f;
		Vector2 startingPos = currentlyDraggedObj.transform.localPosition;
		Vector2 endingPos = new Vector2(x * BoardSquareBehavior.TILE_SIZE, y * BoardSquareBehavior.TILE_SIZE);
		while (elapsedTime < totalTime) {
			float t = elapsedTime / totalTime;
			currentlyDraggedObj.transform.localPosition = Vector2.Lerp(startingPos, endingPos, 2 * t * t * (3f - 2.5f * t));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

	private void SetUpBoardSquares() {
		float size = BoardSquareBehavior.TILE_SIZE;
		for (int i = 0; i < gridWidth; ++i) {
			for (int j = 0; j < gridHeight; ++j) {
				GameObject obj = Resources.Load("Prefabs/BoardSquare") as GameObject;
				GameObject instantiatedObj = GameObject.Instantiate(obj);
				instantiatedObj.transform.SetParent(transform);
				instantiatedObj.transform.localPosition = new Vector2(i * size, j * size);

				BoardSquareBehavior sqBehavior = obj.GetComponent<BoardSquareBehavior>();
				GameSquare square = new GameSquare();
				square.attachedToGrid = true;
				sqBehavior.UpdateWithGameSquare(square);
			}
		}
	}
}
