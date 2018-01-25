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
		sq.displayedNumber = 7;
		piece.squareInfo.Add(kvp, sq);
		
		KeyValuePair<int, int> kvp2 = new KeyValuePair<int, int>(0, 1);
		GameSquare sq2 = new GameSquare();
		sq2.displayedNumber = 5;
		piece.squareInfo.Add(kvp2, sq2);
		gameObj.GetComponent<TileBehavior>().SetUpWithGamePiece(piece);
	}
}
