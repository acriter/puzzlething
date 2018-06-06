using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class GameCell {
	public static GameCell GameCellFromJSONNode(JSONNode node) {
		GameCell gameCell = new GameCell();
		gameCell.blockedBottom = node["blockedBottom"];
		gameCell.blockedTop = node["blockedTop"];
		gameCell.blockedLeft = node["blockedLeft"];
		gameCell.blockedRight = node["blockedRight"];
		gameCell.displayedNumber = node["number"];
		return gameCell;
	}

	public int displayedNumber = 0;

	//can be null if it's just part of the starting grid
	public Tile parent;

	//solid cells are the "greyed out" cells that must have another cell on top of them for the level to be clear.
	public bool solid = true;

	public bool blockedLeft = true;
	public bool blockedTop = true;
	public bool blockedRight = true;
	public bool blockedBottom = true;
}
