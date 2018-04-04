using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileDroppedDelegate {
	void DidDropTile(GameObject obj);
}

public class LevelBehavior : MonoBehaviour, ITileDroppedDelegate {
	public GridBehavior gridBehavior;
	public TileBankBehavior tileBankBehavior;

	public void Start() {
		this.gridBehavior.dropTileDelegate = this;
	}

	public void DidDropTile(GameObject obj) {
		if (this.DidUserBeatLevel()) {
			Debug.Log("user beat the level!");
		}
	}

	private bool DidUserBeatLevel() {
		foreach (TileBehavior t in this.tileBankBehavior.tiles) {
			if (!this.gridBehavior.IsTilePlaced(t)) {
				return false;
			}
		}
		return this.gridBehavior.IsLevelSolved();
	}
}
