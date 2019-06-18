using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Vector3 initialMouseOffset;
	private float distanceToCamera;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject topWall;
	public GameObject bottomWall;

	private void Update() {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
		
	void OnMouseDrag() {
		SetPlayerPosition(GetMouseWorldPos() + initialMouseOffset);
	}

	private void OnMouseDown() {
		RefreshMouseOffset();
	}

	private void RefreshMouseOffset() {
		Vector3 pos = transform.position;
		distanceToCamera = Camera.main.WorldToScreenPoint(pos).z;
		initialMouseOffset = pos - GetMouseWorldPos();
	}

	private Vector3 GetMouseWorldPos() {
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = distanceToCamera;
		return Camera.main.ScreenToWorldPoint(mousePoint); 
	}

	private void SetPlayerPosition(Vector3 p) {
		Bounds playerBounds = GetComponent<Renderer>().bounds;
		float minZ = leftWall.GetComponent<Renderer>().bounds.max.z + playerBounds.extents.z;
		float maxZ = rightWall.GetComponent<Renderer>().bounds.min.z - playerBounds.extents.z;
		float minY = bottomWall.GetComponent<Renderer>().bounds.max.y + playerBounds.extents.y;
		float maxY = topWall.GetComponent<Renderer>().bounds.min.y - playerBounds.extents.y;

		p.z = Mathf.Clamp(p.z, minZ, maxZ);
		p.y = Mathf.Clamp(p.y, minY, maxY);

		transform.position = p;
	}
}
