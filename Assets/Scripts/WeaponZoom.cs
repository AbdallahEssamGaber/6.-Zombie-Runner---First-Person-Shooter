using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] GameObject scoopOverlay;
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject weaponCamera;
    [SerializeField] Camera camerMain;
    [SerializeField] FirstPersonController fpsController;
    [SerializeField] float zommed_in = 10, zommed_out = 10;
    [SerializeField] float zommed_in_sen = 0.5f, zommed_out_sen = 2f;

    public bool isZoomed = false;

 

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ZoomOn();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ZoomOut();
        }


        if(!GetComponent<Weapon>().isAvailable && GetComponent<Weapon>().isTaken)
        {
            ZoomOut();
            this.enabled = false;
        }

    }

    void ZoomOn()
    {
        isZoomed = true;
        StartCoroutine(ScoopOverlay());
        animator.SetBool("scooped", true);
        weaponCamera.SetActive(false);

        camerMain.fieldOfView = zommed_in;
        fpsController.sensitivityX = zommed_in_sen;
        fpsController.sensitivityY = zommed_in_sen;
    }

    public void ZoomOut()
    {
        isZoomed = false;
        scoopOverlay.SetActive(false);
        reticle.SetActive(true);
        animator.SetBool("scooped", false);
        weaponCamera.SetActive(true);

        camerMain.fieldOfView = zommed_out;
        fpsController.sensitivityX = zommed_out_sen;
        fpsController.sensitivityY = zommed_out_sen;
    }

    IEnumerator ScoopOverlay()
    {
        while (isZoomed)
        {
            scoopOverlay.SetActive(true);
            reticle.SetActive(false);
            yield return null;
        }
        
  


    }
}
