using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject draggedItem;
	Vector3 startPosition;
	Vector3 startMousePosition;
	Transform startParent;
	Transform canvas;

	public void OnBeginDrag(PointerEventData eventData) {
		draggedItem = gameObject;
		startParent = transform.parent;
		startPosition = transform.position;
		startMousePosition = Input.mousePosition;
		//GetComponent<CanvasGroup>().blocksRaycasts = false;
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
		transform.SetParent(canvas);
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = Input.mousePosition - startMousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		draggedItem = null;
		//GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == canvas) {
			transform.SetParent(startParent);
			transform.position = startPosition;
		}
	}
}