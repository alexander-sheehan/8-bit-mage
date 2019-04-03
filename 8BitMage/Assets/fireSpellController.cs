using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSpellController : MonoBehaviour {
	public GameObject character;

	private GameObject flame;

	private GameObject flameParticleSystem;

	public Quaternion rot;

	private characterMovement charMove;

	private Transform firePoint;

	// Use this for initialization
	void Start () {
		firePoint = GameObject.FindGameObjectWithTag ("firePoint").transform;
		character = GameObject.FindGameObjectsWithTag ("Player") [1];
		charMove = character.GetComponent<characterMovement> ();
		if (GameObject.FindGameObjectsWithTag ("fireSpell").Length <= 1) {
			rot = Quaternion.Euler (transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f);
			flame = Instantiate (Resources.Load ("Particle Systems/fireParticleSystem"), transform.position, rot) as GameObject;
		} else {																							
			Destroy (gameObject);
		}
	}

	void Update () {
		if (!Input.GetKey (KeyCode.P)) {
			Destroy (flame);
			Destroy (gameObject);
		} else {
			flame.transform.position = firePoint.position;
			if (charMove.wallGrounded) {
				Vector3 localPosition = firePoint.localPosition;
				Vector3 adjustedPosition = localPosition;
				adjustedPosition.x *= -1;
				firePoint.localPosition = adjustedPosition;
				flame.transform.position = firePoint.position;
				firePoint.localPosition = localPosition;
			}
			if ((charMove.facingRight && !charMove.wallGrounded) || (!charMove.facingRight && charMove.wallGrounded)) {
				flame.transform.rotation = Quaternion.Euler (rot.x, 90f, -90f);
			} else {
				flame.transform.rotation = Quaternion.Euler (rot.x, -90f, 90f);
			}
		}
	}
}
