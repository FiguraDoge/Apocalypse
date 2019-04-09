using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// For every dock, there is a position for the boat

public class Dock : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;

    public Transform seat;
    public GameObject text;

    private bool CheckTrigger()
    {
        return grabAction.GetState(handType);
    }

    void OnTriggerStay(Collider other)
    {
        if (CheckTrigger())
        {
            // Show the UI
            text.SetActive(true);
            // Move player to the position
            other.transform.position = seat.position;
        }
    }
}
