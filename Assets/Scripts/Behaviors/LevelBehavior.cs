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
		string levelToLoad = ApplicationModelBehavior.LevelToLoad;
		this.gridBehavior.InitializeWithLevel(levelToLoad);
		this.gridBehavior.dropTileDelegate = this;

		this.tileBankBehavior.InitializeWithLevel(levelToLoad);
	}

	public void DidDropTile(GameObject obj) {
		if (this.DidUserBeatLevel()) {
			Debug.Log("user beat the level!");
		}
	}

	private bool DidUserBeatLevel() {
		foreach (TileBehavior t in this.tileBankBehavior.tileBehaviors) {
			if (!this.gridBehavior.IsTilePlaced(t)) {
				return false;
			}
		}
		return this.gridBehavior.IsLevelSolved();
	}
}
