using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridBehavior : DropHandler {
	public int gridWidth = 3;
	public int gridHeight = 3;
	public GameObject myObj;

	public void Start() {
		this.SetUpBoardSquares();
	}

	private void SetUpBoardSquares() {
		float size = BoardSquareBehavior.TILE_SIZE;
		for (int i = 0; i < gridWidth; ++i) {
			for(int j = 0; j < gridHeight; ++j) {
				GameObject obj = new GameObject();
				obj.AddComponent<BoardSquareBehavior>();
				obj.transform.SetParent(transform);
				obj.transform.localPosition = new Vector2(i * size, j * size);
				GameObject newObj = Instantiate(myObj);
				newObj.transform.SetParent(obj.transform);
				RectTransform rt = newObj.GetComponent<RectTransform>();
				rt.sizeDelta = new Vector2(size - 2, size - 2);
				newObj.transform.localPosition = new Vector2(1, 1);
			}
		}
	}
}
