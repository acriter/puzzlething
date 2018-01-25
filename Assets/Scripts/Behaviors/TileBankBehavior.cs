using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TileBankBehavior : MonoBehaviour {
	List<TileBehavior> tiles;

	public void Start() {
		tiles = new List<TileBehavior>();

		GameObject gameObj = new GameObject("Tile Behavior");
		gameObj.transform.SetParent(transform);
		TileBehavior beh = gameObj.AddComponent<TileBehavior>();

		GamePiece piece = new GamePiece();
		piece.squareInfo = new Dictionary<BoardSquareThing, GameSquare>();
		BoardSquareThing bst = new BoardSquareThing();
		bst.row = 0;
		bst.column = 0;
		GameSquare sq = new GameSquare();
		sq.displayedNumber = 7;
		piece.squareInfo.Add(bst, sq);

		BoardSquareThing bst2 = new BoardSquareThing();
		bst2.row = 0;
		bst2.column = 1;
		GameSquare sq2 = new GameSquare();
		sq2.displayedNumber = 5;
		piece.squareInfo.Add(bst2, sq2);
		beh.SetUpWithGamePiece(piece);
	}
}
