using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {


    /*
     * Movement Properties
     */
    // numerical value for length between two tile centers (or, equally, the length of 1 tile).
    private float tileSize;
    // The change in movement desired from one keypress
    private float deltaPos;
    // the rigidbody of the player object. Used to apply movement.
    private Rigidbody2D myRigidbody;

    /*
     * Collision Properties
     */
    // Attached objects representing the next possible movement locations.
    // If the feelers encounter an obstacle, movement is disabled.
    public GameObject topFeeler, botFeeler, rightFeeler, leftFeeler;

    /*
     * Vision Properties
     */
    // Attached Prefab for vision gameobjects
    public GameObject pVision;
    // Property holding the current furthest vision object for each cardinal
    // direction. Used in update loop to extend Line of Sight to max range.
    private GameObject farTopVis, farBotVis, farLeftVis, farRightVis;
    // Attache objects of the players immediate vision
    public GameObject tVis, rVis, bVis, lVis;
    public GameObject tlVis, trVis, blVis, brVis, cVis;

    enum Direction {
        Up, Down, Left, Right
    };

    private Direction curFacing;

    // Use this for initialization
    void Start() {
        tileSize = 1;
        deltaPos = tileSize;
        myRigidbody = GetComponent<Rigidbody2D>();
        ResetFarVision();
    }

    // Update is called once per frame
    void Update() {
        CheckInput();
        GrantAllVision();
    }

    void CheckInput() {
        Vector2 tempCoord = new Vector2();
        if (Input.GetKeyDown(KeyCode.D)) {
            ExecuteMovement(Direction.Right);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            ExecuteMovement(Direction.Left);
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            ExecuteMovement(Direction.Up);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            ExecuteMovement(Direction.Down);
        }

    }

    void ChangePosition(Vector2 newCoord) {
        myRigidbody.MovePosition(newCoord);
        MoveAllFeelers(newCoord);
        MoveCornerFeelers(newCoord);

        CleanUpVision();
    }

    void MoveFeeler(GameObject feeler, Vector2 newCoord) {
        Rigidbody2D rbFeel = feeler.GetComponent<Rigidbody2D>();
        rbFeel.MovePosition(newCoord);
    }

    void MoveCornerFeelers(Vector2 newCoord) {
        Vector2 tVec = new Vector2(newCoord.x, newCoord.y + tileSize);
        Vector2 rVec = new Vector2(newCoord.x + tileSize, newCoord.y);
        Vector2 bVec = new Vector2(newCoord.x, newCoord.y - tileSize);
        Vector2 lVec = new Vector2(newCoord.x - tileSize, newCoord.y);
        Vector2 tlVec = new Vector2(newCoord.x - tileSize, newCoord.y + tileSize);

        Vector2 trVec = new Vector2(newCoord.x + tileSize, newCoord.y + tileSize);

        Vector2 blVec = new Vector2(newCoord.x - tileSize, newCoord.y - tileSize);

        Vector2 brVec = new Vector2(newCoord.x + tileSize, newCoord.y - tileSize);

        MoveFeeler(tlVis, tlVec);
        MoveFeeler(trVis, trVec);
        MoveFeeler(blVis, blVec);
        MoveFeeler(brVis, brVec);
        MoveFeeler(tVis, tVec);
        MoveFeeler(rVis, rVec);
        MoveFeeler(bVis, bVec);
        MoveFeeler(lVis, lVec);
        MoveFeeler(cVis, newCoord);
    }

    void MoveAllFeelers(Vector2 newCoord) {
        Vector2 topVec = new Vector2(newCoord.x, newCoord.y + tileSize);

        Vector2 botVec = new Vector2(newCoord.x, newCoord.y - tileSize);

        Vector2 rightVec = new Vector2(newCoord.x + tileSize, newCoord.y);

        Vector2 leftVec = new Vector2(newCoord.x - tileSize, newCoord.y);

        MoveFeeler(topFeeler, topVec);
        MoveFeeler(botFeeler, botVec);
        MoveFeeler(rightFeeler, rightVec);
        MoveFeeler(leftFeeler, leftVec);
    }

    void SetRotate(float negAngle, float posAngle) {
        float rotationCorrect;

        if (curFacing == Direction.Left || curFacing == Direction.Down) {
            rotationCorrect = negAngle - transform.eulerAngles.z;
            transform.Rotate(new Vector3(0, 0, rotationCorrect));
        } else if (curFacing == Direction.Right || curFacing == Direction.Up) {
            rotationCorrect = posAngle - transform.eulerAngles.z;
            transform.Rotate(new Vector3(0, 0, rotationCorrect));
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    void GrantVision(GameObject vision, Direction dir) {
        visionController sVision = vision.GetComponent<visionController>();
        Vector3 nextPos = new Vector3();
        switch (dir) {
            case Direction.Up:
                nextPos = new Vector3(vision.transform.position.x, vision.transform.position.y + 1, vision.transform.position.z);
                break;
            case Direction.Down:
                nextPos = new Vector3(vision.transform.position.x, vision.transform.position.y - 1, vision.transform.position.z);
                break;
            case Direction.Left:
                nextPos = new Vector3(vision.transform.position.x - 1, vision.transform.position.y, vision.transform.position.z);
                break;
            case Direction.Right:
                nextPos = new Vector3(vision.transform.position.x + 1, vision.transform.position.y, vision.transform.position.z);
                break;

        }

        if (sVision.firstFixedUpdate == true) {
            if (sVision.GetCollisionStatus() == false) {
                GameObject cloneVision = Instantiate(pVision, nextPos, new Quaternion());
                cloneVision.tag = "ray";

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

        foreach (GameObject visionClone in visionClones) {
            visionController sClone = visionClone.GetComponent<visionController>();
            Rigidbody2D rbClone = visionClone.GetComponent<Rigidbody2D>();
            sClone.toDestroy = true;
            rbClone.MovePosition(new Vector2(-100, -100));
        }

        visionController sVision = cVis.GetComponent<visionController>();
        sVision.firstFixedUpdate = false;

        ResetFarVision();

    }

    void ResetFarVision() {
        farTopVis = cVis;
        farBotVis = cVis;
        farLeftVis = cVis;
        farRightVis = cVis;
    }

    void GrantAllVision() {
        GrantVision(farTopVis, Direction.Up);
        GrantVision(farBotVis, Direction.Down);
        GrantVision(farLeftVis, Direction.Left);
        GrantVision(farRightVis, Direction.Right);
    }

    void ExecuteMovement(Direction dir)
    {
        Vector2 tempCoord = new Vector2();
        curFacing = dir;
        GameObject curFeeler = DetermineFeeler(dir);
        Vector2 offsetAngle = DetermineOffsetAngles(dir);
        Vector2 curDelta = DetermineDelta(dir);
        SetRotate(offsetAngle.x, offsetAngle.y);
        if (curFeeler.GetComponent<feelerController>().GetCollisionStatus() == false)
        {
            tempCoord.Set(myRigidbody.position.x + curDelta.x, myRigidbody.position.y + curDelta.y);
            ChangePosition(tempCoord);
        }
    }

    Vector2 DetermineOffsetAngles(Direction dir)
    {
        Vector2 curVector = new Vector2();
        if (dir == Direction.Left || dir == Direction.Right)
        {
            curVector = new Vector2(90, 270);
        } else if(dir == Direction.Up || dir == Direction.Down)
        {
            curVector = new Vector2(180, 0);
        }

        return curVector;
    }

    GameObject DetermineFeeler(Direction dir)
    {
        GameObject curFeeler = new GameObject();

        switch(dir)
        {
            case Direction.Up:
                curFeeler = topFeeler;
                break;
            case Direction.Down:
                curFeeler = botFeeler;
                break;
            case Direction.Left:
                curFeeler = leftFeeler;
                break;
            case Direction.Right:
                curFeeler = rightFeeler;
                break;
        }
        return curFeeler;
    }

    Vector2 DetermineDelta(Direction dir)
    {
        Vector2 returnVec = new Vector2();
        switch(dir)
        {
            case Direction.Up:
                returnVec.Set(0, deltaPos);
                break;
            case Direction.Down:
                returnVec.Set(0, deltaPos * -1);
                break;
            case Direction.Left:
                returnVec.Set(deltaPos * -1, 0);
                break;
            case Direction.Right:
                returnVec.Set(deltaPos, 0);
                break;
        }
        return returnVec;
    }
}