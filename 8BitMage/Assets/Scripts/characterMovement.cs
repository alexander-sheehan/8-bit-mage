using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class characterMovement : MonoBehaviour {

	public float maxSpeed = 15f;
	public bool facingRight = true;
	private Rigidbody2D rb2D;
	Animator anim;


	// Get the RigidBody and Animator components of the Character so we can work with them
	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{ 
		int moveAnimState = anim.GetInteger ("movementState");

		/* WHAT I WANT TO DO GOING FORWARD:
		 * if the player is inputing a horizontal movement (and player is not in the air)
		 * play running animationo
		 * else if the abs(player movement) is > 0
		 * play sliding animation
		 * else
		 * play idle animation
		 * 
		 * How can this be improved?
		 * 1: velocity change requirements to trigger slide animations (+ others?)
		 * 		example: only slide when player is decreasing from a certain velocity
		 */


		// Get the input of the player (on the x axis)
		float move = Input.GetAxis ("Horizontal");

		// If the player velocity is 0, then the moving animation
		// should stop and the idle animation should start
		if (anim.GetFloat ("Speed") != 0 && rb2D.velocity.x == 0)
			anim.StopPlayback ();
			
		// First attempt at getting a slide animation into the movement cycle
		if (Mathf.Abs (move) > 0) {
			anim.SetInteger ("movementState", 1);
		} else if (rb2D.velocity.x > 0) {
			anim.SetInteger ("movementState", 2);
		}
		else {
			anim.SetInteger ("movementState", 0);
		}

		// Set Character velocity
		rb2D.velocity = new Vector2 (move * maxSpeed, rb2D.velocity.y);

		// If we move in a different direction, flip the world (of the character)
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	// Simple function to flip the world (of the character sprite) on the x axis to reverse the sprite when player turns
	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale; 
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
