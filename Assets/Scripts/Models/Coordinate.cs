using System.Collections;
using System.Collections.Generic;

public struct Coordinate {
	public int row, column;

	public static Coordinate NullCoordinate() {
		return new Coordinate(-1, -1);
	}

	public Coordinate(int x, int y) {
		row = x;
		column = y;
	}

	public override bool Equals(object obj) {
		// Check for null values and compare run-time types.
		if (obj == null || GetType() != obj.GetType())
			return false;

		Coordinate p = (Coordinate)obj;
		return (row == p.row) && (column == p.column);
	}
	public override int GetHashCode() {
		return row ^ column;
	}
}
