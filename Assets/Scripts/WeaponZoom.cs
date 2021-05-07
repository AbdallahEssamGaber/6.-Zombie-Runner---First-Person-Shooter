using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float zommed_in = 10, zommed_out = 10;
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = zommed_in;
            Debug.Log("Zoomed In");
        }
        else
        {
            Camera.main.fieldOfView = zommed_out;
        }
    }
}
