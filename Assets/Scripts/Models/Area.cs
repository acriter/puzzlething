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
		bool foundCorrectNumber = false;

		foreach (GameCell cell in this.cells) {
			if (cell.displayedNumber == this.cells.Count) {
				foundCorrectNumber = true;
			}

			if (cell.displayedNumber > 0 && cell.displayedNumber != this.cells.Count) {
				return false;
			}
		}
		return foundCorrectNumber;
	}

	public int Size() {
		return this.cells.Count;
	}
}
