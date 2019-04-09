using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Row : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;

    private Rigidbody rbr;
    private Rigidbody rbl;
    private Rigidbody boatRB;

    public SteamVR_Behaviour_Pose trackedObjectLeft;
    public SteamVR_Behaviour_Pose trackedObjectRight;

    void Start()
    {
        rbr = rightHand.GetComponent<Rigidbody>();
        rbl = leftHand.GetComponent<Rigidbody>();
        boatRB = this.gameObject.GetComponent<Rigidbody>();

        trackedObjectRight = rightHand.GetComponent<SteamVR_Behaviour_Pose>();
        trackedObjectLeft = leftHand.GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        Debug.Log("left hand velocity = " + trackedObjectLeft.GetVelocity());
        Debug.Log("right hand velocity = " + trackedObjectRight.GetVelocity());

        if (trackedObjectLeft.GetVelocity().z < -1) {
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * .5f);
            if (Vector3.Dot(boatRB.velocity, transform.forward) <= 2f)
            {
                this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * .5f);
            }
            this.transform.Rotate(0, 1, 0, Space.Self);
        }
        if (trackedObjectRight.GetVelocity().z < -1)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * .5f);
            if (Vector3.Dot(boatRB.velocity, transform.forward) <= 5f)
            {
                this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * .5f);
            }
            this.transform.Rotate(0, -1, 0, Space.Self);
        }

        // Vector3 dir = boatRB.velocity;
        //dir.Normalize();
        this.gameObject.GetComponent<Rigidbody>().AddForce(boatRB.velocity * -.075f);

    }
}
