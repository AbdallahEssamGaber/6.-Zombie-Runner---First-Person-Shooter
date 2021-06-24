using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Animator aim;
    [SerializeField] Animator gunAnimator;


    bool canShoot = true;

  



    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {

        if (ammoType == AmmoType.Bullets)
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) <= 0)
            {
                gameObject.GetComponentInChildren<SimpleShoot>().enabled = false;
                canShoot = false;
            }
            if (aim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            }
            else
            {
                gunAnimator.ResetTrigger("Fire");

            }
        }

        DisplayAmmo();

        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();

    }
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoType != AmmoType.Bullets)
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
            {
                PlayMuzzleFlash();
            }
            
        }

        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo(ammoType);

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    public void ProcessRaycast()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            if (hit.transform.tag == "head")
            {
                EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
                target.TakeDamage(20f);
            }
            else if (hit.transform.tag == "enemy")
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                target.TakeDamage(damage);
            }
            
            
        }
   

        RaycastHit ss;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out ss))
        {
            hitterDamage targetB = ss.transform.GetComponent<hitterDamage>();
            if (targetB == null) return;

            targetB.TakeDamageHitter(damage);
        }
 

    }

  

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
