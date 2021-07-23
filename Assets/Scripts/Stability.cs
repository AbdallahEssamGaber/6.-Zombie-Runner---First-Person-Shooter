using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Stability : MonoBehaviour
{
     Weapon[] weapons;
    private void Start()
    {
        weapons = GetComponentsInChildren<Weapon>();
    }
    float unStab;
    public IEnumerator messUp(float maxUnStab, float incUnStab)
    {
        while (-maxUnStab < -unStab)
        {
            StartCoroutine(Norma());
            transform.localRotation = Quaternion.Euler(-unStab, 0, 0);
            unStab += Time.deltaTime * 100;
            yield return new WaitForFixedUpdate();
        }
        foreach (Weapon weapon in weapons)
        {
            if (weapon.maxUnStab < weapon.incUnStab)
            {
                weapon.maxUnStab = weapon.incUnStab;
                BackToNormal();
            }
        }
    }
    public IEnumerator Norma()
    {
        while (-unStab < 0.01)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime / 50);
            unStab -= Time.deltaTime / 5;
            BackToNormal();
            yield return new WaitForFixedUpdate();
        }
    }
    private void BackToNormal()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.maxUnStab = unStab + weapon.incUnStab;
        }
    }
}
