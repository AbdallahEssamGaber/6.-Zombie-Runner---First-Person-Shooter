using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitterDamage : MonoBehaviour
{
    [SerializeField] float hitPoints = 50f;
    [SerializeField] Animator door;
    [SerializeField] Animator enterence;
    [SerializeField] firstanimconfig emilly;

    bool notEntered = true;
    bool finished, toPreventTheStart, toEnterOneTime;
    public void TakeDamageHitter(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0 && notEntered)
        {
            Open();
        }
    }

    void Open()
    {
        notEntered = false;
        GetComponent<MeshRenderer>().enabled = false;
        door.SetTrigger("doorOpen");
    }

    void Update()
    {
        if (!notEntered)
        {
            enterence.SetTrigger("closeDoor");
        }

        if (enterence.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            toPreventTheStart = true;
            if (finished && !toEnterOneTime) 
            {
                toEnterOneTime = true;
                emilly.BeforeScene();
            }

        }
        else
        {
            if(toPreventTheStart)
            {
                finished = true;
            }
            
        }

    }

}
