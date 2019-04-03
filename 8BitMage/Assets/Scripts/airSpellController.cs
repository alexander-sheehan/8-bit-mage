using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airSpellController : MonoBehaviour {
	public GameObject character;
	public characterMovement charMove;

	// Needs work
	void Start () {
		character = GameObject.FindGameObjectsWithTag ("Player") [1];
		charMove = character.GetComponent<characterMovement> ();
		if (GameObject.FindGameObjectsWithTag ("airSpell").Length <= 1 && !charMove.grounded) {
			Rigidbody2D rb2D = character.GetComponent<Rigidbody2D> ();
			rb2D.AddForce (Vector3.up * 700);
		} else {
			Destroy (gameObject);
		}
	}

	void Update() {
		if (charMove.grounded) Destroy (gameObject, .1f);
	}
}
