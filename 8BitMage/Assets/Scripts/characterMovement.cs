using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class characterMovement : MonoBehaviour {

	public float maxSpeed = 15f;
	public float maxSlideSpeed = 9f;
	public bool facingRight = true;
	private Rigidbody2D rb2D;
	Animator anim;

	public bool grounded = false;
	float groundRadius = .2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	public float jumpForce = 700f;

	private void Awake()
	{
		// Get the RigidBody and Animator components of the Character so we can work with them
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{

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

		/* TODO: 
		* Create more of a slide on the character movment after input has ended
		* Add wall sliding
		* Fix animations in air
		* Tighten controls
		*/

		// Initialized stuff for jumping and determining what is ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rb2D.velocity.y);

		// Get the input of the player (on the x axis)
		float move = Input.GetAxisRaw ("Horizontal");
			
		// First attempt at getting a slide animation into the movement cycle
		// Sliding should only happen when the player releases inputs and 
		if (Mathf.Abs (move) > 0) 
			anim.SetInteger ("movementState", 1);
		else if (Mathf.Abs(rb2D.velocity.x) > maxSlideSpeed && !Input.GetButton ("Horizontal"))
			anim.SetInteger ("movementState", 2);
		else 
			anim.SetInteger ("movementState", 0);


		// This section deals with horizontal acceleration and deceleration
		float newSpeed = rb2D.velocity.x;

		// With linear accerlation, I don't think this *kick off* part is necessary
		// But fuck it, it's here and I'm too lazy to change it
		if (newSpeed == 0) {
			if (move > 0) {
				newSpeed = 1f;
			} else if (move < 0) {
				newSpeed = -1f;
			}

		// We're linearlly accelerating the player, so adding flat amounts based on input and current velocity
		} else if (move > 0) {
			newSpeed += 1.6f;
		} else if (move < 0) {
			newSpeed -= 1.6f;
		} else if (newSpeed > -.9f && newSpeed < .9f){
			newSpeed = 0;
		} else if (move == 0 && newSpeed > 0) {
			newSpeed -= .1f;
		} else if (move == 0 && newSpeed < 0) {
			newSpeed += .1f;
		}

		// Capping speeds based on global `maxSpeed`
		if (newSpeed > maxSpeed) {
			newSpeed = maxSpeed;
		} else if (newSpeed < -maxSpeed) {
			newSpeed = -maxSpeed;
		}

		// Apply the new velocity to the character's rigid body component
		rb2D.velocity = new Vector2 (newSpeed, rb2D.velocity.y);

		// If we move in a different direction, flip the world (of the character)
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update()
	{
		// Because jumping is a single button instance (you click it and don't hold it down),
		// we want this to live in Update and not FixedUpdate
		// If we're on the ground and the player presses `space`
		// TODO: 
		// Wall jumping 
		// Holding down space increases the height of a jump?
		if (grounded && Input.GetKeyDown (KeyCode.Space)){

			// This is for animations
			anim.SetBool ("Ground", false);

			// Add a positive velocity to the character's y axis
			rb2D.AddForce (new Vector2 (0, jumpForce));
		}
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
