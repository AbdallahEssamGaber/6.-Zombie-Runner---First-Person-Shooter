using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera cameraObject;
    [SerializeField] float range = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out RaycastHit hit, range);
        //if (hit.transform != null)
        //{

        //    print(hit.transform.name);

        //}
        if (hit.transform.name == "Enemy")
        {

            print(hit.transform.name);

        }
    }
}
