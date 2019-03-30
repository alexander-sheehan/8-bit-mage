using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
	int timeOffCenter = 0;
	float maxCameraSpeed = .5f;
	float cameraVelocity = .2f;
	public GameObject Character;
	bool cameraChanged = false;
	float boundingBoxSize = .6f;
	float screenWidth = Screen.width/2;
	float screenHeight = Screen.height/2;

	void Update () {
		float horzcenter = transform.position.x;
		float vertcenter = transform.position.y;

		float horzDist = Mathf.Abs (Character.transform.position.x - transform.position.x);
		float vertDist = Mathf.Abs (Character.transform.position.y - transform.position.y);

		var verticalSize   = Camera.main.orthographicSize * 2.0;
		var horizontalSize = verticalSize * Screen.width / Screen.height;

		// TODO: add screen recentering acceleration based on
		// - distance horzDist & vertDist

		// If player touches left bounding box.
		// Bounding boxes are determined by boundingBoxSize
		if (horzDist > (horizontalSize/2 * boundingBoxSize) && ((Character.transform.position.x - transform.position.x) < 0)) {
			var leftPos = new Vector3(transform.position.x - cameraVelocity, transform.position.y, transform.position.z);
			transform.position = leftPos;
			cameraChanged = true;
		} 
		// right bounding box
		if (horzDist > (horizontalSize/2 * boundingBoxSize) && ((Character.transform.position.x - transform.position.x) > 0)) {
			var rightPos = new Vector3(transform.position.x + cameraVelocity, transform.position.y, transform.position.z);
			transform.position = rightPos;
			cameraChanged = true;
		}
		// top bounding box
		if (vertDist > (verticalSize / 2 * boundingBoxSize) && ((Character.transform.position.y - transform.position.y) < 0)) {
			var bottomPos = new Vector3(transform.position.x, transform.position.y - cameraVelocity, transform.position.z);
			transform.position = bottomPos;
			cameraChanged = true;
		}
		// bottom bounding box
		if (vertDist > (verticalSize / 2 * boundingBoxSize) && ((Character.transform.position.y - transform.position.y) > 0)) {
			var topPos = new Vector3(transform.position.x, transform.position.y + cameraVelocity, transform.position.z);
			transform.position = topPos;
			cameraChanged = true;
		}

		if (cameraChanged) {
			if (cameraVelocity < .5f) {
				cameraVelocity += .01f;
			}
			cameraChanged = false;
		} else {
			cameraVelocity = .2f;
		}
	}
}
