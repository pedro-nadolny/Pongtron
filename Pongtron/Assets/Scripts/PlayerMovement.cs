using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 0) { return; }

		Touch touch = Input.GetTouch(0);
		Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

		transform.position = touchPosition;

		print(transform.position);
	}
}
