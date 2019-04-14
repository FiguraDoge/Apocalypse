using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbingTrigger : MonoBehaviour
{
    private bool leftHandTagged;
    private bool rightHandTagged;

    private Vector3 prevLeftPos;
    private Vector3 prevRightPos;

    public SteamVR_Input_Sources LeftHandSource;
    public SteamVR_Input_Sources rightHandSource;
    public SteamVR_Action_Boolean climbAction;

    public GameObject leftHandObject;
    public GameObject rightHandObject;
    public GameObject PlayerObject;
    public Rigidbody playerGravityRigibBody;

    public bool xMove;
    public bool yMove;
    public bool zMove;

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
            //disable gravity when either of the hand is triggered.
            playerGravityRigibBody.useGravity = false;
            //deactivate when the player release the trigger
            if (!checkTrigger(LeftHandSource))
            {
                leftHandTagged = false;
            }

            if (!checkTrigger(rightHandSource))
            {
                rightHandTagged = false;
            }
            if(!rightHandTagged && !leftHandTagged)
            {
                //enable gravity so player may fall down
                playerGravityRigibBody.useGravity = true;
            }

            //depending on the current hands status, move the player
            if(leftHandTagged && rightHandTagged)
            {
                //when both hands are triggered
                //do nothing for right now
                
            }
            else if (leftHandTagged)
            {
                //when only left hand is attached, move the player according to the y value change in left hand's position
                PlayerObject.transform.position -= new Vector3(xMove ? leftHandObject.transform.position.x - prevLeftPos.x : 0, yMove ? leftHandObject.transform.position.y - prevLeftPos.y : 0, zMove ? leftHandObject.transform.position.z - prevLeftPos.z : 0);
            }
            else if (rightHandTagged)
            {
                //when only right hand is attached, move the player according to the y value change in right hand's position
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
        if (other.gameObject.tag.Equals("LeftHand") && checkTrigger(LeftHandSource))
        {
            //Debug.Log("Triggered By LeftHand");
            leftHandTagged = true;
        }

        if (other.gameObject.tag.Equals("RightHand"))
        {
            //Debug.Log("Triggered By RightHand");
            if (checkTrigger(rightHandSource))
            {
                rightHandTagged = true;
            }
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
