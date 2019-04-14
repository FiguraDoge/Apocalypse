using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/* 
 * This dock_water is for player to port the boat
 * For each water there should be one gameobject boat
 * If boat == null, then there is available port for boat
 * If boat != null, then there is no available port
 * When player pull the trigger inside the dock, dock should send player to the lnad, and send the boat to the port
 * unless there is no available port
 * Also, each dock_water has a corresponding dock_ground.
*/

public class Dock_Water : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;

    public GameObject dock_ground;
    public Transform landPosition;      // This is going to be where player land
    public Transform dockPosition;    // This is going to be position after player park the boat
    public GameObject portText;
    public PlayerManager playerManager;

    private Text text;
    private Dock_Ground dockGround;

    private bool CheckTrigger()
    {
        return grabAction.GetState(handType);
    }

    void Start()
    {
        playerManager = PlayerManager.instance;
        text = portText.GetComponent<Text>();
        dockGround = dock_ground.GetComponent<Dock_Ground>();
    }

    // Inside the trigger area
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dockGround.getBoat() == null)
            {
                text.text = "Press the trigger to port the boat";
                
                if (CheckTrigger())
                {
                    // Move player to the ground, and player is no longer a child of boat
                    GameObject player = other.gameObject;
                    player.transform.position = landPosition.position;
                    player.GetComponent<Rigidbody>().useGravity = true;

                    // Move boat to the parkPosition
                    GameObject boat = playerManager.getBoat();
                    boat.transform.position = dockPosition.position;
                    boat.GetComponent<Row>().setPlayerOnBoard(false);
                    boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                    playerManager.setBoat(null);
                    dockGround.setBoat(boat);
                }
            }
            else
            {
                text.text = "There is no available port in this dock";
            }
        }
    }

    // If player is outside the trigger area 
    void OnTriggerExit()
    {
        text.text = "";
    }
}
