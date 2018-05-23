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

	private Vector2 PositionForNewContainer() {
		return new Vector2(0, this.tileContainers.Count * CONTAINER_HEIGHT);
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
		this.AddNewContainer();
	}
}
