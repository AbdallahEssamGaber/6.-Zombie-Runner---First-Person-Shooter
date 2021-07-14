using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenWeapons : MonoBehaviour
{
    Weapon[] weapons;
    [SerializeField] float radius = 0.9f;

    public int weaponFound = 0;

    private void OnTriggerEnter(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            switch (hitCollider.tag)
            {
                case "Pistol":      //todo put the same tags
                    weaponFound += 1;
                    weapons = GetComponentsInChildren<Weapon>();
                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.ammoType == AmmoType.Pistol)
                        {
                            weapon.isTaken = true;
                            weapon.reticle.SetActive(true);
                            if (weaponFound == 1)
                            {
                                GetComponentInChildren<WeaponSwitcher>().currentWeapon = 0;
                                GetComponentInChildren<WeaponSwitcher>().SwitchWeapon();
                            }
                           // hitCollider.GetComponent<AudioSource>().Play();
                            Destroy(hitCollider.GetComponent<BoxCollider>());
                            MeshRenderer[] meshs = hitCollider.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer mesh in meshs)
                            {
                                Destroy(mesh);
                            }
                            Destroy(hitCollider.gameObject,2);
                        }
                    }
                    break;
                case "Shotgun":  //todo put the same tags

                    weaponFound += 1;
                    weapons = GetComponentsInChildren<Weapon>();
                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.ammoType == AmmoType.Shotgun)
                        {
                            weapon.isTaken = true;
                            weapon.reticle.SetActive(true);

                            if (weaponFound == 1)
                            {
                                GetComponentInChildren<WeaponSwitcher>().currentWeapon = 1;
                                GetComponentInChildren<WeaponSwitcher>().SwitchWeapon();
                            }
                            //hitCollider.GetComponent<AudioSource>().Play();
                            Destroy(hitCollider.GetComponent<BoxCollider>());
                            MeshRenderer[] meshs = hitCollider.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer mesh in meshs)
                            {
                                Destroy(mesh);
                            }
                            Destroy(hitCollider.gameObject,2);
                        }
                    }
                    break;
                case "Carbine":  //todo put the same tags

                    weaponFound += 1;
                    weapons = GetComponentsInChildren<Weapon>();
                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.ammoType == AmmoType.Sniper)
                        {
                            weapon.isTaken = true;
                            weapon.reticle.SetActive(true);

                            if (weaponFound == 1)
                            {
                                GetComponentInChildren<WeaponSwitcher>().currentWeapon = 2;
                                GetComponentInChildren<WeaponSwitcher>().SwitchWeapon();
                            }
                            //hitCollider.GetComponent<AudioSource>().Play();
                            Destroy(hitCollider.GetComponent<BoxCollider>());
                            MeshRenderer[] meshs = hitCollider.GetComponentsInChildren<MeshRenderer>();
                            foreach (MeshRenderer mesh in meshs)
                            {
                                Destroy(mesh);
                            }
                            Destroy(hitCollider.gameObject,2);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
