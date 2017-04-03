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
	// current value of how many FixedUpdate() remain before destination is reached.
	private int frameCount;
	// total value of how many FixedUpdate() are required to reach destination.
	// the higher the number, the slower the gnome moves.
	private int maxFrames;

	private Vector2 dest;
	private Rigidbody2D myRigidbody;

	public GameObject topFeeler, botFeeler, rightFeeler, leftFeeler;
	private feelerController topFeelerScript, botFeelerScript, rightFeelerScript, leftFeelerScript;

	public GameObject pVision;
	private int visionCount;
	private GameObject farTopVis, farBotVis, farLeftVis, farRightVis;

	public GameObject tVis, rVis, bVis, lVis;
	public GameObject tlVis, trVis, blVis, brVis, cVis;


	private SpriteRenderer sprite;

	enum Direction {
		Up, Down, Left, Right
	};

	private Direction curFacing;

	// Use this for initialization
	void Start () {

		sprite = GetComponent<SpriteRenderer> ();
		Debug.Log (sprite);
		Debug.Log ("Player Start Method");
		//offset = new Vector2 (0.16f, 0.16f);
		offset = new Vector2 (0, 0);
		tileWidth = 1;
		tileHeight = 1;

		deltaX = offset.x + tileWidth;
		deltaY = offset.y + tileHeight;

		dest = new Vector2 (0,0);

		myRigidbody = GetComponent<Rigidbody2D> ();

		frameCount = 0;
		maxFrames = 12;

		topFeelerScript = topFeeler.GetComponent<feelerController> ();
		botFeelerScript = botFeeler.GetComponent<feelerController> ();
		rightFeelerScript = rightFeeler.GetComponent<feelerController> ();
		leftFeelerScript = leftFeeler.GetComponent<feelerController> ();


		farTopVis = cVis;
		farBotVis = cVis;
		farLeftVis = cVis;
		farRightVis = cVis;
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();	
		GrantVision (farTopVis, Direction.Up);
		GrantVision (farBotVis, Direction.Down);
		GrantVision (farLeftVis, Direction.Left);
		GrantVision (farRightVis, Direction.Right);
		//CleanUpVision ();

	}

	// Update is called once per frame
	void FixedUpdate () {
//		if (frameCount!=0) {
//			dest = new Vector2 (dest.x - shortMove.x, dest.y - shortMove.y);
//			//transform.Translate (shortMove, 0f, 0f);
//			myRigidbody.MovePosition(new Vector2(myRigidbody.position.x + shortMove.x, myRigidbody.position.y + shortMove.y));
//			frameCount--;
//		}else {
//			dest = new Vector2 (0, 0);
//		}

	}

	void LateUpdate() {

	}



	void CheckInput() {
		Vector2 tempCoord = new Vector2 ();
		if (Input.GetKeyDown (KeyCode.D)) {	
//			DetermineDest(dXVector);
//			SetUnitFacing ();
			curFacing = Direction.Right;
			SetRotate ( 90, 270);
			if (rightFeelerScript.GetCollisionStatus () == false) {
				tempCoord.Set (myRigidbody.position.x + deltaX, myRigidbody.position.y);
				ChangePosition (tempCoord);
			}

		} else if (Input.GetKeyDown (KeyCode.A)) {
//			DetermineDest (dXVector * -1);
//			SetUnitFacing ();
			curFacing = Direction.Left;
			SetRotate ( 90, 270);
			if (leftFeelerScript.GetCollisionStatus () == false) {
				tempCoord.Set (myRigidbody.position.x - deltaX, myRigidbody.position.y);
				ChangePosition (tempCoord);
			}
		}

		if (Input.GetKeyDown (KeyCode.W)) {
//			DetermineDest (dYVector);
//			SetUnitFacing ();
			curFacing = Direction.Up;
			SetRotate ( 180, 0);
			if (topFeelerScript.GetCollisionStatus () == false) {
				tempCoord.Set (myRigidbody.position.x, myRigidbody.position.y + deltaY);
				ChangePosition (tempCoord);
			}
		} else if (Input.GetKeyDown (KeyCode.S)) {
//			DetermineDest (dYVector * -1);
//			SetUnitFacing ();
			curFacing = Direction.Down;
			SetRotate ( 180, 0);
			if (botFeelerScript.GetCollisionStatus () == false) {
				tempCoord.Set (myRigidbody.position.x, myRigidbody.position.y - deltaY);
				ChangePosition (tempCoord);
			}
		}

        if(Input.GetKeyDown(KeyCode.E))
        {
            //CenterGnome();
        }
	}

	void DetermineDest(Vector2 vector) {

		if (frameCount==0) {
			dest = new Vector2 (vector.x, vector.y);
			frameCount = maxFrames;
		}
	}

	void ChangePosition(Vector2 newCoord) {
		//oldCoord.Set (myRigidbody.position.x, myRigidbody.position.y);
		myRigidbody.MovePosition(newCoord);
		MoveAllFeelers (newCoord);
		MoveCornerFeelers (newCoord);

		CleanUpVision ();
		//topFeeler.transform.position.Set (newCoord.x, newCoord.y + .32f, topFeeler.transform.position.z);
		//topFeeler.riposition.Set(newCoord.x, newCoord.y + .32f, topFeeler.transform.position.z);
	}

	void MoveFeeler(GameObject feeler, Vector2 newCoord) {
		Rigidbody2D rbFeel = feeler.GetComponent<Rigidbody2D>();
		rbFeel.MovePosition (newCoord);
	}

	void MoveCornerFeelers(Vector2 newCoord) {
		Vector2 tVec = new Vector2 (newCoord.x, newCoord.y + tileHeight);
		Vector2 rVec = new Vector2 (newCoord.x + tileWidth, newCoord.y);
		Vector2 bVec = new Vector2 (newCoord.x, newCoord.y - tileHeight);
		Vector2 lVec = new Vector2 (newCoord.x - tileWidth, newCoord.y);
		Vector2 tlVec = new Vector2 (newCoord.x - tileWidth, newCoord.y + tileHeight);

		Vector2 trVec = new Vector2 (newCoord.x + tileWidth, newCoord.y + tileHeight);

		Vector2 blVec = new Vector2 (newCoord.x - tileWidth, newCoord.y - tileHeight);

		Vector2 brVec = new Vector2 (newCoord.x + tileWidth, newCoord.y - tileHeight);

		MoveFeeler (tlVis, tlVec);
		MoveFeeler (trVis, trVec);
		MoveFeeler (blVis, blVec);
		MoveFeeler (brVis, brVec);
		MoveFeeler (tVis, tVec);
		MoveFeeler (rVis, rVec);
		MoveFeeler (bVis, bVec);
		MoveFeeler (lVis, lVec);
		MoveFeeler (cVis, newCoord);
	}

	void MoveAllFeelers(Vector2 newCoord) {
		Vector2 topVec = new Vector2 (newCoord.x, newCoord.y + tileHeight);

		Vector2 botVec = new Vector2 (newCoord.x, newCoord.y - tileHeight);

		Vector2 rightVec = new Vector2 (newCoord.x + tileWidth, newCoord.y);

		Vector2 leftVec = new Vector2 (newCoord.x - tileWidth, newCoord.y);

		MoveFeeler (topFeeler, topVec);
		MoveFeeler (botFeeler, botVec);
		MoveFeeler (rightFeeler, rightVec);
		MoveFeeler (leftFeeler, leftVec);
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

	void SetRotate(float negAngle, float posAngle) {
		float rotationCorrect;

		if(curFacing == Direction.Left || curFacing == Direction.Down){
			rotationCorrect = negAngle - transform.eulerAngles.z;
			transform.Rotate (new Vector3(0,0,rotationCorrect));
		}else if(curFacing == Direction.Right || curFacing == Direction.Up){
			rotationCorrect = posAngle - transform.eulerAngles.z;
			transform.Rotate (new Vector3(0,0,rotationCorrect));
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
//		Debug.Log ("Player Triggered.");
//		RevertPosition ();
	}

	public Vector3 GetPosition() {
		return transform.position;
	}

	void GrantVision(GameObject vision, Direction dir) {
		visionController sVision = vision.GetComponent<visionController> ();
		Vector3 nextPos = new Vector3();
		switch(dir) {
		case Direction.Up:
			nextPos = new Vector3 (vision.transform.position.x, vision.transform.position.y + 1, vision.transform.position.z);
			break;
		case Direction.Down:
			nextPos = new Vector3 (vision.transform.position.x, vision.transform.position.y - 1, vision.transform.position.z);
			break;
		case Direction.Left:
			nextPos = new Vector3 (vision.transform.position.x - 1, vision.transform.position.y, vision.transform.position.z);
			break;
		case Direction.Right:
			nextPos = new Vector3 (vision.transform.position.x + 1, vision.transform.position.y, vision.transform.position.z);
			break;
			
		}

		if (sVision.firstFixedUpdate == true) {
			if (sVision.GetCollisionStatus () == false) {
				GameObject cloneVision = Instantiate (pVision, nextPos, new Quaternion ());
				cloneVision.tag = "ray";
				visionCount++;

				switch (dir) {
				case Direction.Up:
					farTopVis = cloneVision;
					break;
				case Direction.Down:
					farBotVis = cloneVision;
					break;
				case Direction.Left:
					farLeftVis = cloneVision;
					break;
				case Direction.Right:
					farRightVis = cloneVision;
					break;
				}
			}
		}
	}

	void CleanUpVision() {
		GameObject[] visionClones = new GameObject[0];

		visionClones = GameObject.FindGameObjectsWithTag("ray");

		foreach(GameObject visionClone in visionClones) {
			visionController sClone = visionClone.GetComponent<visionController> ();
			Rigidbody2D rbClone = visionClone.GetComponent<Rigidbody2D> ();
//			Collider2D cClone = visionClone.GetComponent<Collider2D> ();
			sClone.toDestroy = true;
			rbClone.MovePosition(new Vector2(-100, -100));
//			cClone.enabled = false;
		}

		visionController sVision = cVis.GetComponent<visionController> ();
		sVision.firstFixedUpdate = false;


		farTopVis = cVis;
		farBotVis = cVis;
		farLeftVis = cVis;
		farRightVis = cVis;
	}
}
