using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directorController : MonoBehaviour {

	enum Direction {
		Up, Down, Left, Right
	};

	public GameObject hedge;
	public GameObject fog;
	// Use this for initialization
	void Start () {


		BuildWall (new Vector2(-5, -5), Direction.Up, 22);
		BuildWall (new Vector2(-5, 16), Direction.Right, 22);
		BuildWall (new Vector2(16, 16), Direction.Down, 22);
		BuildWall (new Vector2(16, -5), Direction.Left, 22);
		//Instantiate (hedge, new Vector3(1.76f, .16f, 0), new Quaternion());

		GenerateFog(new Vector2(-15, -15), 45);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void BuildWall (Vector2 startCoord, Direction growthDir, int length ) {

		Vector3 curCoord = new Vector3 (startCoord.x, startCoord.y, 0);

		for(int i = 0; i < length; i++) {



			Instantiate (hedge, curCoord ,new Quaternion());

			switch (growthDir) {
			case Direction.Up:
				curCoord.y += 1;
				break;
			case Direction.Down:
				curCoord.y -= 1;
				break;
			case Direction.Right:
				curCoord.x += 1;
				break;
			case Direction.Left:
				curCoord.x -= 1;
				break;
			}
		}
	
	}

	void GenerateFog(Vector2 startCoord, int length) {

		Vector3 curCoord = new Vector3 (startCoord.x, startCoord.y, 0);
		Vector3 offset = new Vector3(startCoord.x,startCoord.y, 0);

		for (int i = 0; i < length; i++) {
			curCoord.y = i;
			for (int j = 0; j < length; j++) {
				curCoord.x = j;
				Instantiate (fog, curCoord + offset ,new Quaternion());
			}
		}
	}
}
