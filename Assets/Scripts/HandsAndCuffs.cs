using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAndCuffs : MonoBehaviour
{
    [SerializeField] int hitsToDestroy = 3;
    public bool canPickUpWeapons = false;

    bool on = false;

    int counter = 0;

    Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/Hand Idle.controller", typeof(RuntimeAnimatorController));
    }
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
    


        if (counter == hitsToDestroy)
        {
            print("sdfsdfsdf");
            canPickUpWeapons = true;
            //todo: destroy arms after animation done
            return;
        }



        //todo: if out of collider dont complete break the cuffs and TB

        if (on)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                print("walaasdfsdfsa");
                counter++;
            }





        }



    }
}
