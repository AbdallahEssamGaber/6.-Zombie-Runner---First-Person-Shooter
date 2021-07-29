using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAndCuffs : MonoBehaviour
{
    bool on = false;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Hands")
        {
            on = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hands")
        {
            on = false;
        }
    }


    void Update()
    {
        if (on)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                //do stuff
                print("walaasdfsdfsa");
            }
        }
    }
}
