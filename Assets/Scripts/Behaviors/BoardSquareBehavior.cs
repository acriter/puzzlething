using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardSquareBehavior : MonoBehaviour {
	//TODO: move this somewhere better
	public static int TILE_SIZE = 64;

	private GameSquare square;
	public Text text;
	public Image leftImage, rightImage, topImage, bottomImage;

	public void UpdateWithGameSquare(GameSquare square) {
		this.square = square;

		Color c = bottomImage.color;
		Color alphaColor = new Color(c.r, c.g, c.b, 0.3f);

		if (square.blockedBottom) {
			bottomImage.color = alphaColor;
		}
		if (square.blockedLeft) {
			leftImage.color = alphaColor;
		}
		if (square.blockedRight) {
			rightImage.color = alphaColor;
		}
		if (square.blockedTop) {
			topImage.color = alphaColor;
		}
		if (square.displayedNumber == 0) {
			text.text = "";
		} else {
			text.text = square.displayedNumber.ToString();
		}
	}
}
