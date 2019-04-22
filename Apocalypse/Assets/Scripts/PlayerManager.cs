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
    public GameObject teleport;

    private GameObject boat;
    private Rigidbody playerRB;

    public GameObject blackScreen;
    private bool fadingBlack;
    private bool fadingIn;

    void Start()
    {
        playerRB = this.gameObject.GetComponent<Rigidbody>();
        fadingBlack = false;
        fadingIn = false;
    }

    //
    void Update()
    {
        if (playerRB.velocity.y < -4) //check for if player is falling
        {
            fadingBlack = true; //set to true to call the fading object
            playerRB.velocity = new Vector3(0, 0, 0); //set the velocity to 0 so player stops falling
            setGravity(false); //turn gravity off so player stops falling
        }
        if (fadingBlack) //call fade() if necessary
        {
            fade();
        }
        if (fadingIn) //call fadingIn() if necessary
        {
            fadeIn();
        }
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

    public void fade() //Fades the black screen in
    {
        Debug.Log("Fading");
        if (blackScreen.transform.localScale.x < .5 || blackScreen.transform.localScale.z < .5)
        {
            blackScreen.transform.localScale += new Vector3(.005f, 0, .005f);
            if (blackScreen.transform.localScale.x > .5 || blackScreen.transform.localScale.z > .5)
            {
                fadingBlack = false;
                fadingIn = true;
                setGravity(true);
                transform.position = new Vector3(22.7f, .7f, -19.41f); //put them in certain spot
            }
        }
        else
        {
            fadingBlack = false;
            fadingIn = true;
            setGravity(true);
            transform.position = new Vector3(22.7f, .7f, -19.41f); //put them in certain spot
        }
    }

    public void fadeIn() //Fades the black screen out (the name of the function is a little misleading but you get the point :P )
    {
        Debug.Log("Fading In");
        if (blackScreen.transform.localScale.x > 0 || blackScreen.transform.localScale.z > 0)
        {
            blackScreen.transform.localScale -= new Vector3(.005f, 0, .005f);
            if (blackScreen.transform.localScale.x < 0 || blackScreen.transform.localScale.z < 0)
            {
                fadingIn = false;
                blackScreen.transform.localScale = new Vector3(0, 1, 0);
            }
        }
        else
        {
            fadingIn = false;
        }
    }
}
