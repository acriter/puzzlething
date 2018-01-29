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
		piece.squareInfo = new Dictionary<Coordinate, GameSquare>();
		Coordinate bst = new Coordinate();
		bst.row = 0;
		bst.column = 0;
		GameSquare sq = new GameSquare();
		sq.displayedNumber = 7;
		piece.squareInfo.Add(bst, sq);

		Coordinate bst2 = new Coordinate();
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
		piece2.squareInfo = new Dictionary<Coordinate, GameSquare>();
		Coordinate bst3 = new Coordinate();
		bst3.row = 0;
		bst3.column = 0;
		GameSquare sq3 = new GameSquare();
		sq3.displayedNumber = 2;
		piece2.squareInfo.Add(bst3, sq3);

		Coordinate bst4 = new Coordinate();
		bst4.row = 1;
		bst4.column = 0;
		GameSquare sq4 = new GameSquare();
		piece2.squareInfo.Add(bst4, sq4);

		Coordinate bst5 = new Coordinate();
		bst5.row = 1;
		bst5.column = 1;
		GameSquare sq5 = new GameSquare();
		piece2.squareInfo.Add(bst5, sq5);

		beh2.SetUpWithGamePiece(piece2);
	}
}
