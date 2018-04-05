﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* This class represents an individual cell, i.e. one square and one member of a tile */
/* It knows nothing about other cells that share the same coordinate; see GridBoardSquareBehavior */
public class BoardSquareBehavior : MonoBehaviour {
	//TODO: move this somewhere better
	public static int TILE_SIZE = 64;

	private GameCell cell;
	public Text text;
	public Image leftImage, rightImage, topImage, bottomImage;
	public Image backgroundImage;

	public TileBehavior parentTile;

	public void UpdateSquare() {
		this.InitializeWithGameCell(this.cell);
	}

	public void InitializeWithGameCell(GameCell topCell) {
		this.cell = topCell;

		Color c = bottomImage.color;
		Color alphaColor = new Color(c.r, c.g, c.b, 0.3f);

		if (topCell.blockedBottom) {
			bottomImage.color = alphaColor;
		}
		if (topCell.blockedLeft) {
			leftImage.color = alphaColor;
		}
		if (topCell.blockedRight) {
			rightImage.color = alphaColor;
		}
		if (topCell.blockedTop) {
			topImage.color = alphaColor;
		}
		if (topCell.displayedNumber == 0) {
			text.text = "";
		} else {
			text.text = topCell.displayedNumber.ToString();
		}
	}
}
