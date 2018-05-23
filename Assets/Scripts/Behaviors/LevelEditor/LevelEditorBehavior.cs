using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine;

public class LevelEditorBehavior : MonoBehaviour {

	public TileBankEditorBehavior tileBank;
	public GridEditorBehavior mainBoard;

	public void ExportJson() {
		JSONNode parentNode = new JSONObject();
		JSONNode boardNode = this.mainBoard.gameBoard.ToJson();
		parentNode["squares"] = boardNode;

		JSONNode tilesNode = new JSONObject();
		foreach (EditorTileContainerBehavior tileContainer in tileBank.tileContainers) {
			JSONNode tileNode = tileContainer.gridBehavior.gameBoard.ToJson();
			tilesNode.Add(tileNode);
		}

		parentNode["tiles"] = tilesNode;

		string path = "Assets/Resources/" + "test" + ".json";
		StreamWriter reader = new StreamWriter(path, append: false);
		reader.Write(parentNode.ToString());
	}
}
