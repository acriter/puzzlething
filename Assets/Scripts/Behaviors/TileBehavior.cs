using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TileBehavior : DragHandler {

	private GamePiece piece;

	public void SetUpWithGamePiece(GamePiece piece) {
		this.piece = piece;

		foreach (KeyValuePair<int, int> pair in piece.squareInfo.Keys) {
			GameObject obj = Resources.Load("Prefabs/BoardSquare") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = new Vector2(BoardSquareBehavior.TILE_SIZE * pair.Key, BoardSquareBehavior.TILE_SIZE * pair.Value);

			BoardSquareBehavior sqBehavior = obj.GetComponent<BoardSquareBehavior>();
			sqBehavior.UpdateWithGameSquare(piece.squareInfo[pair]);
		}
		//gameObject.AddComponent<GridLayoutGroup>();
		//gameObject.AddComponent<CanvasGroup>();

		//foreach (KeyValuePair<int, int> pair in piece.squareInfo.Keys) {
		//	GameObject obj = new GameObject();
		//	obj.transform.SetParent(transform);
		//	obj.transform.localPosition = new Vector2(TILE_SIZE * pair.Key, TILE_SIZE * pair.Value);

		//	Image dupImage = Instantiate(tileImage);
		//	dupImage.transform.SetParent(obj.transform);
		//	RectTransform rt = dupImage.GetComponent<RectTransform>();
		//	rt.sizeDelta = new Vector2(TILE_SIZE, TILE_SIZE);
		//	dupImage.transform.localPosition = Vector2.zero;

		//	if (piece.squareInfo[pair].displayedNumber != 0) {
		//		GameObject textObj = new GameObject();
		//		textObj.transform.SetParent(obj.transform);
		//		textObj.transform.localPosition = new Vector2(75, 0);
		//		Text text = textObj.AddComponent<Text>();
		//		text.color = Color.black;
		//		text.fontStyle = FontStyle.Bold;
		//		text.font = font;
		//		text.text = piece.squareInfo[pair].displayedNumber.ToString();
		//		text.fontSize = 30;
		//	}
		//}
	}
}
