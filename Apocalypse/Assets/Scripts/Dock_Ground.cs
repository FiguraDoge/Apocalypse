using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// This dock is for player to enter the boat
public class Dock_Ground : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;

    public Transform seat;
    public GameObject text;

    private bool CheckTrigger()
    {
        return grabAction.GetState(handType);
    }

    // If player is inside the trigger area
    void OnTriggerStay(Collider other)
    {
        // Show the UI
        text.SetActive(true);
        if (CheckTrigger() && other.tag == "Player")
        {
            // Move player to the position
            GameObject player = other.gameObject.transform.parent.parent.gameObject;
            player.transform.position = seat.position;

            // Set player to ROW mode
            PlayerManager playerManager = PlayerManager.instance;
            playerManager.mode = "ROW";
        }
    }

    // If player is outside the trigger area 
    void OnTriggerExit()
    {
        text.SetActive(false);
    }
}
