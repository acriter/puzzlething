using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBehavior : MonoBehaviour {
	public int gridWidth = 3;
	public int gridHeight = 3;
	public GameObject myObj;

	public void Start() {
		this.SetUpBoardSquares();
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
				//GameObject newObj = Instantiate(myObj);
				//newObj.transform.SetParent(obj.transform);
				//RectTransform rt = newObj.GetComponent<RectTransform>();
				//rt.sizeDelta = new Vector2(size - 2, size - 2);
				//newObj.transform.localPosition = new Vector2(1, 1);
			}
		}
	}
}
