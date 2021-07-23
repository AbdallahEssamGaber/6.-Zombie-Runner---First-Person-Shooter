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
    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject hitEffectRest;
    [SerializeField] GameObject hitEffectEnemy;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] public AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Animator aim;
    [SerializeField] Animator gunAnimator;
    [SerializeField] public bool isTaken = false;
    [SerializeField] public GameObject reticle;
    [SerializeField] public float maxUnStab;
    [SerializeField] public float incUnStab;

    public bool canShoot = false;
    public bool isAvailable = false;
    ShootPos shootPos;
    Stability stability;

    private void Start()
    {
        ammoText.text = "";
        reticle.SetActive(false);

        shootPos = FindObjectOfType<ShootPos>();
        stability = FindObjectOfType<Stability>();
    }



    void Update()
    {

        if (isAvailable)
        {
            ammoText.text = ammoSlot.GetCurrentAmmo(ammoType).ToString();
        }
        else
        {
            return;
        }

        if (ammoType == AmmoType.Pistol)
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) <= 0)
            {
                gameObject.GetComponentInChildren<SimpleShoot>().enabled = false;
                canShoot = false;
            }
            if (aim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            {
                gunAnimator.ResetTrigger("Fire");
            }
           
        } else if (ammoType != AmmoType.Pistol)
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) <= 0)
            {
                canShoot = false;
            }
        }

        if (ammoSlot.GetCurrentAmmo(ammoType) <= 0)
        {
            canShoot = false;
        }

        if (Input.GetMouseButtonDown(0) && canShoot == true && aim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            
            StartCoroutine(Shoot());
        }



    }

   
    IEnumerator Shoot()
    {
        canShoot = false;
        GetComponent<ShakeTrigger>().Stomp();

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
    
                PlayMuzzleFlash();
        }

        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo(ammoType);

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        //Create the muzzle flash
        GameObject tempFlash;
        tempFlash = Instantiate(muzzleFlash, barrelLocation.position, barrelLocation.rotation);

        //Destroy the muzzle flash effect
        Destroy(tempFlash, destroyTimer);
    }

 

    public void ProcessRaycast()
    {
        
        RaycastHit hit;
        Ray ray = new Ray(shootPos.transform.position, shootPos.transform.forward);
        if (Physics.Raycast(ray, out hit, range))
        {

            if (hit.transform.tag == "head" || hit.transform.tag == "enemy")
            {
                CreateHitImpactBody(hit);
                EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
                target.TakeDamage(damage * 2);
            }
            else 
            {
                CreateHitImpactRest(hit);
            }
            
            
        }
        RaycastHit ss;
        Ray ray2 = new Ray(shootPos.transform.position, shootPos.transform.forward);
        if (Physics.Raycast(ray2, out ss))
        {
            hitterDamage targetB = ss.transform.GetComponent<hitterDamage>();
            if (targetB == null) return;

            targetB.TakeDamageHitter(damage);
        }
 

    }

  

    void CreateHitImpactRest(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffectRest, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    } 
    void CreateHitImpactBody(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffectEnemy, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
