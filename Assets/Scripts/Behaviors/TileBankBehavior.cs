using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TileBankBehavior : MonoBehaviour {
	List<TileBehavior> tiles;

	//TODO: remove
	public Image tileImage;
	public Font font;

	public void Start() {
		tiles = new List<TileBehavior>();

		GameObject gameObj = new GameObject("Tile Behavior");
		gameObj.transform.SetParent(transform);
		gameObj.AddComponent<TileBehavior>();

		GamePiece piece = new GamePiece();
		piece.squareInfo = new Dictionary<KeyValuePair<int, int>, GameSquare>();
		KeyValuePair<int, int> kvp = new KeyValuePair<int, int>(0, 0);
		GameSquare sq = new GameSquare();
		piece.squareInfo.Add(kvp, sq);

		kvp = new KeyValuePair<int, int>(0, 1);
		sq = new GameSquare();
		sq.displayedNumber = 5;
		piece.squareInfo.Add(kvp, sq);
		gameObj.GetComponent<TileBehavior>().SetUpWithGamePiece(piece);


		gameObj = new GameObject("Tile Behavior");
		gameObj.transform.SetParent(transform);
		gameObj.AddComponent<TileBehavior>();

		piece = new GamePiece();
		piece.squareInfo = new Dictionary<KeyValuePair<int, int>, GameSquare>();
		kvp = new KeyValuePair<int, int>(0, 0);
		sq = new GameSquare();
		sq.blockedTop = true;
		piece.squareInfo.Add(kvp, sq);

		kvp = new KeyValuePair<int, int>(1, 0);
		sq = new GameSquare();
		sq.blockedLeft = true;
		sq.displayedNumber = 3;
		piece.squareInfo.Add(kvp, sq);
		gameObj.GetComponent<TileBehavior>().SetUpWithGamePiece(piece);
	}
}
