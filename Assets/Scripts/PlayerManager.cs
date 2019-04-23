using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

[RequireComponent(typeof(Rigidbody))]
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject teleport;

    private Vector3 respawn;
    private GameObject boat;
    private Rigidbody playerRB;

    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        respawn = this.transform.position;
    }

    public void setBoat(GameObject boat)
    {
        this.boat = boat;
    }

    public GameObject getBoat()
    {
        return this.boat;
    }
    
    public void setGravity(bool val)
    {
        playerRB.useGravity = val;
    }

    public void setTeleport(bool val)
    {
        teleport.SetActive(val);
    }

    public void setPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void setRespawn(Vector3 position)
    {
        this.respawn = position;
    }

    public void setLaserPointer(bool val)
    {
        if (val)
        {
            leftHand.GetComponent<SteamVR_LaserPointer>().enabled = true;
            rightHand.GetComponent<SteamVR_LaserPointer>().enabled = true;
        }
        else
        {
            var holder = leftHand.GetComponent<SteamVR_LaserPointer>().holder;
            var pointer = leftHand.GetComponent<SteamVR_LaserPointer>().pointer;
            leftHand.GetComponent<SteamVR_LaserPointer>().enabled = false;
            Destroy(holder);
            Destroy(pointer);

            var holder2 = rightHand.GetComponent<SteamVR_LaserPointer>().holder;
            var pointer2 = rightHand.GetComponent<SteamVR_LaserPointer>().pointer;
            rightHand.GetComponent<SteamVR_LaserPointer>().enabled = false;
            Destroy(holder2);
            Destroy(pointer2);
        }
    }
}
