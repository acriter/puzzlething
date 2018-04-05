using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* This class represents a coordinate (square) on the game board, and all the tiles/information on that coordinate */
public class GridBoardSquareBehavior : MonoBehaviour {
	GameBoardSquare boardSquare;
	List<GameCellBehavior> stackedGameCells;

	public void Start() {
		//This is a list of the tiles that are on this square
		this.stackedGameCells = new List<GameCellBehavior>();
	}

	public void AddGameCell(GameCellBehavior cell) {
		this.stackedGameCells.Add(cell);
	}

	public void RemoveGameCell(GameCellBehavior cell) {
		this.stackedGameCells.Remove(cell);
	}

	public void Initialize(GameBoardSquare square) {
		this.boardSquare = square;
		GameCell topCell = square.TopCell;
		if (topCell == null) {
			if (!square.isActive) {
				this.Deactivate();
			}
			//it's a slot to put a square but not an actual square
		} else {
			//TODO: code is copied from TileBehavior
			GameObject obj = Resources.Load("Prefabs/GameCell") as GameObject;
			GameObject instantiatedObj = GameObject.Instantiate(obj);
			instantiatedObj.transform.SetParent(transform);
			instantiatedObj.transform.localPosition = Vector2.zero;

			GameCellBehavior cellBehavior = instantiatedObj.GetComponent<GameCellBehavior>();
			cellBehavior.InitializeWithGameCell(topCell);

			this.AddGameCell(cellBehavior);
		}
	}

	public void Activate() {
		//this.backgroundImage.color = Color.white;
	}

	public void Deactivate() {
		//this.backgroundImage.color = new Color(0.1f, 0.1f, 0.1f);
	}
}
