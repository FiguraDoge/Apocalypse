using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        MatchController mc = other.gameObject.GetComponent<MatchController>();
        if(mc != null)
        {
            if (mc.isOnFire())
            {
                fire.SetActive(true);
            }
        }
    }
}
