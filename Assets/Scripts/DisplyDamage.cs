using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplyDamage : MonoBehaviour
{

    [SerializeField] Canvas impactCanves;
    [SerializeField] float impactTime = 0.3f;


    void Start()
    {
        impactCanves.enabled = false;

    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    IEnumerator ShowSplatter()
    {
        impactCanves.enabled = true;
        yield return new WaitForSeconds(impactTime);
        impactCanves.enabled = false;
    }
}
