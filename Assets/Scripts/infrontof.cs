using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class infrontof : MonoBehaviour
{
    [SerializeField] firstanimconfig emilly;

    bool wasOn;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !wasOn)
        {
            wasOn = true;
            emilly.GoThere();
        }
    }


  

}
