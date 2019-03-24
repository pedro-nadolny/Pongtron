using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
		if (noAction()) 
			return;
					
		Ray ray = generateRay();
		RaycastHit hit;

		if (!Physics.Raycast(ray.origin, ray.direction, out hit)) 
			return;
	
		Destroy(hit.transform.gameObject);
	}

	Ray generateRay() {
		Vector3 position = getCurrentActionPostion();

		Vector3 positionFar = new Vector3(position.x, position.y, Camera.main.farClipPlane);
		Vector3 positionNear = new Vector3(position.x, position.y, Camera.main.nearClipPlane);

		Vector3 positionFarOnWorld = Camera.main.ScreenToWorldPoint(positionFar);
		Vector3 positionNearOnWorld = Camera.main.ScreenToWorldPoint(positionNear);

		Ray ray = new Ray(positionNearOnWorld, positionFarOnWorld - positionNearOnWorld);
		return ray;
	}

	Vector3 getCurrentActionPostion() {
		if (isMobile()) {
			return Input.GetTouch(0).position;	
		}

		return Input.mousePosition;
	}

	bool noAction() {
		if (isMobile()) {
			return Input.touchCount == 0;
		}

		return !Input.GetMouseButton(0);
	}

	bool isMobile() {
		return 
			Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer;
	}
}
