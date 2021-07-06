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
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zommed_in = 10, zommed_out = 10;
    [SerializeField] float zommed_in_sen = 0.5f, zommed_out_sen = 2f;



    void OnDisable()
    {
        ZoomOut();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomOn();
        }
        else
        {
            ZoomOut();
        }

    }

    void ZoomOn()
    {
        StartCoroutine(ScoopOverlay());
        animator.SetBool("scooped", true);
        weaponCamera.SetActive(false);

        camerMain.fieldOfView = zommed_in;
        fpsController.mouseLook.XSensitivity = zommed_in_sen;
        fpsController.mouseLook.YSensitivity = zommed_in_sen;
    }

    void ZoomOut()
    {
        scoopOverlay.SetActive(false);
        reticle.SetActive(true);
        animator.SetBool("scooped", false);
        weaponCamera.SetActive(true);

        camerMain.fieldOfView = zommed_out;
        fpsController.mouseLook.XSensitivity = zommed_out_sen;
        fpsController.mouseLook.YSensitivity = zommed_out_sen;
    }

    IEnumerator ScoopOverlay()
    {
        yield return new WaitForSeconds(0.15f);
  
        scoopOverlay.SetActive(true);
        reticle.SetActive(false);


    }
}
