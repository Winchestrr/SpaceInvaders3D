using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        WeaponSystem weaponSystem = collision.gameObject.GetComponentInChildren<WeaponSystem>();

        foreach (GunBase gunBase in weaponSystem.weapons)
        {
            Debug.Log(gunBase);

            if(gunBase.gameObject.tag == weaponPrefab.tag)
            {
                AmmoWeapon tempWeapon = gunBase.gameObject.GetComponent<AmmoWeapon>();

                //tempWeapon.bulletsLeft += tempWeapon.allBullets;
                break;
            }
        }

        if (weaponSystem != null)
        {
            weaponSystem.AddWeapon(weaponPrefab);
            //animacja znikania / particle
            Destroy(gameObject);
        }
    }
}
