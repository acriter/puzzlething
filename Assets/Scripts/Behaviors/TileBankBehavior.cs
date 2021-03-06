﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileBankBehavior : MonoBehaviour {
	public List<TileBehavior> tileBehaviors;
	private TileBank tileBank;
	private List<Tile> tiles {
		get {
			return this.tileBank.tiles;
		}
	}

	public void Start() {
	}

	public void InitializeWithLevel(string levelName) {
		this.tileBank = new TileBank();
		bool fileExists = tileBank.InitializeWithLevel(levelName);

		if (!fileExists) {
			this.SetUpMockTiles();
		} else {
			this.SetUpTiles();
		}
	}

	private void SetUpTiles() {
		this.tileBehaviors = new List<TileBehavior>();

		int i = 0;
		foreach (Tile t in this.tiles) {
			Canvas canvas = GameObject.FindObjectOfType<Canvas>();
			Vector3 canvasScale = canvas.transform.localScale;
			GameObject obj = Resources.Load("Prefabs/TileContainer") as GameObject;
			GameObject tileContainer = GameObject.Instantiate(obj, transform);
			RectTransform rt = tileContainer.GetComponent<RectTransform>();
			//RectTransform ourRect = this.GetComponent<RectTransform>();
			//float width = Screen.width * ourRect.anchorMax.x;
			float height = rt.sizeDelta.y;
			//rt.sizeDelta = new Vector2(width, height);
			tileContainer.transform.localPosition = new Vector3(0, (height + 10) * i);
			tileContainer.transform.localScale = Vector3.one;

			GameObject gameObj = new GameObject("Tile Behavior");
			gameObj.transform.SetParent(tileContainer.transform);
			gameObj.transform.localPosition = new Vector2(0, 0);
			gameObj.transform.localScale = Vector3.one;
			i++;
			TileBehavior beh = gameObj.AddComponent<TileBehavior>();
			this.tileBehaviors.Add(beh);
			beh.SetUpWithGamePiece(t);
		}
	}

	private void SetUpMockTiles() {
		tileBehaviors = new List<TileBehavior>();
		
		GameObject gameObj = new GameObject("Tile Behavior");
		gameObj.transform.SetParent(transform);
		TileBehavior beh = gameObj.AddComponent<TileBehavior>();
		
		Tile piece = new Tile();
		piece.squareInfo = new Dictionary<Coordinate, GameCell>();
		Coordinate bst = new Coordinate();
		bst.row = 0;
		bst.column = 0;
		GameCell sq = new GameCell();
		sq.displayedNumber = 0;
		sq.blockedTop = false;
		piece.squareInfo.Add(bst, sq);
		
		Coordinate bst2 = new Coordinate();
		bst2.row = 0;
		bst2.column = 1;
		GameCell sq2 = new GameCell();
		sq2.displayedNumber = 0;
		sq2.blockedBottom = false;
		sq2.blockedLeft = false;
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
		sq3.displayedNumber = 3;
		sq3.blockedRight = false;
		piece2.squareInfo.Add(bst3, sq3);
		
		Coordinate bst4 = new Coordinate();
		bst4.row = 1;
		bst4.column = 0;
		GameCell sq4 = new GameCell();
		sq4.blockedLeft = false;
		sq4.blockedTop = false;
		piece2.squareInfo.Add(bst4, sq4);
		
		Coordinate bst5 = new Coordinate();
		bst5.row = 1;
		bst5.column = 1;
		GameCell sq5 = new GameCell();
		sq5.blockedBottom = false;
		piece2.squareInfo.Add(bst5, sq5);
		
		beh2.SetUpWithGamePiece(piece2);
		
		tileBehaviors.Add(beh);
		tileBehaviors.Add(beh2);

	}
}
