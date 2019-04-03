using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellController : MonoBehaviour {

	private float speed = 10;
	public Rigidbody2D rb;

	public GameObject character;

	void Start () {
		// TODO: 
		// Add different spells

		// Place holder spell projectile ignores the player
		// NOTE: some spells won't have this, we want a large
		// platform to be touchable by player
		Physics2D.IgnoreCollision (GetComponent<Collider2D>(), GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D>(), GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<Collider2D>());
		Vector2 velocity = new Vector2 (); 
		if (PlayerPrefs.GetString ("grounded") == "true") {
			velocity = transform.right * speed;
			if (Input.GetKey ("up")) {
				velocity.y = speed;
			}
		} else {
			if (Input.GetKey ("down")) {
				velocity.y = -speed;
			} else if (Input.GetKey ("up")) {
				velocity.y = speed;
			} else {
				velocity = transform.right * speed;
			}
		}
		velocity.x = (PlayerPrefs.GetString ("wallGrounded") == "true") ? (velocity.x *= -1) : velocity.x;
		rb.velocity = velocity;
	}

	// hit info can give cool stuff based on what the spell collides with
	void OnTriggerEnter2D(Collider2D hitInfo){
		//Debug.Log(hitInfo.name);
		Destroy (gameObject);
	}
}
