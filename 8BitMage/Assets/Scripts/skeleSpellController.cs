using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;



public class skeleSpellController : MonoBehaviour {

	public GameObject character;
	public bool exists = false;
	public GameObject alert;
	public Animator anim;
	private GameObject instantiatedObj;

	public GameObject panel;
	public Text question;
	public Image iconImage;
	public Button close;
	public Button fire;
	public Button water;
	public Button wind;
	public Button earth;
	public Button electricity;


	// This can be improved.... a lot
	void Update () {
		if (Vector3.Distance (transform.position, character.transform.position) < 3f) {
			if (!exists) {
				Vector3 alertPosition = new Vector3 (transform.position.x, transform.position.y + 1.5f, 0f);
				instantiatedObj = (GameObject) Instantiate (alert, alertPosition, Quaternion.identity);
				exists = true;
			}
		} else {
			exists = false;
			Destroy(instantiatedObj);
		}

		anim.SetBool ("exists", exists);

		if (exists) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				if (!panel.activeSelf) {
					panel.SetActive (true);
				}
			}
		}
		if (panel.activeSelf) {
			if (Input.GetKeyDown(KeyCode.Escape) || Vector3.Distance (transform.position, character.transform.position) > 3f){
				panel.SetActive (false);
			}
		}
	}

}
