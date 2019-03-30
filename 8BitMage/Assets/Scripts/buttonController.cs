using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour {
	public GameObject panel;


	//This is just the beginning with default values. 
	// In the future, each choice will fill out player prefs differently
	public void spellOnclick() {
		panel.SetActive (false);

		Debug.Log ("CLICK!");

		// Primary is the primary element
		PlayerPrefs.SetInt ("primary", 1);

		// Range is how far the spell goes
		PlayerPrefs.SetInt ("range", 20);

		// Speed is how fast the spell goes
		PlayerPrefs.SetFloat ("speed", .2f);

		PlayerPrefs.SetInt ("ready", 1);

	}
}
