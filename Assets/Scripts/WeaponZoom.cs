using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float zommed_in = 10, zommed_out = 10;
    RigidbodyFirstPersonController fpsController;
    [SerializeField] float zommed_in_sen = 0.5f, zommed_out_sen = 2f;

    void Start()
    {
        fpsController = GetComponent<RigidbodyFirstPersonController>();
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = zommed_in;
            fpsController.mouseLook.XSensitivity = zommed_in_sen;
            fpsController.mouseLook.YSensitivity = zommed_in_sen;
            Debug.Log("Zoomed In");
        }
        else
        {
            fpsController.mouseLook.XSensitivity = zommed_out_sen;
            fpsController.mouseLook.YSensitivity = zommed_out_sen;
            Camera.main.fieldOfView = zommed_out;
        }
    }
}
