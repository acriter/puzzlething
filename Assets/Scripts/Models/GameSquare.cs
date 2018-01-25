using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSquare {
	public int displayedNumber = 0;

	public bool blockedLeft, blockedTop, blockedRight, blockedBottom;

	//counts if it's either part of the starting grid or on a tile that's attached to the grid (or a tile attached to a tile on the grid)
	public bool attachedToGrid;
}
