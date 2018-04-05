using System.Collections;
using System.Collections.Generic;

public class Tile {
	public Dictionary<Coordinate, GameCell> squareInfo;

	public Tile() {
		this.squareInfo = new Dictionary<Coordinate, GameCell>();
	}
}
