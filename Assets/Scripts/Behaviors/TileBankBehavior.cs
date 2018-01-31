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

		Tile piece = new Tile();
		piece.squareInfo = new Dictionary<Coordinate, GameCell>();
		Coordinate bst = new Coordinate();
		bst.row = 0;
		bst.column = 0;
		GameCell sq = new GameCell();
		sq.displayedNumber = 7;
		piece.squareInfo.Add(bst, sq);

		Coordinate bst2 = new Coordinate();
		bst2.row = 0;
		bst2.column = 1;
		GameCell sq2 = new GameCell();
		sq2.displayedNumber = 5;
		piece.squareInfo.Add(bst2, sq2);
		beh.SetUpWithGamePiece(piece);


		GameObject gameObj2 = new GameObject("Tile Behavior 2");
		gameObj2.transform.SetParent(transform);
		TileBehavior beh2 = gameObj2.AddComponent<TileBehavior>();
		Tile piece2 = new Tile();
		piece2.squareInfo = new Dictionary<Coordinate, GameCell>();
		Coordinate bst3 = new Coordinate();
		bst3.row = 0;
		bst3.column = 0;
		GameCell sq3 = new GameCell();
		sq3.displayedNumber = 2;
		piece2.squareInfo.Add(bst3, sq3);

		Coordinate bst4 = new Coordinate();
		bst4.row = 1;
		bst4.column = 0;
		GameCell sq4 = new GameCell();
		piece2.squareInfo.Add(bst4, sq4);

		Coordinate bst5 = new Coordinate();
		bst5.row = 1;
		bst5.column = 1;
		GameCell sq5 = new GameCell();
		piece2.squareInfo.Add(bst5, sq5);

		beh2.SetUpWithGamePiece(piece2);
	}
}
