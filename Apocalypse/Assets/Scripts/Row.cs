using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/*
 * Row is attacked to boat, and row only works when player is on boards 
 */

[RequireComponent(typeof(Rigidbody))]
public class Row : MonoBehaviour
{
    private bool playerOnBoard;
    public PlayerManager playerManager;
    public Transform seat;

    private Rigidbody rbr;
    private Rigidbody rbl;

    private GameObject boat;
    private Rigidbody boatRB;

    private GameObject rightHand;
    private GameObject leftHand;
    private SteamVR_Behaviour_Pose trackedObjectLeft;
    private SteamVR_Behaviour_Pose trackedObjectRight;

    public void setPlayerOnBoard(bool val)
    {
        this.playerOnBoard = val;
    }

    public bool getPlayerOnBoard()
    {
        return this.playerOnBoard;
    }

    void Start()
    {
        playerOnBoard = false;
        playerManager = PlayerManager.instance;

        rbr = playerManager.rightHand.GetComponent<Rigidbody>();
        rbl = playerManager.leftHand.GetComponent<Rigidbody>();

        boat = this.gameObject;
        boatRB = boat.GetComponent<Rigidbody>();

        trackedObjectRight = playerManager.rightHand.GetComponent<SteamVR_Behaviour_Pose>();
        trackedObjectLeft = playerManager.leftHand.GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        // Debug check
        // Debug.Log("left hand velocity = " + trackedObjectLeft.GetVelocity());
        // Debug.Log("right hand velocity = " + trackedObjectRight.GetVelocity());
        
        // If the player is in row mode 
        if (!playerOnBoard)
            return;
      
        // If player pull the left controller
        // The boat should go right and forward
        if (trackedObjectLeft.GetVelocity().z < -1)
        {
            boatRB.AddForce(boat.transform.right * .5f);
            // This is a velocity cap for the forawrd direction
            if (Vector3.Dot(boatRB.velocity, boat.transform.forward) <= 2f)
                boatRB.AddForce(boat.transform.forward * .5f);
            boat.transform.Rotate(0, 1f, 0, Space.Self);
        }

        // If player pull the right controller
        // The boat should go left and forward
        if (trackedObjectRight.GetVelocity().z < -1)
        {
            boatRB.AddForce(-transform.right * .5f);
            // This is a velocity cap for the forawrd direction
            if (Vector3.Dot(boatRB.velocity, boat.transform.forward) <= 5f)
                boatRB.AddForce(boat.transform.forward * .5f);
            boat.transform.Rotate(0, -1f, 0, Space.Self);
        }

        // Adding friction to the boat 
        boatRB.AddForce(boatRB.velocity * -.075f);
        playerManager.gameObject.transform.position = seat.position;

    }
}
