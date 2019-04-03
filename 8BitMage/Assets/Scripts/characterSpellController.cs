using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSpellController : MonoBehaviour {
	private int castTime;
	private int totalCastTime;

	private Vector3 freeFirePoint;
	characterMovement charMove;
	private string currentSpell;

	void Start(){
		// Initialize stuff
		charMove = gameObject.GetComponent<characterMovement>();
		castTime = 0;
		currentSpell = "";
	}

	void Update () {
		// Get spell that's being cast
		string pressedSpell = isSpellPressed ();

		// If this is the first tick that the spell is casted
		// start the cast process
		if ((currentSpell == "") && (pressedSpell.Length > 0)) {
			currentSpell = pressedSpell;
		// If spell is being casted, confirm that it is the same spell
		} else if (pressedSpell.Length > 0 && pressedSpell == currentSpell) {
			castTime += 1;

			// If the spell has been casted for the appropriate time,
			// cast it and reset the process
			if (castTime >= getCastTime(currentSpell)) {
				Cast (pressedSpell);
				castTime = 0;
				currentSpell = "";
			}
		// Reset
		} else {
			castTime = 0;
			currentSpell = "";
		}
	}

	// Spell casting method
	// Basically this shit just instantiates the appropriate spell prefab
	void Cast(string spell){
		Transform firePoint = GameObject.FindGameObjectWithTag ("firePoint").transform;

		// If player is against a wall, we should fire the spell
		// in the opposite direction
		if (charMove.wallGrounded) {
			Vector3 localPosition = firePoint.localPosition;
			Vector3 adjustedPosition = localPosition;
			adjustedPosition.x *= -1;
			firePoint.localPosition = adjustedPosition;
			Instantiate (Resources.Load ("Spells/" + spell), firePoint.position, Quaternion.Inverse(firePoint.rotation));

			// Reset the fire point position so that everything's happy 
			firePoint.localPosition = localPosition;
		// Otherwise just fire the thing
		} else {
			Instantiate (Resources.Load ("Spells/" + spell), firePoint.position, firePoint.rotation);
		}
	}

	// Don't judge me for this shit-
	// Do we want interchangeable order or nah?
	string isSpellPressed(){
		string currentSpell = "";
		int numberButtonsPressed = 0;

		if (Input.GetKey(KeyCode.Y)){
			currentSpell += "air_";
			numberButtonsPressed += 1;
		}

		if (Input.GetKey(KeyCode.U)){
			numberButtonsPressed += 1;
			currentSpell += "water_";
		}

		if (Input.GetKey(KeyCode.I)){
			numberButtonsPressed += 1;
			currentSpell += "earth_";
		}

		if (Input.GetKey(KeyCode.O)){
			numberButtonsPressed += 1;
			currentSpell += "electricity_";
		}

		if (Input.GetKey(KeyCode.P)){
			numberButtonsPressed += 1;
			currentSpell += "fire_";
		}
			
		if (numberButtonsPressed > 2) {
			currentSpell = "";
		}

		return currentSpell;
	}

	// This is going to be another ladder... sadly
	// Use switch? I think use switch
	int getCastTime(string spell){
		return 10;
	}
}
