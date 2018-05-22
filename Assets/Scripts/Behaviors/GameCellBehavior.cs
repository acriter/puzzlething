using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* This class represents an individual cell, i.e. one square and one member of a tile */
/* It knows nothing about other cells that share the same coordinate; see GridBoardSquareBehavior */
public class GameCellBehavior : MonoBehaviour, IHighlightableButtonOwnerDelegate {
	//TODO: move this somewhere better
	public static int TILE_SIZE = 128;

	public GameCell cell;
	public Text text;
	public Image leftImage, rightImage, topImage, bottomImage;
	public HighlightableButton leftButton, rightButton, topButton, bottomButton;
	public Image backgroundImage;

	public TileBehavior parentTile;

	public void UpdateCell() {
		this.InitializeWithGameCell(this.cell);
	}

	public void EnableCellEditing() {
		this.leftButton.owner = this;
		this.rightButton.owner = this;
		this.topButton.owner = this;
		this.bottomButton.owner = this;
		this.leftButton.isSelectable = true;
		this.rightButton.isSelectable = true;
		this.topButton.isSelectable = true;
		this.bottomButton.isSelectable = true;
	}

	public void DidPressHighlightableButton(HighlightableButton button) {
		if (button.Equals(this.leftButton)) {
			this.cell.blockedLeft = !this.cell.blockedLeft;
		} else if (button.Equals(this.rightButton)) {
			this.cell.blockedRight = !this.cell.blockedRight;
		} else if (button.Equals(this.topButton)) {
			this.cell.blockedTop = !this.cell.blockedTop;
		} else if (button.Equals(this.bottomButton)) {
			this.cell.blockedBottom = !this.cell.blockedBottom;
		}

		this.UpdateCell();
	}

	public void InitializeWithGameCell(GameCell topCell) {
		this.cell = topCell;

		Color c = Color.black;
		Color alphaColor = new Color(c.r, c.g, c.b, 0.3f);

		if (topCell.blockedBottom) {
			bottomImage.color = c;
		} else {
			bottomImage.color = alphaColor;
		}
		if (topCell.blockedLeft) {
			leftImage.color = c;
		} else {
			leftImage.color = alphaColor;
		}
		if (topCell.blockedRight) {
			rightImage.color = c;
		} else {
			rightImage.color = alphaColor;
		}
		if (topCell.blockedTop) {
			topImage.color = c;
		} else {
			topImage.color = alphaColor;
		}
		if (topCell.displayedNumber == 0) {
			text.text = "";
		} else {
			text.text = topCell.displayedNumber.ToString();
		}
	}
}
