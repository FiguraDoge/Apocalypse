using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Row : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject boat;
    public Transform seat;
    public PlayerManager playerManager;

    private Rigidbody rbr;
    private Rigidbody rbl;
    private Rigidbody boatRB;

    private SteamVR_Behaviour_Pose trackedObjectLeft;
    private SteamVR_Behaviour_Pose trackedObjectRight;

    void Start()
    {
        playerManager = PlayerManager.instance;
        rbr = rightHand.GetComponent<Rigidbody>();
        rbl = leftHand.GetComponent<Rigidbody>();
        boatRB = boat.GetComponent<Rigidbody>();

        trackedObjectRight = rightHand.GetComponent<SteamVR_Behaviour_Pose>();
        trackedObjectLeft = leftHand.GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        // Debug check
        // Debug.Log("left hand velocity = " + trackedObjectLeft.GetVelocity());
        // Debug.Log("right hand velocity = " + trackedObjectRight.GetVelocity());
        
        // If the player is in row mode 
        if (playerManager.mode != "ROW")
        {
            return;
        }
        else
        {
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

            this.gameObject.transform.position = seat.position;
        }
    }
}
