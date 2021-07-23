using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] public int currentWeapon;
    [SerializeField] TextMeshProUGUI ammoText;

    public int weaponIndex;

    void Start()
    {
        weaponIndex = 0;
    }

    private void Update()
    {
        int previousWeapon = currentWeapon;//////////////trickyyyy
        if (GetComponentInParent<TakenWeapons>().weaponFound < 2) return;
        processInputSwitch();
        processScrollSwitch();

        if (previousWeapon != currentWeapon)/////////////trickyyyy
        {
            SwitchWeapon();
        }
    }

    private void processInputSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            //SwitchWeapon(); removed cuz the tricky thing
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            //SwitchWeapon(); removed cuz the tricky thing
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            //SwitchWeapon(); removed cuz the tricky thing
        }
    }
    private void processScrollSwitch()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
                //SwitchWeapon(); removed cuz the tricky thing
            }
            else
            {
                currentWeapon += 1;
                //SwitchWeapon(); removed cuz the tricky thing
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = 2;
                //SwitchWeapon(); removed cuz the tricky thing
            }
            else
            {
                currentWeapon -= 1;
                //SwitchWeapon(); removed cuz the tricky thing
            }
        }
    }

    public void SwitchWeapon()
    {
        foreach (Transform weapon in gameObject.transform)
        {
            if (weaponIndex == currentWeapon)
            {
                if (weapon.GetComponentInChildren<Weapon>().isTaken == false)
                {
                    SwitchWeapon();
                    return;
                }
                EnableWeapon(weapon);
            }
            else
            {
                DisableWeapon(weapon);
            }
            if (weaponIndex >= transform.childCount - 1)
            {
                weaponIndex = 0;
            }
            else
            {
                weaponIndex += 1;
            }
        }

    }
    public void SwitchOff()
    {
        foreach (Transform weapon in gameObject.transform)
        {
            DisableWeapon(weapon);
        }
    }
    public void EnableWeapon(Transform weapon)
    {
        
        MeshRenderer[] meshs = weapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.enabled = true;
        }
        weapon.GetComponentInChildren<Weapon>().canShoot = true;
        weapon.GetComponentInChildren<Weapon>().isAvailable = true;
        if (weapon.GetComponentInChildren<Weapon>().ammoType == AmmoType.Sniper)
        {
            WeaponZoom wZ = weapon.GetComponentInChildren<WeaponZoom>();
            if (wZ == null) return;
            wZ.enabled = true;
        }
        if (weapon.GetComponentInChildren<Weapon>().ammoType == AmmoType.Pistol)
        {
            weapon.GetComponentInChildren<SimpleShoot>().enabled = true;
        }

    }
    private void DisableWeapon(Transform weapon)
    {
        MeshRenderer[] meshs = weapon.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.enabled = false;
        }

        weapon.GetComponentInChildren<Weapon>().canShoot = false;
        weapon.GetComponentInChildren<Weapon>().isAvailable = false;
        if (weapon.GetComponentInChildren<Weapon>().ammoType == AmmoType.Sniper)
        {
            WeaponZoom wZ = weapon.GetComponentInChildren<WeaponZoom>();
            if (wZ == null) return;
            wZ.ZoomOut();
            wZ.enabled = false;
        }

        if (weapon.GetComponentInChildren<Weapon>().ammoType == AmmoType.Pistol)
        {
            weapon.GetComponentInChildren<SimpleShoot>().enabled = false;
        }
    }
}