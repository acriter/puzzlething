using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NewEditModeTest {

	[Test]
	public void NewEditModeTestSimplePasses() {
		Coordinate x = new Coordinate(3, 5);
		Assert.That(2 + 2 == 4);
		MoreComplicatedTest();
	}

	[Test]
	public void AreaCreationWorks() {
		Dictionary<Coordinate, GameBoardSquare> boardInfo = new Dictionary<Coordinate, GameBoardSquare>();
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				Coordinate coord = new Coordinate(i, j);
				GameBoardSquare square = new GameBoardSquare(coord);
				GameCell cell = new GameCell();
				square.AddGameCell(cell);
				boardInfo.Add(coord, square);
			}
		}

		GameBoardSquare sq = boardInfo[new Coordinate(0, 0)];
		sq.TopCell.blockedTop = false;
		sq.TopCell.blockedRight = false;
		GameBoardSquare sq2 = boardInfo[new Coordinate(1, 0)];
		sq2.TopCell.blockedLeft = false;
		GameBoardSquare sq3 = boardInfo[new Coordinate(0, 1)];
		sq3.TopCell.blockedBottom = false;
		sq3.TopCell.blockedRight = false;

		GameBoard board = new GameBoard(boardInfo);

		Assert.That(board.AreaForSquare(new Coordinate(0, 0)).Size() == 3);
		Assert.That(board.AreaForSquare(new Coordinate(1, 0)).Size() == 3);

		GameBoardSquare sq4 = boardInfo[new Coordinate(1, 1)];
		sq4.TopCell.blockedLeft = false;
		sq4.TopCell.blockedRight = false;

		Assert.That(board.AreaForSquare(new Coordinate(0, 0)).Size() == 4);
		Assert.That(board.AreaForSquare(new Coordinate(1, 1)).Size() == 4);

		Assert.That(board.AreaForSquare(new Coordinate(2, 2)).Size() == 1);

		boardInfo[new Coordinate(2, 1)].TopCell.blockedLeft = false;
		boardInfo[new Coordinate(2, 1)].TopCell.blockedTop = false;
		boardInfo[new Coordinate(2, 2)].TopCell.blockedBottom = false;
		boardInfo[new Coordinate(2, 2)].TopCell.blockedLeft = false;
		boardInfo[new Coordinate(1, 2)].TopCell.blockedRight = false;

		Assert.That(board.AreaForSquare(new Coordinate(2, 2)).Size() == 7);
	}

	[Test]
	public void AreaValidationWorks() {
		Area area = new Area();
		GameCell cell = new GameCell();
		cell.displayedNumber = 3;
		GameCell cell2 = new GameCell();
		cell2.displayedNumber = 3;
		GameCell cell3 = new GameCell();
		area.AddCell(cell);
		area.AddCell(cell2);
		area.AddCell(cell3);
		Assert.That(area.MeetsCriteria());

		GameCell cell4 = new GameCell();
		area.AddCell(cell4);
		Assert.That(!area.MeetsCriteria());
		cell.displayedNumber = 4;
		Assert.That(!area.MeetsCriteria());
		cell2.displayedNumber = 4;
		Assert.That(area.MeetsCriteria());
	}

	public void MoreComplicatedTest() {
		Assert.That(2 + 2 == 4);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
