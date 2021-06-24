using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLIght : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;


    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    void DecreaseLightIntensity()
    {
        if(myLight.spotAngle <= minimumAngle)
        {
            return;
        }
        myLight.spotAngle -= angleDecay * Time.deltaTime;
    }

    void DecreaseLightAngle()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }


    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }
    
    public void AddIntensity(float AddIntensity)
    {
        myLight.intensity += AddIntensity;
    }
}
