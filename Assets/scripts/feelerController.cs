using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feelerController : MonoBehaviour {

	private bool collisionStatus;


	void OnTriggerEnter2D (Collider2D col) {
		Debug.Log ("Feeler Triggered.");
		SetCollisionStatus (true);
	}

	void OnTriggerExit2D (Collider2D col) {
		SetCollisionStatus (false);
	}

	// Use this for initialization
	void Start () {
		// Keep an eye on this, may mess things up if spawned on top of a obstacle
		this.collisionStatus = false;
		Debug.Log ("Feeler started.");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ReportCollision () {
		
	}

	public bool GetCollisionStatus() {
		return collisionStatus;
	}

	public void SetCollisionStatus(bool newStatus) {
		collisionStatus = newStatus;
	}
}
