using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	// offset is used to center the player sprite onto the map grid.
	private Vector2 offset;
	// numerical value for length between two tile centers.
	private float tileWidth, tileHeight;
	// combined value. Used to apply movement
	private float deltaX, deltaY;
	private Vector2 dXVector, dYVector;
	// current value of how many FixedUpdate() remain before destination is reached.
	private int frameCount;
	// total value of how many FixedUpdate() are required to reach destination.
	// the higher the number, the slower the gnome moves.
	private int maxFrames;

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

		dest = new Vector2 (0,0);
		shortMove = new Vector2 (0, 0);

		myRigidbody = GetComponent<Rigidbody2D> ();

		frameCount = 0;
		maxFrames = 12;
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (frameCount > 0) {
			dest = new Vector2 (dest.x - shortMove.x, dest.y - shortMove.y);
			//transform.Translate (shortMove, 0f, 0f);
			myRigidbody.MovePosition(new Vector2(myRigidbody.position.x + shortMove.x, myRigidbody.position.y + shortMove.y));
			frameCount--;
		}else {
			dest = new Vector2 (0, 0);
		}

		CheckInput ();
	}



	void CheckInput() {
		if (Input.GetKeyDown (KeyCode.D)) {	
			DetermineDest(dXVector);
			SetUnitFacing ();
		} else if (Input.GetKeyDown (KeyCode.A)) {
			DetermineDest (dXVector * -1);
			SetUnitFacing ();
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			DetermineDest (dYVector);
			SetUnitFacing ();
		} else if (Input.GetKeyDown (KeyCode.S)) {
			DetermineDest (dYVector * -1);
			SetUnitFacing ();
		}

        if(Input.GetKeyDown(KeyCode.E))
        {
            CenterGnome();
        }
	}

	void DetermineDest(Vector2 vector) {

		if (frameCount==0) {
			dest = new Vector2 (vector.x, vector.y);
			shortMove = dest / maxFrames;
			frameCount = maxFrames;
		}
	}

	void SetUnitFacing() {
		
//		if(dest.x < 0){
//			rotationCorrect = 90 - transform.eulerAngles.z;
//			transform.Rotate (new Vector3(0,0,rotationCorrect));
//		}else if(dest.x > 0){
//			rotationCorrect = 270 - transform.eulerAngles.z;
//			transform.Rotate (new Vector3(0,0,rotationCorrect));
//		}
		SetRotate (dest.x, 90, 270);
		SetRotate (dest.y, 180, 0);
	}

	void SetRotate(float destinationVector, float negAngle, float posAngle) {
		float rotationCorrect;

		if(destinationVector < 0){
			rotationCorrect = negAngle - transform.eulerAngles.z;
			transform.Rotate (new Vector3(0,0,rotationCorrect));
		}else if(destinationVector > 0){
			rotationCorrect = posAngle - transform.eulerAngles.z;
			transform.Rotate (new Vector3(0,0,rotationCorrect));
		}
	}

    void CenterGnome() {

    }
}
