using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class characterMovement : MonoBehaviour {

	private float maxSpeed = 15f;
	private float maxSlideSpeed = 5f;
	public bool facingRight = true;
	private Rigidbody2D rb2D;
	private CircleCollider2D characterHitBox;
	Animator anim;

	public bool grounded = false;
	float groundRadius = .3f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	public bool wallGrounded = false;
	public Transform wallCheck;

	private float jumpForce = 700f;

	private float wallJumpYForce = 700;
	private float wallJumpXForce = 1400f;

	public bool leftWallTouch = false;
	public bool rightWallTouch = false;

	private void Awake()
	{
		// Get the RigidBody and Animator components of the Character so we can work with them
		rb2D = GetComponent<Rigidbody2D>();
		characterHitBox = GetComponent<CircleCollider2D> ();
		anim = GetComponent<Animator> ();
	}
		
	void FixedUpdate () 
	{

		// Initialized stuff for jumping and determining what is ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rb2D.velocity.y);

		// wallGrounded is the boolean to determine whether we're touching a wall
		// Touching a wall should allow the player to rejump, but numbers should be tweaked so that you cannot ascend a wall by only jumping
		// I have no idea why it's the radius * 4.5 for the box size... it works, just accept it
		wallGrounded = Physics2D.OverlapBox(wallCheck.position, new Vector2((characterHitBox.radius * 4.5f), .1f), 0f, whatIsGround);
		anim.SetBool ("wallGrounded", wallGrounded);

		// Left and right collider boxes that detect which wall the player is leaning against
		// Once again.. no idea why I have to multiply the radius by 2.5... you can fix it if you want GL HF
		Vector2 leftWallHitBoxPos = new Vector2 (wallCheck.position.x - characterHitBox.radius, wallCheck.position.y);
		Vector2 leftWallHitBoxSize = new Vector2 (characterHitBox.radius * 2.5f, .1f);
		leftWallTouch = Physics2D.OverlapBox (leftWallHitBoxPos, leftWallHitBoxSize, 0f, whatIsGround);

		Vector2 rightWallHitBoxPos = new Vector2 (wallCheck.position.x + characterHitBox.radius, wallCheck.position.y);
		Vector2 rightWallHitBoxSize = new Vector2 (characterHitBox.radius * 2.5f, .1f);
		rightWallTouch = Physics2D.OverlapBox (rightWallHitBoxPos, rightWallHitBoxSize, 0f, whatIsGround);

		// Get the input of the player (on the x axis)
		float move = Input.GetAxisRaw ("Horizontal");
			
		// First attempt at getting a slide animation into the movement cycle
		// Sliding should only happen when the player releases inputs while running at a certain top speed
		if (Mathf.Abs (move) > 0)
			anim.SetInteger ("movementState", 1);
		else if (Mathf.Abs (rb2D.velocity.x) > maxSlideSpeed && !Input.GetButton ("Horizontal"))
			anim.SetInteger ("movementState", 2);
		else 
			anim.SetInteger ("movementState", 0);


		// This section deals with horizontal acceleration and deceleration
		// It's linear... bite me t(-,-t)
		float newSpeed = rb2D.velocity.x;

		// With linear acceleration, I don't think this *kick off* part is necessary
		// But fuck it, it's here and I'm too lazy to change it
		if (newSpeed == 0) {
			if (move > 0) {
				newSpeed = 1f;
			} else if (move < 0) {
				newSpeed = -1f;
			}

		// We're adding flat amounts to the players velocity every frame based on user input
		} else if (move > 0) {
			newSpeed += 1.2f;

		} else if (move < 0) {
			newSpeed -= 1.2f;
		
			// If we're close to 0, just make it 0 (I don't know why I'm doing this, bite me again t(-,-t) )
		} else if (newSpeed > -.9f && newSpeed < .9f){
			newSpeed = 0;

		} else if (move == 0 && newSpeed > 0) {
			newSpeed -= .2f;

		} else if (move == 0 && newSpeed < 0) {
			newSpeed += .2f;
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
		else if (!facingRight && rightWallTouch)
			Flip ();
		else if (facingRight && leftWallTouch)
			Flip ();
	}

	void Update()
	{
		// Because jumping is a single button instance (you click it and don't hold it down),
		// we want this to live in Update and not FixedUpdate
		// TODO: 
		// Holding down space increases the height of a jump?
		if (wallGrounded && !grounded && Input.GetKeyDown (KeyCode.Space)) {

			// This is for animations. When the player jumps- make them not grounded
			anim.SetBool ("Ground", false);

			//For wall jumping
			if (facingRight) 

				// When on a wall, add force to both x and y axis'
				rb2D.AddForce (new Vector2 (-wallJumpXForce, wallJumpYForce));
			else 
				rb2D.AddForce (new Vector2 (wallJumpXForce, wallJumpYForce));

		// If we're not on a wall, then jump normal
		} else if (grounded && Input.GetKeyDown (KeyCode.Space)){

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
