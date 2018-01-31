using System.Collections;
using System.Collections.Generic;

public class Area {
	List<GameCell> cells;

	public Area() {
		this.cells = new List<GameCell>();
	}

	public void AddCell(GameCell cell) {
		if (!cells.Contains(cell)) {
			this.cells.Add(cell);
		}
	}

	public bool MeetsCriteria() {
		foreach (GameCell cell in this.cells) {
			if (cell.displayedNumber > 0 && cell.displayedNumber != this.cells.Count) {
				return false;
			}
		}
		return true;
	}

	public int Size() {
		return this.cells.Count;
	}
}
