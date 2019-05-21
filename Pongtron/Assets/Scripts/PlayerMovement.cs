using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Vector3 mOffset;
	private float mZCoord;

	void OnMouseDrag() {
		transform.position = GetMouseWorldPos() + mOffset;
	}

	private void OnMouseDown() {
		Vector3 pos = gameObject.transform.position;
		mZCoord = Camera.main.WorldToScreenPoint(pos).z;
		mOffset = pos - GetMouseWorldPos();
	}

	private Vector3 GetMouseWorldPos() {
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord;
		return Camera.main.ScreenToWorldPoint(mousePoint); 
	}
}
