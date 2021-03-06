﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feelerController : MonoBehaviour {

	private bool collisionStatus;

	private Rigidbody2D myRigidBody;

	public bool toDestroy;


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

	// Use this for initialization
	void Start () {
		// Keep an eye on this, may mess things up if spawned on top of a obstacle
		this.collisionStatus = false;

		toDestroy = false;

		myRigidBody = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (toDestroy == true) {
			Destroy (gameObject, .5f);
		}

	}

	void ReportCollision () {
		
	}

	public bool GetCollisionStatus() {
		return collisionStatus;
	}

	public void SetCollisionStatus(bool newStatus) {
		collisionStatus = newStatus;
	}

	void OnDestroy() {
		//Debug.Log ("Destroyed");
	}
}
