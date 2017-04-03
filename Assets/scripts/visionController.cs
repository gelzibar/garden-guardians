using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionController : MonoBehaviour {

	private bool collisionStatus;
	public bool toDestroy;
	public bool firstFixedUpdate;


	// Use this for initialization
	void Start () {
		collisionStatus = false;
		toDestroy = false;
		firstFixedUpdate = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (toDestroy == true) {
			Destroy (gameObject, .5f);
		}
	}

	void FixedUpdate() {
		firstFixedUpdate = true;
	
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.CompareTag ("hedge")) {
			SetCollisionStatus (true);
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.CompareTag ("hedge")) {
			SetCollisionStatus (true);
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.CompareTag ("hedge")) {
			SetCollisionStatus (false);
		}
	}

	public bool GetCollisionStatus() {
		return collisionStatus;
	}

	public void SetCollisionStatus(bool newStatus) {
		collisionStatus = newStatus;
	}
}
