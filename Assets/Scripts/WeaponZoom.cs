using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
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
        camerMain.fieldOfView = zommed_in;
        fpsController.mouseLook.XSensitivity = zommed_in_sen;
        fpsController.mouseLook.YSensitivity = zommed_in_sen;
        Debug.Log("Zoomed In");
    }

    void ZoomOut()
    {
        camerMain.fieldOfView = zommed_out;
        fpsController.mouseLook.XSensitivity = zommed_out_sen;
        fpsController.mouseLook.YSensitivity = zommed_out_sen;
    }
}
