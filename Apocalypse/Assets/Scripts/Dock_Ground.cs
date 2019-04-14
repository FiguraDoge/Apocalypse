using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/* 
 * This dock_ground is for player to enter the boat
 * For each dock_ground there should be one gameobject boat
 * If boat == null, then there is no available boat in the port
 * If boat != null, then there is one boat available
 * When player pull the trigger inside the dock, dock should send player to the boat in dock
 * unless there is no available boat
 * Also, each dock_ground has a corresponding dock_water.
*/

public class Dock_Ground : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;

    public GameObject boardText;        // the UI text
    public GameObject boat;             // the boat in port 
    public GameObject dock_water;       // corresponding dock_water
    public PlayerManager playerManager;

    private Dock_Water dockWater;
    private Transform seat;             // the seat for player on boat 
    private Text text;

    private bool CheckTrigger()
    {
        return grabAction.GetState(handType);
    }

    public void setBoat(GameObject boat)
    {
        this.boat = boat;
    }

    public GameObject getBoat()
    {
        return this.boat;
    }

    void Start()
    {
        seat = boat.transform.GetChild(0);
        dockWater = dock_water.GetComponent<Dock_Water>();
        playerManager = PlayerManager.instance;
        text = boardText.GetComponent<Text>();
    }

    void OnTriggerStay(Collider other)
    {
        // If player is inside the trigger area
        if (other.tag == "Player")
        {
            if (boat != null)
            {
                text.text = "Press Trigger to enter the boat";
                if (CheckTrigger())
                {
                    // Move player to the position
                    GameObject player = other.gameObject;
                    player.transform.position = seat.position;

                    boat.GetComponent<Row>().setPlayerOnBoard(true);
                    player.GetComponent<Rigidbody>().useGravity = false;

                    playerManager.setBoat(boat);
                    this.boat = null;
                }
            }
            else
            {
                text.text = "There is no available boat in this dock";
            }
        }
    }

    // If player is outside the trigger area 
    void OnTriggerExit()
    {
        text.text = "";
    }
}
