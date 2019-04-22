using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private GameObject boat;
    private Rigidbody playerRB;

    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        setLayserPointer(false);
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
        this.gameObject.transform.position = position;
    }

    public void setLayserPointer(bool val)
    {
        leftHand.GetComponent<SteamVR_LaserPointer>().enabled = val;
        rightHand.GetComponent<SteamVR_LaserPointer>().enabled = val;
    }
}
