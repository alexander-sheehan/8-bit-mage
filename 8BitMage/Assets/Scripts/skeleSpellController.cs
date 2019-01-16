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

	public Text question;
	public Image iconImage;
	public Button close;
	public Button fire;
	public Button water;
	public Button wind;
	public Button earth;
	public Button electricity;

//	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
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
				
			}
		}

	}
}
