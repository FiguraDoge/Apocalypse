using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour
{
    public GameObject goal;
    public float speed;
    public float finalDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, goal.transform.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, goal.transform.position) < finalDistance)
        {
            endingScene();
        }
    }

    void endingScene()
    {

    }


}
