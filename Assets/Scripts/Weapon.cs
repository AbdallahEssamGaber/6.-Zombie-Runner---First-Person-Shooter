using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Camera cameraObject;
    [SerializeField] float range = 0f;
    [SerializeField] int damage = 1;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitImpactVFX;
    [SerializeField] Ammo ammoSlot;

    void Update()
    {
        if (Input.GetButtonDown("Fire") && ammoSlot.CurrentAmmo() > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastThing();
        ammoSlot.ReduceAmmo();
    }

    void RaycastThing()
    {
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out RaycastHit hit, range))
        {
            HitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;     //cuz the error if u hit smth dont have EnemyHealth
            target.Damage(damage);


        }
        else    //just to making sure "PROGRAMMING"
        {
            return;
        }
    }

    void HitImpact(RaycastHit hit)
    {
        GameObject hitImpactObj = Instantiate(hitImpactVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitImpactObj, 0.1f); 
    }
}
