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
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canShot = true;
    private bool allowFire = true;
    float timeBetweenShot = 0f;
    private void OnEnable()
    {
        // How long has it been since the last shot
        float deltaTime = Time.time - timeBetweenShot;
        if (!allowFire)
        {
            StartCoroutine(CoolDown(deltaTime));
        }
    }

    private IEnumerator CoolDown(float passedTime)
    {
        yield return new WaitForSeconds(timeBetweenShots - passedTime);
        allowFire = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammoSlot.CurrentAmmo(ammoType) > 0 && canShot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        allowFire = false;
        // the last shot's time
        timeBetweenShot = Time.time;
        canShot = false;
        muzzleFlash.Play();
        RaycastThing();
        ammoSlot.ReduceAmmo(ammoType);
        yield return new WaitForSeconds(timeBetweenShots);
        timeBetweenShot = 0f;
        allowFire = true;
        canShot = true;
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
