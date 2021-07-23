using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPos : MonoBehaviour
{
    Transform staTrans;

    private void Start()
    {
        staTrans = FindObjectOfType<Stability>().transform;
    }
    void Update()
    {
        transform.rotation = staTrans.rotation;
    }
}
