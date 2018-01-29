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


		GameObject gameObj2 = new GameObject("Tile Behavior 2");
		gameObj2.transform.SetParent(transform);
		TileBehavior beh2 = gameObj2.AddComponent<TileBehavior>();
		GamePiece piece2 = new GamePiece();
		piece2.squareInfo = new Dictionary<BoardSquareThing, GameSquare>();
		BoardSquareThing bst3 = new BoardSquareThing();
		bst3.row = 0;
		bst3.column = 0;
		GameSquare sq3 = new GameSquare();
		sq3.displayedNumber = 2;
		piece2.squareInfo.Add(bst3, sq3);

		BoardSquareThing bst4 = new BoardSquareThing();
		bst4.row = 1;
		bst4.column = 0;
		GameSquare sq4 = new GameSquare();
		piece2.squareInfo.Add(bst4, sq4);

		BoardSquareThing bst5 = new BoardSquareThing();
		bst5.row = 1;
		bst5.column = 1;
		GameSquare sq5 = new GameSquare();
		piece2.squareInfo.Add(bst5, sq5);

		beh2.SetUpWithGamePiece(piece2);
	}
}
