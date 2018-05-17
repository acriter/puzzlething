using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using UnityEngine;

public class TileBank {

	public List<Tile> tiles;

	//returns false if filepath is not found
	public bool InitializeWithLevel(string levelName) {
		this.tiles = new List<Tile>();

		string path = "Assets/Resources/" + levelName + ".json";
		if (!File.Exists(path)) {
			Debug.LogWarning("couldn't find path " + path);
			return false;
		}

		StreamReader reader = new StreamReader(path); 
		string jsonText = reader.ReadToEnd();
		reader.Close();
		JSONNode node = JSON.Parse(jsonText);
		foreach (JSONNode tile in node["tiles"]) {
			Tile t = new Tile();

			foreach (JSONNode square in tile) {
				Coordinate coord = new Coordinate();
				coord.row = square["row"];
				coord.column = square["column"];
				GameCell cell = GameCell.GameCellFromJSONNode(square);
				t.squareInfo.Add(coord, cell);
			}
			this.tiles.Add(t);
		}

		return true;
	}
}
