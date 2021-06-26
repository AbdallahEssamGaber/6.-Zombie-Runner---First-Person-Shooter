using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapons : MonoBehaviour
{
    [SerializeField] GameObject[] setActive;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            foreach(GameObject obj in setActive)
            {
                obj.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
