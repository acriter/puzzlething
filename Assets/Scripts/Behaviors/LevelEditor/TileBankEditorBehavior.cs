using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBankEditorBehavior : MonoBehaviour, ITileContainerOwnerDelegate {

	public List<EditorTileContainerBehavior> tileContainers;
	int CONTAINER_HEIGHT = 200;

	// Use this for initialization
	void Start () {
		this.tileContainers = new List<EditorTileContainerBehavior>();
		this.AddNewContainer();
	}

	private Vector2 PositionForContainer(int i) {
		return new Vector2(0, i * CONTAINER_HEIGHT);
	}

	private Vector2 PositionForNewContainer() {
		return this.PositionForContainer(this.tileContainers.Count);
	}

	private void AddNewContainer() {
		GameObject containerObj = Resources.Load("Prefabs/EditorTileContainer") as GameObject;
		GameObject tileContainer = GameObject.Instantiate(containerObj);
		tileContainer.transform.SetParent(this.transform);
		tileContainer.transform.localPosition = this.PositionForNewContainer();
		EditorTileContainerBehavior tileContainerBehavior = tileContainer.GetComponent<EditorTileContainerBehavior>();
		tileContainerBehavior.owner = this;
		this.tileContainers.Add(tileContainerBehavior);
	}


	//Interface methods
	public void DidPressAddButton() {
		this.AddNewContainer();		
	}

	public void DidPressDeleteButton(EditorTileContainerBehavior container) {
		this.tileContainers.Remove(container);
		for (int i = 0; i < this.tileContainers.Count; ++i) {
			this.tileContainers[i].transform.localPosition = this.PositionForContainer(i);
		}

		if (this.tileContainers.Count == 0) {
			this.AddNewContainer();
		}
	}
}
