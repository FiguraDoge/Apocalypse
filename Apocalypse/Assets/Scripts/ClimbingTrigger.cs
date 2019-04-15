using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbingTrigger : MonoBehaviour
{
    public SteamVR_Input_Sources leftHandSource;
    public SteamVR_Input_Sources rightHandSource;
    public SteamVR_Action_Boolean climbAction;

    public GameObject leftHandObject;
    public GameObject rightHandObject;
    public GameObject PlayerObject;
    public Rigidbody playerGravityRigibBody;

    public bool xMove;
    public bool yMove;
    public bool zMove;

    private bool leftHandTagged;
    private bool rightHandTagged;

    private Vector3 prevLeftPos;
    private Vector3 prevRightPos;

    // Start is called before the first frame update
    void Start()
    {
        leftHandTagged = false;
        rightHandTagged = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* Determine whether the trigger should be deactivated */
        
        if(leftHandTagged || rightHandTagged)
        {
            // Disable gravity when tagged hand is supposed to hold trigger.
            playerGravityRigibBody.useGravity = false;

            // Deactivate tag when the player release the trigger
            if (!checkTrigger(leftHandSource))
                leftHandTagged = false;

            if (!checkTrigger(rightHandSource))
                rightHandTagged = false;

            // Enable gravity so player can fall down
            if (!rightHandTagged && !leftHandTagged)
                playerGravityRigibBody.useGravity = true; 

            // Depending on the current hands status, move the player
            // 1. Both hands are tagged
            if (leftHandTagged && rightHandTagged)
            {
                // To Do
                // When both hands are triggered
                // Do nothing for right now
            }
            // 2. Left hand is tagged
            else if (leftHandTagged)
            {
                // When only left hand is attached, move the player according to the y value change in left hand's position
                PlayerObject.transform.position -= new Vector3(xMove ? leftHandObject.transform.position.x - prevLeftPos.x : 0, yMove ? leftHandObject.transform.position.y - prevLeftPos.y : 0, zMove ? leftHandObject.transform.position.z - prevLeftPos.z : 0);
            }
            // 3. Right hand is tagged
            else if (rightHandTagged)
            {
                // When only right hand is attached, move the player according to the y value change in right hand's position
                PlayerObject.transform.position -= new Vector3(xMove ? rightHandObject.transform.position.x - prevRightPos.x : 0, yMove ? rightHandObject.transform.position.y - prevRightPos.y : 0, zMove ? rightHandObject.transform.position.z - prevRightPos.z : 0);
            }

        }
        prevLeftPos = leftHandObject.transform.position;
        prevRightPos = rightHandObject.transform.position;

    }

    /* climbing action input is only activated when the hand is in the trigger, and will be deactivated anytime when the player release the trigger */
    void OnTriggerStay(Collider other)
    {
        /* ASSUMPTIONS: 
         * 1 - Both hands have a child with collider in a layer that can interact with the layer the trigger is in
         * 2 - the child component on left hand is tagged "LeftHand", and the child component on the right hand is tagged "Right Hand"
         */
        //Debug.Log(other.gameObject.tag);
        //Debug.Log(other.gameObject.layer);
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag.Equals("LeftHand") && checkTrigger(leftHandSource))
        {
            Debug.Log("Triggered By LeftHand");
            leftHandTagged = true;
        }

        if (other.gameObject.tag.Equals("RightHand") && checkTrigger(rightHandSource))
        {
            Debug.Log("Triggered By RightHand");
            rightHandTagged = true;
        }

        //Debug.Log("Collided with layer: " + other.gameObject.layer);
        //Debug.Log("LeftHandTagged: " + leftHandTagged);
        //Debug.Log("RightHandTagged: " + rightHandTagged);
    }

    private bool checkTrigger(SteamVR_Input_Sources hand)
    {
        return climbAction.GetState(hand);
    }

}
