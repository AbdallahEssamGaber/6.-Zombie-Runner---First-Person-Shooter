using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAndCuffs : MonoBehaviour
{

    [SerializeField] int hitsToDestroy = 3;
    public bool canPickUpWeapons = false;

    bool on = false;

    int counter = 0;


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pole")
        {
            on = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pole")
        {
            on = false;
        }
    }


    void Update()
    {

        if(counter == hitsToDestroy)
        {
          print("sdfsdfsdf");
          canPickUpWeapons = true;
          return;
        }

        if (on)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //do stuff
                print("walaasdfsdfsa");
                counter++;
            }
        }
    }
}
