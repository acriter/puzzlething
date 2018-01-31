using System.Collections;
using System.Collections.Generic;

public class GameCell {
	public int displayedNumber = 0;

	//can be null if it's just part of the starting grid
	public Tile parent;

	public bool blockedLeft = true;
	public bool blockedTop = true;
	public bool blockedRight = true;
	public bool blockedBottom = true;

	//counts if it's either part of the starting grid or on a tile that's attached to the grid (or a tile attached to a tile on the grid)
	public bool attachedToGrid;
}
