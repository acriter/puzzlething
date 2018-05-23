using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITileContainerOwnerDelegate {
	void DidPressAddButton();
	void DidPressDeleteButton(EditorTileContainerBehavior container);
}

public class EditorTileContainerBehavior : MonoBehaviour {
	public Button addButton, deleteButton;
	public ITileContainerOwnerDelegate owner;
	private GridEditorBehavior gridBehavior;
	private bool isEmpty = true;

	public void AddButtonPressed() {
		if (!this.isEmpty) {
			return;
		}

		if (this.owner != null) {
			this.owner.DidPressAddButton(this);
		}

		CanvasGroup addButtonCanvas = this.addButton.GetComponent<CanvasGroup>();
		addButtonCanvas.alpha = 0;

		CanvasGroup deleteButtonCanvas = this.deleteButton.GetComponent<CanvasGroup>();
		deleteButtonCanvas.alpha = 1;

		this.gridBehavior = this.gameObject.AddComponent<GridEditorBehavior>();
		this.gridBehavior.ConfigureWithSize(4, true);
	}

	public void DeleteButtonPressed() {
		if (this.owner != null) {
			this.owner.DidPressDeleteButton(this);
		}
		GameObject.Destroy(this.gameObject);
	}
}
