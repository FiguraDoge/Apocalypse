using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// This dock is for player to park the boat

public class Dock_Water : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;

    public GameObject boat;
    public Transform landPosition;      // This is going to be where player land
    public Transform dockPosition;    // This is going to be position after player park the boat
    public GameObject text;
    public PlayerManager playerManager;

    private bool CheckTrigger()
    {
        return grabAction.GetState(handType);
    }

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    // Inside the trigger area
    void OnTriggerStay(Collider other)
    {
        // Show the UI
        if (playerManager.mode == "ROW")
            text.SetActive(true);

        // If boat is inside
        if (CheckTrigger() && other.tag == "Player")
        {
            // Move player to the ground, and player is no longer a child of boat
            GameObject player = other.transform.parent.parent.gameObject;
            player.transform.position = landPosition.position;
            playerManager.mode = "WALK";

            // Move boat to the parkPosition
            boat.transform.position = dockPosition.position;
            Rigidbody boatRB = boat.GetComponent<Rigidbody>();
            boatRB.velocity = new Vector3(0,0,0);
        }
    }

    // If player is outside the trigger area 
    void OnTriggerExit()
    {
        text.SetActive(false);
    }
}
