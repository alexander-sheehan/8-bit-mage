using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	public GameObject Character;
	
	void Update () {
		float horzcenter = transform.position.x;
		float vertcenter = transform.position.y;

		float screenWidth = Screen.width/2;
		float screenHeight = Screen.height/2;

		float horzDist = Mathf.Abs (Character.transform.position.x - transform.position.x);
		float vertDist = Mathf.Abs (Character.transform.position.y - transform.position.y);

		var verticalSize   = Camera.main.orthographicSize * 2.0;
		var horizontalSize = verticalSize * Screen.width / Screen.height;

		// TODO: add screen recentering acceleration based on
		// - distance horzDist & vertDist
		// - amount of time player has been off center (triggering the window scrolling)

		// If player touches left bounding box
		if (horzDist > (horizontalSize/2 * .6f) && ((Character.transform.position.x - transform.position.x) < 0)) {
			var leftPos = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
			transform.position = leftPos;
		} 
		if (horzDist > (horizontalSize/2 * .6f) && ((Character.transform.position.x - transform.position.x) > 0)) {
			var rightPos = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
			transform.position = rightPos;
		}
		if (vertDist > (verticalSize / 2 * .6f) && ((Character.transform.position.y - transform.position.y) < 0)) {
			var bottomPos = new Vector3(transform.position.x, transform.position.y - .2f, transform.position.z);
			transform.position = bottomPos;
		}
		if (vertDist > (verticalSize / 2 * .6f) && ((Character.transform.position.y - transform.position.y) > 0)) {
			var topPos = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
			transform.position = topPos;
		}

	}
}
