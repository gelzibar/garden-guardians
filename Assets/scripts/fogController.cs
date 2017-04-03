using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogController : MonoBehaviour {


	Renderer rSprite;

	private GameObject player;
	private playerController playerScript;

	private bool colCheck;

	private int count, reset;


	void Awake () {
		rSprite = GetComponent<SpriteRenderer> ();
	}
	// Use this for initialization
	void Start () {
		colCheck = true;
		count = 0;
		reset = 60;
	}

	void OnTriggerEnter2D (Collider2D col) {
		rSprite.enabled = false;

	}

	void OnTriggerStay2D (Collider2D col) {
		rSprite.enabled = false;
	}

	void OnTriggerExit2D (Collider2D col) {
		rSprite.enabled = true;

	}


	// Update is called once per frame
	void FixedUpdate () {
//		if (colCheck == false) {
//			rSprite.enabled = false;
//		}else if(colCheck == true){
//			rSprite.enabled = true;
//		}
//
//		if (count > reset) {
//			rSprite.enabled = true;
//			count = 0;
//		}
//
//		count++;
	}
		
}
