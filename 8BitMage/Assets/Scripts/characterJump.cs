using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterJump: MonoBehaviour {
	
	private Rigidbody2D rb2D;
	Animator anim;

	// Get the RigidBody and Animator components of the Character so we can work with them
	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
