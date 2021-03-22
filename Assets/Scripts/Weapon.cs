using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera cameraObject;
    [SerializeField] float range = 0f;
    [SerializeField] int damage = 1;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;     //cuz the error if u hit smth dont have EnemyHealth
            target.Damage(damage);


        }
        else    //just to making sure "PROGRAMMING"
        {
            return;
        }
    }
}
