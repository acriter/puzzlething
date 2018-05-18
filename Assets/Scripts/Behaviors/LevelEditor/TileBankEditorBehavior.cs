using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBankEditorBehavior : MonoBehaviour {

	public List<EditorTileContainerBehavior> tileContainers;

	// Use this for initialization
	void Start () {
		this.tileContainers = new List<EditorTileContainerBehavior>();	
	}
}
