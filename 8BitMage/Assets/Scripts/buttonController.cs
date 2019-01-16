using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour {
	public GameObject panel;

	public void spellOnclick() {
		panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
