using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            col.GetComponent<playerController>().SetWinning(true);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
