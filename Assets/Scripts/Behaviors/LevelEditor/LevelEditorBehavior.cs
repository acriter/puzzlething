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

		JSONArray tilesNode = new JSONArray();
		foreach (EditorTileContainerBehavior tileContainer in tileBank.tileContainers) {
			GridEditorBehavior grid = tileContainer.gridBehavior;
			if (grid != null) {
				JSONNode tileNode = grid.gameBoard.ToJson();
				tilesNode[-1] = tileNode;
			}
		}

		parentNode["tiles"] = tilesNode;

		string path = "Assets/Resources/" + "test" + ".json";
		StreamWriter writer = new StreamWriter(path, append: false);
		writer.WriteLine(parentNode.ToString());
		writer.Close();
	}
}
