using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmountIncreasing = 5;
    [SerializeField] AmmoType ammoType;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseAmmo(ammoType, ammoAmountIncreasing);
            Destroy(gameObject);
        }
    }
}