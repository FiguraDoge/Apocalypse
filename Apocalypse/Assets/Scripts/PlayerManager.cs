using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
    private GameObject boat;
    private Rigidbody playerRB;

    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
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
}
