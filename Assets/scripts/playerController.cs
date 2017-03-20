using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	// offset is used to correct the player sprite onto the map grid.
	private Vector2 offset;
	// numerical value for length between two tile centers.
	private float tileWidth, tileHeight;
	// combined value. Used to apply movement
	private float deltaX, deltaY;
	private Vector2 dXVector, dYVector;
	private int frameCount;

	//test Vars
	private Vector2 dest;
	private Vector2 shortMove;
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		offset = new Vector2 (0.16f, 0.16f);
		tileWidth = 0.16f;
		tileHeight = 0.16f;
		deltaX = offset.x + tileWidth;
		deltaY = offset.y + tileHeight;

		dXVector = new Vector2 (deltaX, 0);
		dYVector = new Vector2 (0, deltaY);

		//test Vars
		dest = new Vector2 (0,0);
		shortMove = new Vector2 (0, 0);;

		myRigidbody = GetComponent<Rigidbody2D> ();

		frameCount = 0;
		
	}
	
	// Update is called once per frame
	void Update3 () {
		if (Input.GetKeyDown (KeyCode.D)) {
			transform.Translate (deltaX, 0f, 0f);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			transform.Translate (deltaX * -1, 0f, 0f);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			transform.Translate (0f, deltaY, 0f);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			transform.Translate (0f, deltaY * -1, 0f);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (frameCount!=0) {
			dest = new Vector2 (dest.x - shortMove.x, dest.y - shortMove.y);
			//transform.Translate (shortMove, 0f, 0f);
			myRigidbody.MovePosition(new Vector2(myRigidbody.position.x + shortMove.x, myRigidbody.position.y + shortMove.y));
			frameCount--;
//		}else if (dest.x < 0) {
//			dest = new Vector2 (dest.x - shortMove, 0);
//			//transform.Translate (shortMove, 0f, 0f);
//			myRigidbody.MovePosition(new Vector2(myRigidbody.position.x + shortMove, myRigidbody.position.y));			
		}else {
			dest = new Vector2 (0, 0);
		}

		CheckInput ();
	

	}

	void CheckInput() {
		if (Input.GetKeyDown (KeyCode.D)) {
//			if (dest.x == 0 && dest.y == 0) {
//				dest = new Vector2 (deltaX, 0);
//				shortMove = dest.x / 15;
//			}
			DetermineDest(dXVector);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			DetermineDest (dXVector * -1.0f);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			DetermineDest (dYVector);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			DetermineDest (dYVector * -1.0f);
		}
	}

	void DetermineDest(Vector2 vector) {
		// Frames to reach destination if unimpeded
		int frame = 12;

		if (frameCount==0) {
			dest = new Vector2 (vector.x, vector.y);
			shortMove = dest / frame;
			frameCount = frame;
		}
	}
}
