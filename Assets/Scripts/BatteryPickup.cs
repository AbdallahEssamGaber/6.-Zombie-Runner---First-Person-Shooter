using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngel = 90f;
    [SerializeField] float AddIntensity = 1f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        
        other.GetComponentInChildren<FlashLIght>().RestoreLightAngle(restoreAngel);
        other.GetComponentInChildren<FlashLIght>().AddIntensity(AddIntensity);
        Destroy(gameObject);
    }
}
